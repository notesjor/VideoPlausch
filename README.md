# VideoPlausch (Plauschi Client & Server)

## Die Idee
Durch Corona entfallen viele Möglichkeiten für einen kleinen zufälligen Plausch. Zum Beispiel eine Einführungsveranstaltung für Erstsemester - normalerweise würde man sich aufgeregt in den Vorlesungssaal setzen und sich links und rechts mit seinen Sitznachbarn austauschen (vor, während oder nach der Vorlesung). Diese Software simuliert genau diese Art von unkonventioneller Kommunikation und ist dabei fast so niederschwellig zu nutzen, wie ein Plausch im realen Leben.

## Was kostet Plauschi?
Plauschi (Server und Client) sind kostenfrei. Sie können Plauschi für Forschung (Konferenzen, Tagungen, etc.) und Lehre (Seminare, Vorlesungen, etc.) einsetzen.

## Wie funktioniert Plauschi?
Die für die Nutzer*innen wesentliche Oberfläche basiert auf HTML5 und JavaScript (im Folgenden 'Client' genannt). Dieser Client meldet sich dann mit einer frei konfigurierbaren EventID beim Plauschi-Server an. Der Plauschi-Server erstellt automatisch Gruppen für dieses Event und befüllt die Gruppen mit neuen Nutzer*innen. Ist eine Gruppe voll, startet der Client die Video-Konferenz. Ungeduldige Nutzer*innen können aber auch selbst die Video-Konferenz starten. 

## Anleitung für Dozent*innen

### Anleitung für Dozent*innen (ohne technische Kenntnisse und Möglichkeiten)
- Schreiben Sie mir eine E-Mail (an: jan.ruediger@uni-siegen.de - bitte mit Angabe zu Veranstaltungsname und Datum) und Sie erhalten innerhalb weniger Werktage einen Link (URL), den Sie nutzen können (z. B. per E-Mail an Teilnehmer*innen verschicken oder per moodle teilen).

### Anleitung für Dozent*innen (mit einfachen technischen Kenntnissen und Möglichkeiten)
- Laden Sie sich die Datei "Plauschi.html" aus dem aktuellen Release herunter: https://github.com/notesjor/VideoPlausch/releases
- Öffnen Sie die Datei mit einem HTML-Editor. Folgende Werte sollten Sie anpassen:
  - eventID = Tragen Sie hier den Name ihres Events ein - z. B. "Uni Siegen - Erstsemestertreff"
  - groupSize = Wie groß soll eine Gruppe sein? - Sind alle Plätze belegt, startet das Meeting automatisch. Achtung: WebRTC basierte Video-Konferenz-Systeme wie Jitsi haben ein praktisches Limit von ca. 20 Personen. Wenn Sie den (kosten-)freien Plauschi-Server nutzen (siehe plauschiURL) können Sie hier max. 15 Personen pro Gruppe zulassen.
  - videoURL = Basis-URL z. B. für ihre Jitsi-Instanz (Plauschi funktioniert auch mit anderen Video-Konferenz-Systemen). Falls z. B. ihre Universität einen eigenen Video-Konferenz-Server betreibt, können Sie hier die URL eintragen. Achtung: ZOOM bietet m. W. n. keine derartige Option.
  - plauschiURL = Basis-URL für den Plauschi-Server (optional). Sie können hier den (kosten-)freien Plauschi-Server "https://plauschi.corpusexplorer.de/api" verwenden. Wie Sie einen eigenen Plauschi-Server installieren, erfahren Sie weiter unten. Siehe hierzu auch: Welche Daten speichert der Plauschi-Server?
  - Weitere Anpassungen sind optional (z. B. Austausch des Hintergrundbildes). Siehe Kommentare im HTML-Dokument.
