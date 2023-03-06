using Analogy.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Analogy.Interfaces.DataTypes;
using Analogy.LogViewer.Template.Managers;

namespace Analogy.LogViewer.PlainTextParser
{
    public class PlainTextDataProvider : Analogy.LogViewer.Template.OfflineDataProvider
    {
        public override string? OptionalTitle { get; set; } = "Plain Text Parser";
        public override Guid Id { get; set; } = new Guid("4C002803-607F-4325-9C19-242FF1F29877");

        public override string FileOpenDialogFilters { get; set; } = "log files|*.txt";
        public override IEnumerable<string> SupportFormats { get; set; } = new[] { "*.txt" };
        public override string InitialFolderFullPath => Directory.Exists(UserSettings.Directory)
        ? UserSettings.Directory
        : string.Empty;

        private PlainTextLogFileLoader PlainTextLogFileParser { get; set; }

        private ISplitterLogParserSettings UserSettings { get; set; }

        public PlainTextDataProvider(ISplitterLogParserSettings userSettings)
        {
            UserSettings = userSettings;
            PlainTextLogFileParser = new PlainTextLogFileLoader(UserSettingsManager.UserSettings.LogParserSettings);
        }

        public override Task InitializeDataProvider(IAnalogyLogger logger)
        {
            LogManager.Instance.SetLogger(logger);
            return base.InitializeDataProvider(logger);
        }

            public override async Task<IEnumerable<IAnalogyLogMessage>> Process(string fileName, CancellationToken token,
            ILogMessageCreatedHandler messagesHandler)
        {
            if (CanOpenFile(fileName))
            {
                return await PlainTextLogFileParser.Process(fileName, token, messagesHandler);
            }
            return new List<AnalogyLogMessage>(0);

        }

        public override bool CanOpenFile(string fileName) => UserSettingsManager.UserSettings.LogParserSettings.CanOpenFile(fileName);
    }
}
