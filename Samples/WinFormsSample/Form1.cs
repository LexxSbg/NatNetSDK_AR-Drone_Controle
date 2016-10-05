

//=============================================================================
// Copyright © 2009 NaturalPoint, Inc. All Rights Reserved.
// 
// This software is provided by the copyright holders and contributors "as is" and
// any express or implied warranties, including, but not limited to, the implied
// warranties of merchantability and fitness for a particular purpose are disclaimed.
// In no event shall NaturalPoint, Inc. or contributors be liable for any direct,
// indirect, incidental, special, exemplary, or consequential damages
// (including, but not limited to, procurement of substitute goods or services;
// loss of use, data, or profits; or business interruption) however caused
// and on any theory of liability, whether in contract, strict liability,
// or tort (including negligence or otherwise) arising in any way out of
// the use of this software, even if advised of the possibility of such damage.
//=============================================================================

//#############################################################################################################################
// Die Codeteile zwischen NATNETSDK Ende und NATNETSDK Anfang unterstehen dem 
// Copyright (C) 2016 Kerschbaumer und stehen für weitere Verwendung frei zur 
// Verfügung nach http://www.gnu.org/licenses/. 

// Die Verwendung dieses Programmes erfolgt auf eigene Gefahr! Für die Funktionsweise 
// sowie Resultate durch die Verwendung dieses Programmes wird in keiner Hinsicht 
// Garantie übernommen! 
//#############################################################################################################################

using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using System.Runtime.InteropServices;
using System.Net.Sockets;
using NatNetML;

/*
 *
 * Simple C# .NET sample showing how to use the NatNet managed assembly (NatNETML.dll).
 * 
 * It is designed to illustrate using NatNet.  There are some inefficiencies to keep the
 * code as simple to read as possible.
 * 
 * Sections marked with a [NatNet] are NatNet related and should be implemented in your code.
 * 
 * This sample uses the Microsoft Chart Controls for Microsoft .NET for graphing, which
 * requires the following assemblies:
 *   - System.Windows.Forms.DataVisualization.Design.dll
 *   - System.Windows.Forms.DataVisualization.dll
 * Make sure you have these in your path when building and redistributing.
 * 
 */

namespace WinFormTestApp 
{
    public partial class Form1 : Form 
    {
        // [NatNet] Our NatNet object
        private NatNetML.NatNetClientML m_NatNet;

        // [NatNet] Our NatNet Frame of Data object
        private NatNetML.FrameOfMocapData m_FrameOfData = new NatNetML.FrameOfMocapData();
        
        
        // [NatNet] Description of the Active Model List from the server (e.g. Motive)
        NatNetML.ServerDescription desc = new NatNetML.ServerDescription();

        // [NatNet] Queue holding our incoming mocap frames the NatNet server (e.g. Motive)
        private Queue<NatNetML.FrameOfMocapData> m_FrameQueue = new Queue<NatNetML.FrameOfMocapData>();

        // spreadsheet lookup
        Hashtable htMarkers = new Hashtable();
        Hashtable htRigidBodies = new Hashtable();
        List<RigidBody> mRigidBodies = new List<RigidBody>();
        Hashtable htSkelRBs = new Hashtable();

        Hashtable htForcePlates = new Hashtable();
        List<ForcePlate> mForcePlates = new List<ForcePlate>();

        // graphing support
        const int GraphFrames = 500;
        const int maxSeriesCount = 10;

        // frame timing information
        double m_fLastFrameTimestamp = 0.0f;
        float m_fCurrentMocapFrameTimestamp = 0.0f;
        float m_fFirstMocapFrameTimestamp = 0.0f;
        QueryPerfCounter m_FramePeriodTimer = new QueryPerfCounter();
        QueryPerfCounter m_UIUpdateTimer = new QueryPerfCounter();

        // server information
        double m_ServerFramerate = 1.0f;
        float m_ServerToMillimeters = 1.0f;
        int m_UpAxis = 1;   // 0=x, 1=y, 2=z (Y default)
        int mAnalogSamplesPerMocpaFrame = 0;
        int mDroppedFrames = 0;
        int mLastFrame = 0;

        private static object syncLock = new object();
        private delegate void OutputMessageCallback(string strMessage);
        private bool mPaused = false;

        // UI updating
        delegate void UpdateUICallback();
        bool mApplicationRunning = true;
        Thread UIUpdateThread;

        // polling
        delegate void PollCallback();
        Thread pollThread;
        bool mPolling = false;

// NatNetSDK Ende #############################################################################################################################

        //Variable ob Drohne fliegt oder nicht
        int onof = 0;

        //Variable ob UDP läuft
        int udprun = 0;

        //Variable um Drohne zu bewegen
        int move = 0;

        //Variable emergency aktiviert
        int emergy = 0;

