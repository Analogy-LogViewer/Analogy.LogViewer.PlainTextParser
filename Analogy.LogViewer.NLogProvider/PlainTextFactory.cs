using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Analogy.DataProviders.Extensions;
using Analogy.Interfaces;
using Analogy.Interfaces.Factories;
using Analogy.LogViewer.NLogProvider;
using Analogy.LogViewer.NLogProvider.Properties;

namespace Analogy.LogViewer.PlainTextParser
{

    public class PlainTextFactory : IAnalogyFactory
    {
        public static Guid AnalogyNLogGuid { get; } = new Guid("65E6FD2B-1D9E-4DC2-883B-E82253E1FBD2");
        public Guid FactoryID { get; } = AnalogyNLogGuid;
        public string Title { get; } = "Analogy Plain Text Parser";
        public IAnalogyDataProvidersFactory DataProviders { get; }
        public IAnalogyCustomActionsFactory Actions { get; }
        public IEnumerable<IAnalogyChangeLog> ChangeLog => PlainTextParser.ChangeLog.GetChangeLog();
        public IEnumerable<string> Contributors { get; } = new List<string> { "Lior Banai" };
        public string About { get; } = "Analogy Plain Text Parser";
        public PlainTextFactory()
        {
            DataProviders = new AnalogyNLogDataProviderFactory();
            Actions = new AnalogyPlainTextCustomActionFactory();

        }


    }

    public class AnalogyNLogDataProviderFactory : IAnalogyDataProvidersFactory
    {
        public string Title { get; } = "Analogy NLogs Data Provider";
        public IEnumerable<IAnalogyDataProvider> Items { get; }

        public AnalogyNLogDataProviderFactory()
        {
            var builtInItems = new List<IAnalogyDataProvider>();
            var adpnlog = new PlainTextDataProvider(UserSettingsManager.UserSettings.LogParserSettings);
            builtInItems.Add(adpnlog);
            Items = builtInItems;
        }
    }

    public class AnalogyPlainTextCustomActionFactory : IAnalogyCustomActionsFactory
    {
        public string Title { get; } = "Analogy NLog Built-In tools";
        public IEnumerable<IAnalogyCustomAction> Items { get; }

        public AnalogyPlainTextCustomActionFactory()
        {
            Items = new List<IAnalogyCustomAction>(0);
        }
    }

    public class AnalogyPlainTextSettings : IAnalogyDataProviderSettings
    {
       
        public Guid ID { get; } = new Guid("AEE7B966-3A32-445B-8A4C-1BAD40624ABB");
        
        public string Title { get; } = "Plain Text Settings";
        public UserControl DataProviderSettings { get; } = new PlainTextUserControlSettings();
        public Image Icon { get; } = Resources.nlog;

        public Task SaveSettingsAsync()
        {
            UserSettingsManager.UserSettings.Save();
            return Task.CompletedTask;
        }

    }
}
