using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Analogy.DataProviders.Extensions;
using Analogy.Interfaces;
using Analogy.Interfaces.Factories;
using Analogy.LogViewer.PlainTextParser.Properties;

namespace Analogy.LogViewer.PlainTextParser
{

    public class PlainTextFactory : IAnalogyFactory
    {
        public static Guid AnalogyPlainTextGuid { get; } = new Guid("11CBFA00-DA2E-2F9F-B5A1-BE978FD09D57");
        public Guid FactoryID { get; } = AnalogyPlainTextGuid;
        public string Title { get; } = "Analogy Plain Text Parser";
        public IAnalogyDataProvidersFactory DataProviders { get; }
        public IAnalogyCustomActionsFactory Actions { get; }
        public IEnumerable<IAnalogyChangeLog> ChangeLog => PlainTextParser.ChangeLog.GetChangeLog();
        public IEnumerable<string> Contributors { get; } = new List<string> { "Lior Banai" };
        public string About { get; } = "Analogy Plain Text Parser";
        public PlainTextFactory()
        {
            DataProviders = new AnalogyPlainTextDataProviderFactory();
            Actions = new AnalogyCustomActionFactory();

        }


    }

    public class AnalogyPlainTextDataProviderFactory : IAnalogyDataProvidersFactory
    {
        public string Title { get; } = "Analogy Plain Text Provider";
        public IEnumerable<IAnalogyDataProvider> Items { get; }

        public AnalogyPlainTextDataProviderFactory()
        {
            var builtInItems = new List<IAnalogyDataProvider>();
            var adpnlog = new PlainTextDataProvider(UserSettingsManager.UserSettings.LogParserSettings);
            builtInItems.Add(adpnlog);
            Items = builtInItems;
        }
    }

    public class AnalogyCustomActionFactory : IAnalogyCustomActionsFactory
    {
        public string Title { get; } = "Analogy Plain Text tools";
        public IEnumerable<IAnalogyCustomAction> Items { get; }

        public AnalogyCustomActionFactory()
        {
            Items = new List<IAnalogyCustomAction>(0);
        }
    }

    public class AnalogyPlainTextParserSettings : IAnalogyDataProviderSettings
    {
       
        public Guid ID { get; } = new Guid("1D14EC70-60C0-1823-BE9C-F1A59303FFB3");
        
        public string Title { get; } = "Plain Text Settings";
        public UserControl DataProviderSettings { get; } = new PlainTextUserSettingSettings();
        public Image Icon { get; } = Resources.Analogy_small_16x16;

        public Task SaveSettingsAsync()
        {
            UserSettingsManager.UserSettings.Save();
            return Task.CompletedTask;
        }

    }
}