        //Variablen Move
        float froll = 0F;
        float fpitch = 0F;
        float fyaw = 0F;
        float fgaz = 0F;

        //AR.Drohne Verbindung herstellen http://www.msh-tools.com/ardrone/ARDrone_Developer_Guide.pdf

        private IPAddress IPAddr;
        private IPEndPoint IP5556;
        private UdpClient ATCmdPort;

        public void InitDrohnConnection()
        {
            // Die Drone hat standardmäßig die erste IP im 1ner Netz.
            IPAddr = IPAddress.Parse("192.168.1.1");

            // Auf dieser IP öffnet die Drone den Port 5556 zur Datenübertragung
            IP5556 = new IPEndPoint(IPAddr, 5556);

            // Ein UDP-Client auf den Daten Port der Drone 5556 erstellen
            ATCmdPort = new UdpClient(5556);    
        }

        
        public void SendCommand(string command)
        {
            // Wandeln in Binär zur Übertragung
            byte[] commandBytes = Encoding.ASCII.GetBytes(command);

            // Sende Binärdaten zur Drone
            ATCmdPort.Send(commandBytes, commandBytes.Length, IP5556);
        }

// NatNetSDK Anfang #############################################################################################################################

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Show available ip addresses of this machine
            String strMachineName = Dns.GetHostName();
            //IPHostEntry ipHost = Dns.GetHostByName(strMachineName); veraltet
            IPHostEntry ipHost = Dns.GetHostEntry(strMachineName);
            foreach (IPAddress ip in ipHost.AddressList)
            {
                string strIP = ip.ToString();
                comboBoxLocal.Items.Add(strIP);
            }
            int selected = comboBoxLocal.Items.Add("193.171.53.68");
            comboBoxLocal.SelectedItem = comboBoxLocal.Items[selected];

            // create NatNet client
            int iConnectionType = 0;
            int iResult = CreateClient(iConnectionType);

            // create and run an Update UI thread
            UpdateUICallback d = new UpdateUICallback(UpdateUI);
            UIUpdateThread = new Thread(() =>
            {
                while (mApplicationRunning)
                {
                    try
                    {
                        this.Invoke(d);
                        Thread.Sleep(15);
                    }
                    catch (System.Exception ex)
                    {
                        OutputMessage(ex.Message);
                        break;
                    }
                }
            });
            UIUpdateThread.Start();

            // create and run a polling thread
            PollCallback pd = new PollCallback(PollData);
            pollThread = new Thread(() =>
            {
                while (mPolling)
                {
                    try
                    {
                        this.Invoke(pd);
                        Thread.Sleep(15);
                    }
                    catch (System.Exception ex)
                    {
                        OutputMessage(ex.Message);
                        break;
                    }
                }
            });
        }

        /// <summary>
        /// Create a new NatNet client, which manages all communication with the NatNet server (e.g. Motive)
        /// </summary>
        /// <param name="iConnectionType">0 = Multicast, 1 = Unicast</param>
        /// <returns></returns>
        private int CreateClient(int iConnectionType)
        {
            // release any previous instance
            if (m_NatNet != null)
            {
                m_NatNet.Uninitialize();
            }

            // [NatNet] create a new NatNet instance
            m_NatNet = new NatNetML.NatNetClientML(iConnectionType);

            // [NatNet] set a "Frame Ready" callback function (event handler) handler that will be
            // called by NatNet when NatNet receives a frame of data from the server application
            m_NatNet.OnFrameReady += new NatNetML.FrameReadyEventHandler(m_NatNet_OnFrameReady);

            /*
            // [NatNet] for testing only - event signature format required by some types of .NET applications (e.g. MatLab)
            m_NatNet.OnFrameReady2 += new FrameReadyEventHandler2(m_NatNet_OnFrameReady2);
            */

            // [NatNet] print version info
            int[] ver = new int[4];
            ver = m_NatNet.NatNetVersion();
            String strVersion = String.Format("NatNet Version : {0}.{1}.{2}.{3}", ver[0], ver[1], ver[2], ver[3]);
            OutputMessage(strVersion);

            return 0;
        }

