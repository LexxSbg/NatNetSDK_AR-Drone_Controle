# NatNetSDK_AR-Drone_Controle

Steuerung eines Multikopters mit Hilfe eines Hochgeschwindigkeits-Kamerasystem 
(Controlling a robot with a high-speed camera system)

https://youtu.be/aGydqx-c-9k

In diesem Projekt wird die Flugbahn eine Drohne durch reine Handbewegungen gesteuert. Dazu wurde ein Hochgeschwindigkeitskamerasystem verwendet, welches die Position der Drohne sowie der Hand im dreidimensionalen Raum bestimmt. Anhand dieser Positionsdaten wird der Abstand zwischen Drohne und Hand berechnet und die notwendigen Steuerbefehle, um diesen Abstand auszugleichen, an die Drohne gesendet. Sodass schlussendlich die Drohne stets der Hand folgt.

Die Geschwindigkeit der Drohne wurde für die Versuche in diesem eher kleinen Labor auf 1/20 der Maximalgeschwindigkeit eingestellt, weshalb der Flug der Drohne ein wenig träge wirkt. 

Hersteller des Kamerasystems: www.optitrack.com
Hersteller der Drohne: www.parrot.com

Für das Steuerprogramm wurden ein Beispielprogramm aus der SDK des Kameraherstellers (http://optitrack.com/downloads/developer-tools.html) adaptiert sowie vier Klassen des Projektes https://github.com/shtejv/ARDrone-Control-.NET implementiert.

Alle Codeteile des NatNetSDK_ARDrone Programms die nicht von mir stammen sind markiert. Die von mir erstellten Codeteile stehen für weitere Projekte frei zur Verfügung. 

Die Verwendung dieses Programmes erfolgt auf eigene Gefahr! Für die Funktionsweise sowie Resultate durch die Verwendung dieses Programmes wird in keiner Hinsicht Garantie übernommen oder gehaftet! 
