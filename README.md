# Analogy Plain Text Parser   [![Gitter](https://badges.gitter.im/Analogy-LogViewer/community.svg)](https://gitter.im/Analogy-LogViewer/community?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge)
plain text parser (can be used to read most logs formats)

for example, to parse simple Nlog files with the following line:


>2019-11-14 12:32:24.9449|INFO|Microsoft.Hosting.Lifetime|Application started. Press Ctrl+C to shut down.|ProcedureManager|16684

>2019-11-14 12:32:24.9449|INFO|Microsoft.Hosting.Lifetime|Hosting environment: Development|ProcedureManager|16684

1. open Analogy and   go to settings window of the parser:
Settings menu --> "Custom Data Provider Settings: --> "plain Text settings":
![Settings](Assets/Usage/step1.jpg)

copy the relevant line, split it (load layout button) and arrange the type to the text:
![Settings](Assets/Usage/step2.jpg)

after saving you can open the log from the "plain text parser tab:
![loaded log](Assets/Usage/loadedLog.jpg)

