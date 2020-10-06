﻿using Analogy.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Analogy.Interfaces.DataTypes;

namespace Analogy.LogViewer.PlainTextParser
{
    public class PlainTextDataProvider : Analogy.LogViewer.Template.OfflineDataProvider
    {
        public override string OptionalTitle { get; set; } = "Plain Text Parser";
        public override Guid Id { get; set; } = new Guid("4C002803-607F-4325-9C19-242FF1F29877");

        public override string FileOpenDialogFilters { get; set; } = "log files|*.txt";
        public override IEnumerable<string> SupportFormats { get; set; } = new[] { "*.txt" };
        public override string InitialFolderFullPath => Directory.Exists(UserSettings?.Directory)
        ? UserSettings.Directory
        : string.Empty;

        private PlainTextLogFileLoader PlainTextLogFileParser { get; set; }

        private ISplitterLogParserSettings UserSettings { get; set; }

        public override  Image LargeImage { get; set; } = null;

        public override Image SmallImage { get; set; } = null;

        public PlainTextDataProvider(ISplitterLogParserSettings userSettings)
        {
            UserSettings = userSettings;
        }

        public override Task InitializeDataProviderAsync(IAnalogyLogger logger)
        {
            LogManager.Instance.SetLogger(logger);
            PlainTextLogFileParser = new PlainTextLogFileLoader(UserSettingsManager.UserSettings.LogParserSettings);
            return Task.CompletedTask;
        }

            public override async Task<IEnumerable<AnalogyLogMessage>> Process(string fileName, CancellationToken token,
            ILogMessageCreatedHandler messagesHandler)
        {
            if (CanOpenFile(fileName))
                return await PlainTextLogFileParser.Process(fileName, token, messagesHandler);
            return new List<AnalogyLogMessage>(0);

        }

        public override bool CanOpenFile(string fileName) => UserSettingsManager.UserSettings.LogParserSettings.CanOpenFile(fileName);
    }
}