        /// <summary>
        /// Connect to a NatNet server (e.g. Motive)
        /// </summary>
        private void Connect()
        {
            // [NatNet] connect to a NatNet server
            int returnCode = 0;
            string strLocalIP = comboBoxLocal.SelectedItem.ToString();
            string strServerIP = textBoxServer.Text;
            returnCode = m_NatNet.Initialize(strLocalIP, strServerIP);
            if (returnCode == 0)
                OutputMessage("Initialization Succeeded.");
            else
            {
                OutputMessage("Error Initializing.");
                checkBoxConnect.Checked = false;
            }

            // [NatNet] validate the connection
            returnCode = m_NatNet.GetServerDescription(desc);
            if (returnCode == 0)
            {
                OutputMessage("Connection Succeeded.");
                OutputMessage("   Server App Name: " + desc.HostApp);
                OutputMessage(String.Format("   Server App Version: {0}.{1}.{2}.{3}", desc.HostAppVersion[0], desc.HostAppVersion[1], desc.HostAppVersion[2], desc.HostAppVersion[3]));
                OutputMessage(String.Format("   Server NatNet Version: {0}.{1}.{2}.{3}", desc.NatNetVersion[0], desc.NatNetVersion[1], desc.NatNetVersion[2], desc.NatNetVersion[3]));
                checkBoxConnect.Text = "Disconnect";

                // Tracking Tools and Motive report in meters - lets convert to millimeters
                if (desc.HostApp.Contains("TrackingTools") || desc.HostApp.Contains("Motive"))
                    m_ServerToMillimeters = 1000.0f;

                // [NatNet] [optional] Query mocap server for the current camera framerate
                int nBytes = 0;
                byte[] response = new byte[10000];
                int rc;
                rc = m_NatNet.SendMessageAndWait("FrameRate", out response, out nBytes);
                if (rc == 0)
                {
                    try
                    {
                        m_ServerFramerate = BitConverter.ToSingle(response, 0);
                        OutputMessage(String.Format("   Camera Framerate: {0}", m_ServerFramerate));
                    }
                    catch (System.Exception ex)
                    {
                        OutputMessage(ex.Message);
                    }
                }

                // [NatNet] [optional] Query mocap server for the current analog framerate
                rc = m_NatNet.SendMessageAndWait("AnalogSamplesPerMocapFrame", out response, out nBytes);
                if (rc == 0)
                {
                    try
                    {
                        mAnalogSamplesPerMocpaFrame = BitConverter.ToInt32(response, 0);
                        OutputMessage(String.Format("   Analog Samples Per Camera Frame: {0}", mAnalogSamplesPerMocpaFrame));
                    }
                    catch (System.Exception ex)
                    {
                        OutputMessage(ex.Message);
                    }
                }

                // [NatNet] [optional] Query mocap server for the current up axis
                rc = m_NatNet.SendMessageAndWait("UpAxis", out response, out nBytes);
                if (rc == 0)
                {
                    m_UpAxis = BitConverter.ToInt32(response, 0);
                }

                m_fCurrentMocapFrameTimestamp = 0.0f;
                m_fFirstMocapFrameTimestamp = 0.0f;
                mDroppedFrames = 0;
            }
            else
            {
                OutputMessage("Error Connecting.");
                checkBoxConnect.Checked = false;
                checkBoxConnect.Text = "Connect";
            }

        }

        private void Disconnect()
        {           
            // [NatNet] disconnect
            // optional : for unicast clients only - notify Motive we are disconnecting
            int nBytes = 0;
            byte[] response = new byte[10000];
            int rc;
            rc = m_NatNet.SendMessageAndWait("Disconnect", out response, out nBytes);
            if (rc == 0)
            {

            }
            // shutdown our client socket
            m_NatNet.Uninitialize();
            checkBoxConnect.Text = "Connect";
        }

