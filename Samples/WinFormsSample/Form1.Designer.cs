namespace WinFormTestApp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.X = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Z = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Yaw = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pitch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Roll = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuClear = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPause = new System.Windows.Forms.ToolStripMenuItem();
            this.checkBoxConnect = new System.Windows.Forms.CheckBox();
            this.DroneOnOff = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.MoveDrone = new System.Windows.Forms.Button();
            this.Connect_Drohne = new System.Windows.Forms.CheckBox();
            this.comboBoxLocal = new System.Windows.Forms.ComboBox();
            this.Local = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxServer = new System.Windows.Forms.TextBox();
            this.Reset = new System.Windows.Forms.Button();
            this.Emergency = new System.Windows.Forms.Button();
            this.RadioMulticast = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.X,
            this.Y,
            this.Z,
            this.Yaw,
            this.Pitch,
            this.Roll});
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.ScrollBar;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.Size = new System.Drawing.Size(560, 466);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // ID
            // 
            this.ID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ID.FillWeight = 62.07922F;
            this.ID.HeaderText = "ID";
            this.ID.MinimumWidth = 50;
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // X
            // 
            dataGridViewCellStyle2.Format = "N4";
            dataGridViewCellStyle2.NullValue = null;
            this.X.DefaultCellStyle = dataGridViewCellStyle2;
            this.X.FillWeight = 22.52875F;
            this.X.HeaderText = "X";
            this.X.Name = "X";
            this.X.ReadOnly = true;
            this.X.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Y
            // 
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = null;
            this.Y.DefaultCellStyle = dataGridViewCellStyle3;
            this.Y.FillWeight = 22.52875F;
            this.Y.HeaderText = "Y";
            this.Y.Name = "Y";
            this.Y.ReadOnly = true;
            this.Y.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Z
            // 
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = null;
            this.Z.DefaultCellStyle = dataGridViewCellStyle4;
            this.Z.FillWeight = 22.52875F;
            this.Z.HeaderText = "Z";
            this.Z.Name = "Z";
            this.Z.ReadOnly = true;
            this.Z.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Yaw
            // 
            dataGridViewCellStyle5.Format = "N2";
            dataGridViewCellStyle5.NullValue = null;
            this.Yaw.DefaultCellStyle = dataGridViewCellStyle5;
            this.Yaw.FillWeight = 22.52875F;
            this.Yaw.HeaderText = "Pitch (X)";
            this.Yaw.Name = "Yaw";
            this.Yaw.ReadOnly = true;
            this.Yaw.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Pitch
            // 
            dataGridViewCellStyle6.Format = "N2";
            dataGridViewCellStyle6.NullValue = null;
            this.Pitch.DefaultCellStyle = dataGridViewCellStyle6;
            this.Pitch.FillWeight = 22.52875F;
            this.Pitch.HeaderText = "Yaw (Y)";
            this.Pitch.Name = "Pitch";
            this.Pitch.ReadOnly = true;
            this.Pitch.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Roll
            // 
            dataGridViewCellStyle7.Format = "N2";
            dataGridViewCellStyle7.NullValue = null;
            this.Roll.DefaultCellStyle = dataGridViewCellStyle7;
            this.Roll.FillWeight = 22.52875F;
            this.Roll.HeaderText = "Roll (Z)";
            this.Roll.Name = "Roll";
            this.Roll.ReadOnly = true;
            this.Roll.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.ContextMenuStrip = this.contextMenuStrip1;
            this.listView1.GridLines = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView1.Location = new System.Drawing.Point(578, 261);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(363, 180);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Time";
            this.columnHeader1.Width = 80;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Message";
            this.columnHeader2.Width = 400;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuClear,
            this.menuPause});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(106, 48);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // menuClear
            // 
            this.menuClear.Name = "menuClear";
            this.menuClear.Size = new System.Drawing.Size(105, 22);
            this.menuClear.Text = "Clear";
            this.menuClear.Click += new System.EventHandler(this.menuClear_Click);
            // 
            // menuPause
            // 
            this.menuPause.CheckOnClick = true;
            this.menuPause.Name = "menuPause";
            this.menuPause.Size = new System.Drawing.Size(105, 22);
            this.menuPause.Text = "Pause";
            this.menuPause.Click += new System.EventHandler(this.menuPause_Click);
            // 
            // checkBoxConnect
            // 
            this.checkBoxConnect.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxConnect.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
            this.checkBoxConnect.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.checkBoxConnect.Location = new System.Drawing.Point(58, 105);
            this.checkBoxConnect.Name = "checkBoxConnect";
            this.checkBoxConnect.Size = new System.Drawing.Size(92, 23);
            this.checkBoxConnect.TabIndex = 5;
            this.checkBoxConnect.Text = "Connect Motiv";
            this.checkBoxConnect.UseVisualStyleBackColor = true;
            this.checkBoxConnect.CheckedChanged += new System.EventHandler(this.checkBoxConnect_CheckedChanged);
            // 
            // DroneOnOff
            // 
            this.DroneOnOff.Location = new System.Drawing.Point(188, 105);
            this.DroneOnOff.Name = "DroneOnOff";
            this.DroneOnOff.Size = new System.Drawing.Size(100, 23);
            this.DroneOnOff.TabIndex = 11;
            this.DroneOnOff.Text = "Drohne ON/OFF";
            this.DroneOnOff.UseVisualStyleBackColor = true;
            this.DroneOnOff.Click += new System.EventHandler(this.DroneOnOff_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.Silver;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(578, 237);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(363, 21);
            this.label1.TabIndex = 13;
            this.label1.Text = "Messages";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(578, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(363, 222);
            this.tabControl1.TabIndex = 21;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.MoveDrone);
            this.tabPage1.Controls.Add(this.Connect_Drohne);
            this.tabPage1.Controls.Add(this.comboBoxLocal);
            this.tabPage1.Controls.Add(this.checkBoxConnect);
            this.tabPage1.Controls.Add(this.Local);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.textBoxServer);
            this.tabPage1.Controls.Add(this.DroneOnOff);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(355, 196);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Connect";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // MoveDrone
            // 
            this.MoveDrone.Location = new System.Drawing.Point(189, 146);
            this.MoveDrone.Name = "MoveDrone";
            this.MoveDrone.Size = new System.Drawing.Size(99, 23);
            this.MoveDrone.TabIndex = 25;
            this.MoveDrone.Text = "Move Drone";
            this.MoveDrone.UseVisualStyleBackColor = true;
            this.MoveDrone.Click += new System.EventHandler(this.MoveDrone_Click);
            // 
            // Connect_Drohne
            // 
            this.Connect_Drohne.Appearance = System.Windows.Forms.Appearance.Button;
            this.Connect_Drohne.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
            this.Connect_Drohne.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.Connect_Drohne.Location = new System.Drawing.Point(58, 146);
            this.Connect_Drohne.Name = "Connect_Drohne";
            this.Connect_Drohne.Size = new System.Drawing.Size(94, 23);
            this.Connect_Drohne.TabIndex = 24;
            this.Connect_Drohne.Text = "Connect Drohne";
            this.Connect_Drohne.UseVisualStyleBackColor = true;
            this.Connect_Drohne.CheckedChanged += new System.EventHandler(this.Connect_Drone);
            // 
            // comboBoxLocal
            // 
            this.comboBoxLocal.FormattingEnabled = true;
            this.comboBoxLocal.Location = new System.Drawing.Point(58, 23);
            this.comboBoxLocal.Name = "comboBoxLocal";
            this.comboBoxLocal.Size = new System.Drawing.Size(121, 21);
            this.comboBoxLocal.TabIndex = 17;
            this.comboBoxLocal.SelectedIndexChanged += new System.EventHandler(this.comboBoxLocal_SelectedIndexChanged);
            // 
            // Local
            // 
            this.Local.AutoSize = true;
            this.Local.Location = new System.Drawing.Point(9, 26);
            this.Local.Name = "Local";
            this.Local.Size = new System.Drawing.Size(33, 13);
            this.Local.TabIndex = 9;
            this.Local.Text = "Local";
            this.Local.Click += new System.EventHandler(this.Local_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Server";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // textBoxServer
            // 
            this.textBoxServer.Location = new System.Drawing.Point(58, 55);
            this.textBoxServer.Name = "textBoxServer";
            this.textBoxServer.Size = new System.Drawing.Size(121, 20);
            this.textBoxServer.TabIndex = 8;
            this.textBoxServer.Text = "193.171.53.68";
            this.textBoxServer.TextChanged += new System.EventHandler(this.textBoxServer_TextChanged);
            // 
            // Reset
            // 
            this.Reset.Location = new System.Drawing.Point(771, 478);
            this.Reset.Name = "Reset";
            this.Reset.Size = new System.Drawing.Size(99, 23);
            this.Reset.TabIndex = 30;
            this.Reset.Text = "Reset";
            this.Reset.UseVisualStyleBackColor = true;
            this.Reset.Click += new System.EventHandler(this.Reset_Click);
            // 
            // Emergency
            // 
            this.Emergency.Location = new System.Drawing.Point(640, 478);
            this.Emergency.Name = "Emergency";
            this.Emergency.Size = new System.Drawing.Size(99, 23);
            this.Emergency.TabIndex = 29;
            this.Emergency.Text = "Emergency";
            this.Emergency.UseVisualStyleBackColor = true;
            this.Emergency.Click += new System.EventHandler(this.Emergency_Click_1);
            // 
            // RadioMulticast
            // 
            this.RadioMulticast.AutoSize = true;
            this.RadioMulticast.Checked = true;
            this.RadioMulticast.Location = new System.Drawing.Point(12, 484);
            this.RadioMulticast.Name = "RadioMulticast";
            this.RadioMulticast.Size = new System.Drawing.Size(67, 17);
            this.RadioMulticast.TabIndex = 14;
            this.RadioMulticast.TabStop = true;
            this.RadioMulticast.Text = "Multicast";
            this.RadioMulticast.UseVisualStyleBackColor = true;
            this.RadioMulticast.CheckedChanged += new System.EventHandler(this.RadioMulticast_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(600, 453);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(323, 13);
            this.label3.TabIndex = 31;
            this.label3.Text = "Vorsicht! Bei Emergency schaltet die Drohne die Motoren sofort ab.";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.ClientSize = new System.Drawing.Size(953, 513);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Reset);
            this.Controls.Add(this.Emergency);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.RadioMulticast);
            this.Name = "Form1";
            this.Text = "NatNetSDK_ARDrone_Control";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.CheckBox checkBoxConnect;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button DroneOnOff;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label Local;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxServer;
        private System.Windows.Forms.RadioButton RadioMulticast;
        private System.Windows.Forms.ComboBox comboBoxLocal;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn X;
        private System.Windows.Forms.DataGridViewTextBoxColumn Y;
        private System.Windows.Forms.DataGridViewTextBoxColumn Z;
        private System.Windows.Forms.DataGridViewTextBoxColumn Yaw;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pitch;
        private System.Windows.Forms.DataGridViewTextBoxColumn Roll;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuClear;
        private System.Windows.Forms.ToolStripMenuItem menuPause;
        private System.Windows.Forms.CheckBox Connect_Drohne;
        private System.Windows.Forms.Button MoveDrone;
        private System.Windows.Forms.Button Emergency;
        private System.Windows.Forms.Button Reset;
        private System.Windows.Forms.Label label3;
    }
}

