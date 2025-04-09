using Analogy.Interfaces.DataTypes;
using Analogy.LogViewer.Template.Managers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;

namespace Analogy.LogViewer.PlainTextParser
{
    public class UserSettingsManager
    {
        private static readonly Lazy<UserSettingsManager> _instance =
            new Lazy<UserSettingsManager>(() => new UserSettingsManager());
        public static UserSettingsManager UserSettings { get; set; } = _instance.Value;
        public string FileSetting { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Analogy.LogViewer", "AnalogyPlainTextSettings.json");
        public ISplitterLogParserSettings LogParserSettings { get; set; }

        public UserSettingsManager()
        {
            if (File.Exists(FileSetting))
            {
                try
                {
                    string data = File.ReadAllText(FileSetting);
                    LogParserSettings = System.Text.Json.JsonSerializer.Deserialize<SplitterLogParserSettings>(data);
                }
                catch (Exception ex)
                {
                    LogManager.Instance.LogError(ex, "Error loading user setting file", ex, "Plain Text Provider");
                    LogParserSettings = new SplitterLogParserSettings();
                    LogParserSettings.Splitter = "|";
                    LogParserSettings.SupportedFilesExtensions = new List<string> { "*.*" };
                }
            }
            else
            {
                LogParserSettings = new SplitterLogParserSettings();
                LogParserSettings.Splitter = "|";
                LogParserSettings.SupportedFilesExtensions = new List<string> { "*.*" };
            }
        }

        public void Save()
        {
            try
            {
                File.WriteAllText(FileSetting, System.Text.Json.JsonSerializer.Serialize(LogParserSettings));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}