        private void checkBoxConnect_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxConnect.Checked)
            {
                Connect();
            }
            else
            {
                Disconnect();
            }
        }

        private void OutputMessage(string strMessage)
        {
            if (mPaused)
                return;

            if (!mApplicationRunning)
                return;

            if (this.listView1.InvokeRequired)
            {
                // It's on a different thread, so use Invoke
                OutputMessageCallback d = new OutputMessageCallback(OutputMessage);
                this.Invoke(d, new object[] { strMessage });
            }
            else
            {
                // It's on the same thread, no need for Invoke
                DateTime d = DateTime.Now;
                String strTime = String.Format("{0}:{1}:{2}:{3}", d.Hour, d.Minute, d.Second, d.Millisecond);
                ListViewItem item = new ListViewItem(strTime, 0);
                item.SubItems.Add(strMessage);
                listView1.Items.Add(item);
            }
            
        }
      
        private RigidBody FindRB(int id, int parentID = -2)
        {
            foreach (RigidBody rb in mRigidBodies)
            {
                if (rb.ID == id)
                {
                    if (parentID != -2)
                    {
                        if (rb.parentID == parentID)
                            return rb;
                    }
                    else
                    {
                        return rb;
                    }
                }
            }
            return null;
        }
        
        /// <summary>
        /// Update the spreadsheet.  
        /// Note: This refresh is quite slow and provided here only as a complete example. 
        /// In a production setting this would be optimized.
        /// </summary>
        private void UpdateDataGrid()
        {
            // update MarkerSet data
            for (int i = 0; i < m_FrameOfData.nMarkerSets; i++)
            {
                NatNetML.MarkerSetData ms = m_FrameOfData.MarkerSets[i];
                for (int j = 0; j < ms.nMarkers; j++)
                {
                    string strUniqueName = ms.MarkerSetName + j.ToString();
                    int key = strUniqueName.GetHashCode();
                    if (htMarkers.Contains(key))
                    {
                        int rowIndex = (int)htMarkers[key];
                        if (rowIndex >= 0)
                        {
                            dataGridView1.Rows[rowIndex].Cells[1].Value = ms.Markers[j].x;
                            dataGridView1.Rows[rowIndex].Cells[2].Value = ms.Markers[j].y;
                            dataGridView1.Rows[rowIndex].Cells[3].Value = ms.Markers[j].z;
                        }
                    }
                }
            }

            // update RigidBody data
            for (int i = 0; i < m_FrameOfData.nRigidBodies; i++)
            {
                NatNetML.RigidBodyData rb = m_FrameOfData.RigidBodies[i];
                int key = rb.ID.GetHashCode();

// NatNetSDK Ende #############################################################################################################################
               
                NatNetML.MarkerSetData rm = m_FrameOfData.MarkerSets[i];

// NatNetSDK Anfang #############################################################################################################################               

                // note : must add rb definitions here one time instead of on get data descriptions because we don't know the marker list yet.
                if (!htRigidBodies.ContainsKey(key))
                {
                    // Add RigidBody def to the grid
                    if ((rb.Markers[0] != null) && (rb.Markers[0].ID != -1))
                    {
                        string name;
                        RigidBody rbDef = FindRB(rb.ID);
                        if (rbDef != null)
                        {
                            name = rbDef.Name;
                        }
                        else
                        {
                            //name = rb.ID.ToString();

// NatNetSDK Ende #############################################################################################################################
        
                 name = rm.MarkerSetName;
                            
// NatNetSDK Anfang #############################################################################################################################
                        }
      
                        int rowIndex = dataGridView1.Rows.Add("RigidBody: " + name);

                        key = rb.ID.GetHashCode();
                        htRigidBodies.Add(key, rowIndex);

                        // Add Markers associated with this rigid body to the grid
                        for (int j = 0; j < rb.nMarkers; j++)
                        {
                            String strUniqueName = name + "-" + rb.Markers[j].ID.ToString();
                            int keyMarker = strUniqueName.GetHashCode();
                            int newRowIndexMarker = dataGridView1.Rows.Add(strUniqueName);
                            htMarkers.Add(keyMarker, newRowIndexMarker);
                        }
                    }
                }
                else
                {
                    // update RigidBody data
                    int rowIndex = (int)htRigidBodies[key];
                    if (rowIndex >= 0)
                    {
                        bool tracked = rb.Tracked;
                        if (!tracked)
                        {
                            OutputMessage("RigidBody not tracked in this frame.");
                        }

                        dataGridView1.Rows[rowIndex].Cells[1].Value = rb.x * m_ServerToMillimeters;
                        dataGridView1.Rows[rowIndex].Cells[2].Value = rb.y * m_ServerToMillimeters;
                        dataGridView1.Rows[rowIndex].Cells[3].Value = rb.z * m_ServerToMillimeters;

                        // Convert quaternion to eulers.  Motive coordinate conventions: X(Pitch), Y(Yaw), Z(Roll), Relative, RHS
                        float[] quat = new float[4] { rb.qx, rb.qy, rb.qz, rb.qw };
                        float[] eulers = new float[3];
                        eulers = m_NatNet.QuatToEuler(quat, (int)NATEulerOrder.NAT_XYZr);
                        double x = RadiansToDegrees(eulers[0]);     // convert to degrees
                        double y = RadiansToDegrees(eulers[1]);
                        double z = RadiansToDegrees(eulers[2]);

                        dataGridView1.Rows[rowIndex].Cells[4].Value = x;
                        dataGridView1.Rows[rowIndex].Cells[5].Value = y;
                        dataGridView1.Rows[rowIndex].Cells[6].Value = z;

                        // update Marker data associated with this rigid body
                        for (int j = 0; j < rb.nMarkers; j++)
                        {
                            if (rb.Markers[j].ID != -1)
                            {
                                string name;
                                RigidBody rbDef = FindRB(rb.ID);
                                if (rbDef != null)
                                {
                                    name = rbDef.Name;
                                }
                                else
                                {
                                    name = rb.ID.ToString();
                                }

                                String strUniqueName = name + "-" + rb.Markers[j].ID.ToString();
                                int keyMarker = strUniqueName.GetHashCode();
                                if (htMarkers.ContainsKey(keyMarker))
                                {
                                    int rowIndexMarker = (int)htMarkers[keyMarker];
                                    NatNetML.Marker m = rb.Markers[j];
                                    dataGridView1.Rows[rowIndexMarker].Cells[1].Value = m.x;
                                    dataGridView1.Rows[rowIndexMarker].Cells[2].Value = m.y;
                                    dataGridView1.Rows[rowIndexMarker].Cells[3].Value = m.z;
                                }
                            }
                        }
                    }
                }
           }

// NatNetSDK Ende #############################################################################################################################

            DrohneMove();

// NatNetSDK Anfang #############################################################################################################################
         
            // update labeled markers data
            // remove previous dynamic marker list
            // for testing only - this simple approach to grid updating too slow for large marker count use
            int rowOffset = htMarkers.Count + htRigidBodies.Count + htForcePlates.Count + 1;
            int labeledCount = 0;
            if (false)
            {
                int nTotalRows = dataGridView1.Rows.Count;
                for (int i = rowOffset; i < nTotalRows; i++)
                    dataGridView1.Rows.RemoveAt(rowOffset);
                for (int i = 0; i < m_FrameOfData.nMarkers; i++)
                {
                    NatNetML.Marker m = m_FrameOfData.LabeledMarkers[i];

                    int modelID, markerID;
                    m_NatNet.DecodeID(m.ID, out modelID, out markerID);
                    string name = "Labeled Marker (ModelID: " + modelID + "  MarkerID: " + markerID + ")";
                    if (modelID == 0)
                        name = "UnLabeled Marker ( ID: " + markerID + ")";
                    int rowIndex = dataGridView1.Rows.Add(name);
                    dataGridView1.Rows[rowIndex].Cells[1].Value = m.x;
                    dataGridView1.Rows[rowIndex].Cells[2].Value = m.y;
                    dataGridView1.Rows[rowIndex].Cells[3].Value = m.z;
                    labeledCount++;
                }
            }

            // DEPRECATED
            // update unlabeled markers data
            // remove previous dynamic marker list
            // for testing only - this simple approach to grid updating too slow for large marker count use
            rowOffset += labeledCount;
            if (false)
            {
                int nTotalRows = dataGridView1.Rows.Count;
                for (int i = rowOffset; i < nTotalRows; i++)
                    dataGridView1.Rows.RemoveAt(rowOffset);
                for (int i = 0; i < m_FrameOfData.nOtherMarkers; i++)
                {
                    NatNetML.Marker m = m_FrameOfData.OtherMarkers[i];
                    int rowIndex = dataGridView1.Rows.Add("Unlabeled Marker (ID: " + m.ID + ")");
                    dataGridView1.Rows[rowIndex].Cells[1].Value = m.x;
                    dataGridView1.Rows[rowIndex].Cells[2].Value = m.y;
                    dataGridView1.Rows[rowIndex].Cells[3].Value = m.z;
                }
            }
        }

