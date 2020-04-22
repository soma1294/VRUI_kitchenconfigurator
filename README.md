# Hinweise für M. Hudritsch
## Advanced GameDev

<p>Die Prefabs und Skripts für das GameDev Projekt befinden sich im Asset-Ordner "Teleporting".<br> 
Die Szene in der alle Skripts und Prefabs in Verwendung sind heisst "ButtonDemo".</p>

<p>In der gleichen Szene befindet sich auch das für Projekt 2 erstellte "UI-Panel". Dieses funktioniert noch nicht optimal mit dem teleportieren.
Spezifisch wird die Position des UI-Panels noch nicht korrekt angepasst, wenn der Spieler sich teleportiert. 
Falls sie das UI-Panel stört können sie das GameObject "CanvasTopParent" deaktivieren.</p>

## Projekt 2: KitchenConfigurator II or UI and UX in VR

<p>Der KitchenConfigurator hat bereits das neue Teleport System integriert.</p>

<p>Die Prefabs und Skripts der UI Elemente für das Projekt 2 befinden sich im Asset-Ordner "VRUI".<br>
Die Szene in der alle Skripts und Prefabs in Verwendung sind ist ebenfalls "ButtonDemo".</p>

<p>Das UI sollte wegen der erwähnten Probleme wenn möglich in der Start Position getestet werden.</p>

# Bachelor: UI/UX-Design in VR
**Student: Steven Henz**<br>
**Dozent: Markus Hudritsch**

<p>Ziel der Arbeit ist es mithilfe von Papers, Fachliteratur, Community-Tipps und Usertests ein auf OVR Applikationen angepasstes UI in Unity zu erstellen. 
Am Ende der Entwicklung soll ein Unity-Package vorhanden sein, das einfach importiert werden kann und so von OVR-Entwicklern implementiert werden kann ohne, 
dass es nötig ist ein eigenes UI zu entwickeln. Die einzelnen Komponenten sollten so selbsterklärend wie möglich sein, 
damit die Verwendung eines Handbuches wenn möglich komplett wegfällt. Nichtsdestotrotz soll zusätzlich ein Handbuch erstellt werden, 
dass die einzelnen Komponenten erklärt und Tipps zu deren Verwendung gibt. Um die Möglichkeiten dieses so erstellte UI aufzuzeigen, 
wird die Benutzeroberfläche des Unity Projekts «VRRoomConfigurator» von Mathias Spring mit diesem neuen System umgesetzt. 
Auch soll UI des «VRRoomConfigurator» Projekts verbessert werden, so dass zum Beispiel nicht verwendete UI-Elemente ausgeblendet werden.</p>

<p>Das Design des UI selbst wird darauf fokussiert sein, dass ein Bedienen durch Tasteneingaben an den VR-Touch Kontrollern so stark wie möglich reduziert wird. 
Der Grund dafür ist es, dass es für VR Neulinge so einfach wie möglich sein sollte das UI zu bedienen. 
Dies bedeutet das jegliche UI Elemente durch Berührung mit den virtuellen Händen bedient werden. 
Es soll darauf geachtet werden das bei Designentscheidung auf die Ergonomie geachtet wird und das UI von Rechts- sowie Linkshändern genau gleich gut verwendet werden kann. 
Ebenfalls soll der Benutzer beim Bedienen ein klares haptisches Feedback erhalten.</p>

<p>Da wie bereits erwähnt Eingaben direkt an den Kontrollern stark reduziert werden sollen, wird ebenfalls das Teleportsystem so umgebaut, 
dass keine Eingabe am Kontroller nötig sein wird.</p>

Optional:<br>
Ebenfalls soll das Projekt «VRRoomConfigurator» um einige Funktionen erweitert werden:
* Bauteile können nach dem setzen wieder ausgewählt werden und geändert werden.
* Das Löschen von bereits platzierten Möbeln soll geändert werden, so dass diese nicht mehr in eine feste «DeleteZone» platziert werden müssen.
* Die Position der «PreviewBox» für die Möbel kann beliebig gesetzt werden, um die Bedienung zu vereinfachen.
* Die Logik der Vorschau-Box für das setzen der Möbel soll verbessert werden, da diese momentan nicht wie erwartet funktioniert beim setzen von Möbeln neben alte Möbel.
* Die Logik der Platzierung der Möbel soll verbessert werden, damit zum Beispiel Bodenmöbel nicht an der Decke platziert werden können.
* Die Qualität der dynamisch generierten Möbel soll verbessert werden, um die Immersion zu erhöhen.
* Die Grafische Qualität soll besser an die Occulus Quest angepasst werden.
* Ein möglichst genaues Handbuch soll erstellt werden, dass es zukünftigen Entwicklern vereinfacht an diesem Projekt weiterzuarbeiten.