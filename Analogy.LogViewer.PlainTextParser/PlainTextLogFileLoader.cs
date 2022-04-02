using Analogy.Interfaces;
using Analogy.Interfaces.DataTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Analogy.LogViewer.PlainTextParser
{
    public class PlainTextLogFileLoader
    {
        private ISplitterLogParserSettings _logFileSettings;
        private PlainLogFileParser _parser;
        public PlainTextLogFileLoader(ISplitterLogParserSettings logFileSettings)
        {
            _logFileSettings = logFileSettings;
            _parser = new PlainLogFileParser(_logFileSettings);
        }
        public async Task<IEnumerable<AnalogyLogMessage>> Process(string fileName, CancellationToken token, ILogMessageCreatedHandler messagesHandler)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                AnalogyLogMessage empty = new AnalogyLogMessage($"File is null or empty. Aborting.",
                    AnalogyLogLevel.Critical, AnalogyLogClass.General, "Analogy", "None")
                {
                    Source = "Analogy",
                    Module = System.Diagnostics.Process.GetCurrentProcess().ProcessName
                };
                messagesHandler.AppendMessage(empty, Utils.GetFileNameAsDataSource(fileName));
                return new List<AnalogyLogMessage> { empty };
            }
            if (!_logFileSettings.IsConfigured)
            {
                AnalogyLogMessage empty = new AnalogyLogMessage($"File Parser is not configured. Please set it first in the settings Window",
                    AnalogyLogLevel.Critical, AnalogyLogClass.General, "Analogy", "None")
                {
                    Source = "Analogy",
                    Module = System.Diagnostics.Process.GetCurrentProcess().ProcessName
                };
                messagesHandler.AppendMessage(empty, Utils.GetFileNameAsDataSource(fileName));
                return new List<AnalogyLogMessage> { empty };
            }
            if (!_logFileSettings.CanOpenFile(fileName))
            {
                AnalogyLogMessage empty = new AnalogyLogMessage($"File {fileName} Is not supported or not configured correctly in the windows settings",
                    AnalogyLogLevel.Critical, AnalogyLogClass.General, "Analogy", "None")
                {
                    Source = "Analogy",
                    Module = System.Diagnostics.Process.GetCurrentProcess().ProcessName
                };
                messagesHandler.AppendMessage(empty, Utils.GetFileNameAsDataSource(fileName));
                return new List<AnalogyLogMessage> { empty };
            }
            List<AnalogyLogMessage> messages = new List<AnalogyLogMessage>();
            try
            {
                long count = 0;
                using (var stream = File.OpenRead(fileName))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        while (!reader.EndOfStream)
                        {
                            var line = await reader.ReadLineAsync() ?? "";
                            var items = line.Split(_parser.splitters, StringSplitOptions.RemoveEmptyEntries);
                            while (items.Length < _logFileSettings.ValidItemsCount)
                            {
                                line = line + Environment.NewLine + await reader.ReadLineAsync();
                                items = line.Split(_parser.splitters, StringSplitOptions.RemoveEmptyEntries);
                            }
                            var entry = _parser.Parse(line);
                            messages.Add(entry);
                            count++;
                            messagesHandler.ReportFileReadProgress(new AnalogyFileReadProgress(AnalogyFileReadProgressType.Incremental, 1, count, count));
                        }
                    }
                }
                messagesHandler.AppendMessages(messages, fileName);
                return messages;
            }
            catch (Exception e)
            {
                AnalogyLogMessage empty = new AnalogyLogMessage($"Error occured processing file {fileName}. Reason: {e.Message}",
                    AnalogyLogLevel.Critical, AnalogyLogClass.General, "Analogy", "None")
                {
                    Source = "Analogy",
                    Module = System.Diagnostics.Process.GetCurrentProcess().ProcessName
                };
                messagesHandler.AppendMessage(empty, Utils.GetFileNameAsDataSource(fileName));
                return new List<AnalogyLogMessage> { empty };
            }
        }
    }
}