// NatNetSDK Ende #############################################################################################################################

        private void AddSequencnumber()
        {
            if (Command.sequenceNumber == -1)
            {
            Command.sequenceNumber = Command.sequenceNumber +2;    
            }
            else { 
            Command.sequenceNumber = Command.sequenceNumber++;
            }
        }

        private void DroneOnOff_Click(object sender, EventArgs e)
        {
            if (udprun == 1)
            {
                if (onof == 0)
                {
                   // Drohne aufsteigen lassen
                    AddSequencnumber();
                    FlightModeCommand takeoff = new FlightModeCommand(DroneFlightMode.TakeOff);
                    SendCommand(takeoff.CreateCommand());

                    AddSequencnumber();
                    HoverModeCommand hover = new HoverModeCommand(DroneHoverMode.Hover);
                    SendCommand(hover.CreateCommand());

                    onof = 1;

                    Console.WriteLine("Drohne -> take off");
                    OutputMessage("Drohne -> take off");
                }
                else if (onof == 1)
                {
                    // Drohne landen lassen
                    AddSequencnumber();
                    FlightModeCommand landing = new FlightModeCommand(DroneFlightMode.Land);
                    SendCommand(landing.CreateCommand());

                    Command.sequenceNumber = -1;

                    move = 0;
                    onof = 0;

                    Console.WriteLine("Drohne -> landing");
                    OutputMessage("Drohne -> landing");

                }
            }
        }

        private void Emergency_Click_1(object sender, EventArgs e)
        {     
            if (udprun == 1 && onof == 1) 
            { 
                // Emergency ausführen
                AddSequencnumber();
                FlightModeCommand emergency = new FlightModeCommand(DroneFlightMode.Emergency);
                SendCommand(emergency.CreateCommand());

                emergy = 1;
                move = 0;
                onof = 0;

                Console.WriteLine("Drohne -> emergency");
                OutputMessage("Drohne -> emergency");
            }
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            if (emergy == 1) 
            {            
                // Emergency zurücksetzen
                AddSequencnumber();
                FlightModeCommand reset = new FlightModeCommand(DroneFlightMode.Reset);
                SendCommand(reset.CreateCommand());

                Command.sequenceNumber = -1;

                emergy = 0;

                Console.WriteLine("Drohne -> emergency");
                OutputMessage("Drohne -> emergency");
            }
        }

        // Drohne bewegen
        private void MoveDrone_Click(object sender, EventArgs e)
        {
            if (onof == 1 && udprun == 1 && move == 0)
            {
                move = 1;

                Console.WriteLine("Drohne -> Move aktiviert");
                OutputMessage("Drohne -> Move aktiviert");
            }
            else if(move == 1 || onof == 0 || udprun == 0)
            {                
                AddSequencnumber();
                HoverModeCommand hover = new HoverModeCommand(DroneHoverMode.Hover);
                SendCommand(hover.CreateCommand());
                move = 0;

                Console.WriteLine("Drohne -> Move deaktiviert");
                OutputMessage("Drohne -> Move deaktiviert");
            }
        }

        private void DrohneMove()
        {

            // Drohne und Controller abfragen

            NatNetML.MarkerSetData rm1 = m_FrameOfData.MarkerSets[0];
            NatNetML.MarkerSetData rm2 = m_FrameOfData.MarkerSets[1];

            NatNetML.RigidBodyData rb1 = m_FrameOfData.RigidBodies[0];
            NatNetML.RigidBodyData rb2 = m_FrameOfData.RigidBodies[1];
            NatNetML.RigidBodyData temp;

            //Überprüfen welcher der eingehenden Rigidbodys die Drohne ist und welcher die Hand
           if (rm2.MarkerSetName == "drone")
            {
               temp=rb1;
               rb1 = rb2;
               rb2 = temp;
               temp = null;
            }

            // mind. Abstand von Controller und Drohne für Reaktion 
            double spanne = 0.25;

            // Ausführungsdauer des Kommandos
            //int command_duration_ms = 0;

            // Bewegung in x Richtung
            if ((rb1.x < (rb2.x - spanne) || rb1.x > (rb2.x + spanne)) && move == 1)
            {
                if (rb2.x < rb1.x)
                {
                    fpitch = 0.05F;
                    AddSequencnumber();
                    FlightMoveCommand vor = new FlightMoveCommand(froll, fpitch, fyaw, fgaz);
                    SendCommand(vor.CreateCommand());
                    fpitch = 0;

                    //Thread.Sleep(command_duration_ms);

                    Console.WriteLine("Drohne -> zurück");
                    OutputMessage("Drohne -> zurück");
                }
                
                else if (rb2.x > rb1.x)
                {
                    fpitch = -0.05F;
                    AddSequencnumber();
                    FlightMoveCommand vor = new FlightMoveCommand(froll, fpitch, fyaw, fgaz);
                    SendCommand(vor.CreateCommand());
                    fpitch = 0;

                    //Thread.Sleep(command_duration_ms);

                    Console.WriteLine("Drohne -> vor");
                    OutputMessage("Drohne -> vor");
                }
            }

            // Bewegung in z Richtung
            if ((rb1.z < (rb2.z - spanne) || rb1.z > (rb2.z + spanne)) && move == 1)
            {
                if (rb2.z < rb1.z)
                {
                    froll = -0.05F;
                    AddSequencnumber();
                    FlightMoveCommand vor = new FlightMoveCommand(froll, fpitch, fyaw, fgaz);
                    SendCommand(vor.CreateCommand());
 
                    //Thread.Sleep(command_duration_ms);

                    Console.WriteLine("Drohne -> links");
                    OutputMessage("Drohne -> links");
                }

                else if (rb2.z > rb1.z)
                {
                    froll = 0.05F;
                    AddSequencnumber();
                    FlightMoveCommand vor = new FlightMoveCommand(froll, fpitch, fyaw, fgaz);
                    SendCommand(vor.CreateCommand());
                    
                   //Thread.Sleep(command_duration_ms);

                    Console.WriteLine("Drohne -> rechts");
                    OutputMessage("Drohne -> rechts");
                }

                froll = 0F;
                fpitch = 0F;
                /*rb1.x = 0;
                rb2.x = 0;
                rb1.z = 0;
                rb2.z = 0;*/
            }
        }

