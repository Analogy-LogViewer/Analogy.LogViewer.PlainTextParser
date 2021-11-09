# Analogy Plain Text Parser

[![Nuget](https://img.shields.io/nuget/dt/Analogy.LogViewer.PlainTextParser)](https://www.nuget.org/packages/Analogy.LogViewer.PlainTextParser/) 
![.NET Core Desktop](https://github.com/Analogy-LogViewer/Analogy.LogViewer.PlainTextParser/workflows/.NET%20Core%20Desktop/badge.svg)
<a href="https://github.com/Analogy-LogViewer/Analogy.LogViewer.PlainTextParser/issues">
    <img src="https://img.shields.io/github/issues/Analogy-LogViewer/Analogy.LogViewer.PlainTextParser" alt="Issues" />
</a>
![GitHub closed issues](https://img.shields.io/github/issues-closed-raw/Analogy-LogViewer/Analogy.LogViewer.PlainTextParser)
<a href="https://github.com/Analogy-LogViewer/Analogy.LogViewer.PlainTextParser/blob/master/LICENSE.md">
    <img src="https://img.shields.io/github/license/Analogy-LogViewer/Analogy.LogViewer.PlainTextParser" alt="License" />
</a>
[![Nuget](https://img.shields.io/nuget/v/Analogy.LogViewer.PlainTextParser)](https://www.nuget.org/packages/Analogy.LogViewer.PlainTextParser/) 


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


## How To Use
1. Download the latest [Analogy Log Viewer](https://github.com/Analogy-LogViewer/Analogy.LogViewer) from the [release](https://github.com/Analogy-LogViewer/Analogy.LogViewer/releases) section (.net framework or .net Core version).
2. Download (or Compile) this project and put the compiled DLL in the same folder as the Analogy Log Viewer.
