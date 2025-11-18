using Analogy.Interfaces;
using Analogy.LogViewer.Template.WinForms;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analogy.LogViewer.PlainTextParser
{
    public class PlainTextFactory : PrimaryFactoryWinForms
    {
        internal static Guid Id { get; } = new Guid("11CBFA00-DA2E-2F9F-B5A1-BE978FD09D57");
        public override Guid FactoryId { get; set; } = Id;
        public override string Title { get; set; } = "Plain Text Parser";
        public override IEnumerable<IAnalogyChangeLog> ChangeLog { get; set; } = PlainTextParser.ChangeLog.GetChangeLog();

        public override IEnumerable<string> Contributors { get; set; } = new List<string> { "Lior Banai" };
        public override string About { get; set; } = "Plain Text Parser";
    }

    public sealed class AnalogyPlainTextDataProviderFactory : DataProvidersFactoryWinForms
    {
        public override Guid FactoryId { get; set; } = PlainTextFactory.Id;
        public override string Title { get; set; } = "Plain Text Provider";
        public override IEnumerable<IAnalogyDataProvider> DataProviders { get; set; }

        public AnalogyPlainTextDataProviderFactory()
        {
            DataProviders = new List<IAnalogyDataProvider> { new PlainTextDataProvider(UserSettingsManager.UserSettings.LogParserSettings) };
        }
    }

    public class AnalogyCustomActionFactory : Analogy.LogViewer.Template.CustomActionsFactory
    {
        public override Guid FactoryId { get; set; } = PlainTextFactory.Id;

        public override string Title { get; set; } = "Plain Text tools";
        public override IEnumerable<IAnalogyCustomAction> Actions { get; }

        public AnalogyCustomActionFactory()
        {
            Actions = new List<IAnalogyCustomAction>(0);
        }
    }

    public class AnalogyPlainTextParserSettings : TemplateUserSettingsFactoryWinForms
    {
        public override Guid FactoryId { get; set; } = PlainTextFactory.Id;
        public override Guid Id { get; set; } = new Guid("20DC5AD8-CDBF-47AD-8227-89451291A1E3");

        public override string Title { get; set; } = "Plain Text Settings";
        public override UserControl DataProviderSettings { get; set; }

        public override void CreateUserControl(ILogger logger)
        {
            DataProviderSettings = new PlainTextSettingSettings();
        }

        public override Task SaveSettingsAsync()
        {
            UserSettingsManager.UserSettings.Save();
            return Task.CompletedTask;
        }
    }
}