// NatNetSDK Anfang #############################################################################################################################

        void ProcessFrameOfData(ref NatNetML.FrameOfMocapData data)
        {
            // detect and reported any 'reported' frame drop (as reported by server)
            if (m_fLastFrameTimestamp != 0.0f)
            {
                double framePeriod = 1.0f / m_ServerFramerate;
                double thisPeriod = data.fTimestamp - m_fLastFrameTimestamp;
                double fudgeFactor = 0.002f; // 2 ms
                if ((thisPeriod - framePeriod) > fudgeFactor)
                {
                    //OutputMessage("Frame Drop: ( ThisTS: " + data.fTimestamp.ToString("F3") + "  LastTS: " + m_fLastFrameTimestamp.ToString("F3") + " )");
                    mDroppedFrames++;
                }
            }

            // check and report frame drop (frame id based)
            if (mLastFrame != 0)
            {
                if ((data.iFrame - mLastFrame) != 1)
                {
                    //OutputMessage("Frame Drop: ( ThisFrame: " + data.iFrame.ToString() + "  LastFrame: " + mLastFrame.ToString() + " )");
                    //mDroppedFrames++;
                }
            }

            // [NatNet] Add the incoming frame of mocap data to our frame queue,  
            // Note: the frame queue is a shared resource with the UI thread, so lock it while writing
            lock (syncLock)
            {
                // [optional] clear the frame queue before adding a new frame
                m_FrameQueue.Clear();
                FrameOfMocapData deepCopy = new FrameOfMocapData(data);
                m_FrameQueue.Enqueue(deepCopy);
            }

            mLastFrame = data.iFrame;
            m_fLastFrameTimestamp = data.fTimestamp;
        }

        /// <summary>
        /// [NatNet] m_NatNet_OnFrameReady will be called when a frame of Mocap
        /// data has is received from the server application.
        ///
        /// Note: This callback is on the network service thread, so it is
        /// important to return from this function quickly as possible 
        /// to prevent incoming frames of data from buffering up on the
        /// network socket.
        ///
        /// Note: "data" is a reference structure to the current frame of data.
        /// NatNet re-uses this same instance for each incoming frame, so it should
        /// not be kept (the values contained in "data" will become replaced after
        /// this callback function has exited).
        /// </summary>
        /// <param name="data">The actual frame of mocap data</param>
        /// <param name="client">The NatNet client instance</param>
        void m_NatNet_OnFrameReady(NatNetML.FrameOfMocapData data, NatNetML.NatNetClientML client)
        {
            double elapsedIntraMS = 0.0f;
            QueryPerfCounter intraTimer = new QueryPerfCounter();
            intraTimer.Start();

            // detect and report and 'measured' frame drop (as measured by client)
            m_FramePeriodTimer.Stop();
            double elapsedMS = m_FramePeriodTimer.Duration();

            ProcessFrameOfData(ref data);

            // report if we are taking too long, which blocks packet receiving, which if long enough would result in socket buffer drop
            intraTimer.Stop();
            elapsedIntraMS = intraTimer.Duration();
            if (elapsedIntraMS > 5.0f)
            {
                OutputMessage("Warning : Frame handler taking too long: " + elapsedIntraMS.ToString("F2"));
            }

            m_FramePeriodTimer.Start();
        }

        // [NatNet] [optional] alternate function signatured frame ready callback handler for .NET applications/hosts
        // that don't support the m_NatNet_OnFrameReady defined above (e.g. MATLAB)
        void m_NatNet_OnFrameReady2(object sender, NatNetEventArgs e)
        {
            m_NatNet_OnFrameReady(e.data, e.client);
        }

        private void PollData()
        {
            FrameOfMocapData data = m_NatNet.GetLastFrameOfData();
            ProcessFrameOfData(ref data);
        }

        private void SetDataPolling(bool poll)
        {
            if (poll)
            {
                // disable event based data handling
                m_NatNet.OnFrameReady -= m_NatNet_OnFrameReady;

                // enable polling 
                mPolling = true;
                pollThread.Start();
            }
            else
            {
                // disable polling
                mPolling = false;

                // enable event based data handling
                m_NatNet.OnFrameReady += new NatNetML.FrameReadyEventHandler(m_NatNet_OnFrameReady);
            }
        }

        private void GetLastFrameOfData()
        {
            FrameOfMocapData data = m_NatNet.GetLastFrameOfData();
            ProcessFrameOfData(ref data);
        }

        private void WriteFrame(FrameOfMocapData data)
        {
            String str = "";

            str += data.fTimestamp.ToString("F3") + "\t";

            // 'all' markerset data
            for (int i = 0; i < m_FrameOfData.nMarkerSets; i++)
            {
                NatNetML.MarkerSetData ms = m_FrameOfData.MarkerSets[i];
                if (ms.MarkerSetName == "all")
                {
                    for (int j = 0; j < ms.nMarkers; j++)
                    {
                        str += ms.Markers[j].x.ToString("F3") + "\t";
                        str += ms.Markers[j].y.ToString("F3") + "\t";
                        str += ms.Markers[j].z.ToString("F3") + "\t";
                    }
                }
            }

            // force plates
            // just write first subframe from each channel (fx[0], fy[0], fz[0], mx[0], my[0], mz[0])
            for (int i = 0; i < m_FrameOfData.nForcePlates; i++)
            {
                NatNetML.ForcePlateData fp = m_FrameOfData.ForcePlates[i];
                for (int iChannel = 0; iChannel < fp.nChannels; iChannel++)
                {
                    if (fp.ChannelData[iChannel].nFrames == 0)
                    {
                        str += 0.0f;    // empty frame
                    }
                    else
                    {
                        str += fp.ChannelData[iChannel].Values[0] + "\t";
                    }
                }
            }
        }