- Laden Sie die geänderte HTML-Datei auf einen Webserver und verteilen Sie die URL an ihre Studierenden.
- Hinweis: Wenn Sie wollen, können Sie im Quellcode das Hintergrundbild, RevelJS und JQuery durch eine lokale (selbst gehostete) Dateie ersetzen. Dadurch werden externe Server-Anfragen vermieden.

### Anleitung für Dozent*innen (mit komplexen technischen Kenntnissen und Möglichkeiten)
- Wenn Sie einen eigenen Plauschi-Server betreiben möchten, laden Sie sich aus dem aktuellen Release die Datei "Server.zip" herunter: https://github.com/notesjor/VideoPlausch/releases
- Entpacken Sie die Dateien.
- Systemvoraussetzung:
  - Linux: Falls Sie Plauschi-Server auf einem Linux-Server installieren, müssen Sie das Mono-Framework 6.x installieren (https://www.mono-project.com/download/stable/). Nachdem Mono installiert wurde, sollten Sie den Plauschi-Server einmalig (auch nach jedem Update) für ihr System kompilieren. Dies geschieht durch folgenden Aufruf: mono --aot=nrgctx-trampolines=8096,nimt-trampolines=8096,ntrampolines=4048 --server PlauschiServer.exe
  - Windows: Auf einem Windows-Server ist keine weitere Installation nötig, da i. d. R. das .NET Framework über das Windows-Update installiert wird. Falls es doch zu Komplikationen unter Windows kommen sollte, installieren Sie bitte das .NET Framework ab 4.6.1 manuell.
- Starten Sie den Plauschi-Server einmalig. 
  - Windows: PlauschiServer.exe ausführen
  - Linux: mono --server PlauschiServer.exe
- Beim ersten Start bricht der Server ab. Er erzeugt eine Datei "server.cnf". Öffnen Sie diese Datei mit einem JSON-Editor. Folgendes kann geändert werden:
  - HostnameOrIp = Standardwert "*" - Bedeutet: Alle Hostnamen und IPs werden akzeptiert. Wenn Sie hier die IP oder den Hostnamen einschränken möchten. z. B. weil Plauschi hinter einem Reverse-Proxy konfiguriert wird, können Sie hier die IP/Hostnamen des Proxys eintragen. Hinweis: Plauschi bietet keine SSL/TLS-Transportverschlüsselung - wenn Sie eine Verschlüsselung wünschen, nutzen Sie (wie erwähnt) einen Reverse-Proxy, der diese Transportverschlüsselung übernimmt.
  - Port = An welchem Port soll Plauschi lauschen? Der Standardport ist 8123.
  - MaxUsersPerGroup = Wie viele Nutzer pro Gruppe sind serverseitig erlaubt. Hinweis: Dies ist ein hartes Limit. Clientseitig kann ein beliebiges kleineres Limit gewählt werden.
  - MaxEvents = Wie viele Events verwaltet der Server maximal? Standardwert: 1000
- Starten Sie den Plauschi-Server erneut (Befehl wie oben). Es bietet sich an, Plauschi mittels crontab (Linux) oder Aufgabenplanung (Windows) direkt nach Start des Systems auszuführen.

### Welche Daten speichert der Plauschi-Server?
Keine (!!!) - wie Sie im Quellcode des Servers sehen können (https://github.com/notesjor/VideoPlausch/blob/master/PlauschiServer/PlauschiServer/Program.cs), speichert der Server keinerlei Daten. Die einzigen Daten, die für eine Gruppenbündelung notwendig sind, verbleiben im flüchtigen Arbeitsspeicher. Ist eine Gruppe voll, wird sie automatisch aus dem System gelöscht. IP der Nutzer*innen oder andere personenbezogene Daten werden erst gar nicht erhoben. 

## Fragen / Probleme
Reichen Sie gerne Fragen/Probleme über den Issue-Tracker ein: https://github.com/notesjor/VideoPlausch/issues
Bei persönlichen oder sehr konkreten Fragen können Sie auch eine E-Mail an mich schicken: jan.ruediger@uni-siegen.de
