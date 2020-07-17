using Analogy.DataProviders.Extensions;
using Analogy.Interfaces;
using Analogy.Interfaces.Factories;
using Analogy.LogViewer.PlainTextParser.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analogy.LogViewer.PlainTextParser
{

    public class PlainTextFactory : IAnalogyFactory
    {
        internal static Guid AnalogyPlainTextGuid { get; } = new Guid("11CBFA00-DA2E-2F9F-B5A1-BE978FD09D57");
        public Guid FactoryId { get; } = AnalogyPlainTextGuid;
        public string Title { get; } = "Plain Text Parser";
        public IEnumerable<IAnalogyChangeLog> ChangeLog => PlainTextParser.ChangeLog.GetChangeLog();
        public IEnumerable<string> Contributors { get; } = new List<string> { "Lior Banai" };
        public string About { get; } = "Plain Text Parser";

    }

    public class AnalogyPlainTextDataProviderFactory : IAnalogyDataProvidersFactory
    {
        public virtual Guid FactoryId { get; } = PlainTextFactory.AnalogyPlainTextGuid;
        public virtual string Title { get; } = "Plain Text Provider";
        public IEnumerable<IAnalogyDataProvider> DataProviders { get; }

        public AnalogyPlainTextDataProviderFactory()
        {
            DataProviders = new List<IAnalogyDataProvider> { new PlainTextDataProvider(UserSettingsManager.UserSettings.LogParserSettings) };
        }
    }

    public class AnalogyCustomActionFactory : IAnalogyCustomActionsFactory
    {
        public Guid FactoryId { get; set; } = PlainTextFactory.AnalogyPlainTextGuid;

        public string Title { get; } = "Plain Text tools";
        public IEnumerable<IAnalogyCustomAction> Actions { get; }

        public AnalogyCustomActionFactory()
        {
            Actions = new List<IAnalogyCustomAction>(0);
        }
    }

    public class AnalogyPlainTextParserSettings : IAnalogyDataProviderSettings
    {
        public virtual Guid FactoryId { get; set; } = PlainTextFactory.AnalogyPlainTextGuid;
        public Guid ID { get; set; } = new Guid("20DC5AD8-CDBF-47AD-8227-89451291A1E3");

        public string Title { get; } = "Plain Text Settings";
        public UserControl DataProviderSettings { get; } = new PlainTextSettingSettings();
        public Image SmallImage { get; } = Resources.Analogy_small_16x16;
        public Image LargeImage { get; } = null;

        public Task SaveSettingsAsync()
        {
            UserSettingsManager.UserSettings.Save();
            return Task.CompletedTask;
        }

    }
}