// NatNetSDK Ende #############################################################################################################################

        private void Connect_Drone(object sender, EventArgs e)
        {
            if (Connect_Drohne.Checked)
            {
                try
                {    
                    // Drohne verbinden
                    if (udprun == 0)
                    {
                        InitDrohnConnection();
                        udprun = 1;
                        Console.WriteLine("Drohne -> verbinden");
                        OutputMessage("Drohne -> verbinden");
                    }

                    // Drohne trennen
                    else if (udprun == 1 && onof == 0)
                    {
                        ATCmdPort.Close();
                        udprun = 0;
                        Console.WriteLine("Drohne -> trennen");
                        OutputMessage("Drohne -> trennen");
                    }              
                }
                catch (System.Exception ex)
                {
                    
                }
            }
        }

// NatNetSDK Anfang #############################################################################################################################

        private void UpdateUI()
        {
            m_UIUpdateTimer.Stop();
            double interframeDuration = m_UIUpdateTimer.Duration();

            QueryPerfCounter uiIntraFrameTimer = new QueryPerfCounter();
            uiIntraFrameTimer.Start();

            // the frame queue is a shared resource with the FrameOfMocap delivery thread, so lock it while reading
            // note this can block the frame delivery thread.  In a production application frame queue management would be optimized.
            lock (syncLock)
            {
                while (m_FrameQueue.Count > 0)
                {
                    m_FrameOfData = m_FrameQueue.Dequeue();

                    if (m_FrameQueue.Count > 0)
                        continue;

                    if (m_FrameOfData != null)
                    {
                        // for servers that only use timestamps, not frame numbers, calculate a 
                        // frame number from the time delta between frames
                        if (desc.HostApp.Contains("TrackingTools"))
                        {
                            m_fCurrentMocapFrameTimestamp = m_FrameOfData.fLatency;
                            if (m_fCurrentMocapFrameTimestamp == m_fLastFrameTimestamp)
                            {
                                continue;
                            }
                            if (m_fFirstMocapFrameTimestamp == 0.0f)
                            {
                                m_fFirstMocapFrameTimestamp = m_fCurrentMocapFrameTimestamp;
                            }
                            m_FrameOfData.iFrame = (int)((m_fCurrentMocapFrameTimestamp - m_fFirstMocapFrameTimestamp) * m_ServerFramerate);
                        }

                        // update the data grid
                        UpdateDataGrid();

                        // SMPTE timecode (if timecode generator present)
                        int hour, minute, second, frame, subframe;
                        bool bSuccess = m_NatNet.DecodeTimecode(m_FrameOfData.Timecode, m_FrameOfData.TimecodeSubframe, out hour, out minute, out second, out frame, out subframe);
                    }
                }
            }
            
            uiIntraFrameTimer.Stop();
            double uiIntraFrameDuration = uiIntraFrameTimer.Duration();
            m_UIUpdateTimer.Start();
        }

        public int LowWord(int number)
        {
            return number & 0xFFFF;
        }

        public int HighWord(int number)
        {
            return ((number >> 16) & 0xFFFF);
        }

        double RadiansToDegrees(double dRads)
        {
            return dRads * (180.0f / Math.PI);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            mApplicationRunning = false;

            if (UIUpdateThread.IsAlive)
                UIUpdateThread.Abort();

            m_NatNet.Uninitialize();
        }

        private void RadioMulticast_CheckedChanged(object sender, EventArgs e)
        {
            bool bNeedReconnect = checkBoxConnect.Checked;
            int iResult = CreateClient(0);
            if (bNeedReconnect)
                Connect();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void menuClear_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
        }

        private void menuPause_Click(object sender, EventArgs e)
        {
            mPaused = menuPause.Checked;
        }

        private void comboBoxLocal_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBoxServer_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void Local_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

    // Wrapper class for the windows high performance timer QueryPerfCounter
    // ( adapted from MSDN https://msdn.microsoft.com/en-us/library/ff650674.aspx )
    public class QueryPerfCounter
    {
        [DllImport("KERNEL32")]
        private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(out long lpFrequency);

        private long start;
        private long stop;
        private long frequency;
        Decimal multiplier = new Decimal(1.0e9);

        public QueryPerfCounter()
        {
            if (QueryPerformanceFrequency(out frequency) == false)
            {
                // Frequency not supported
                throw new Win32Exception();
            }
        }

        public void Start()
        {
            QueryPerformanceCounter(out start);
        }

        public void Stop()
        {
            QueryPerformanceCounter(out stop);
        }

        // return elapsed time between start and stop, in milliseconds.
        public double Duration()
        {
            double val = ((double)(stop - start) * (double)multiplier) / (double)frequency;
            val = val / 1000000.0f;   // convert to ms
            return val;
        }
    }
}