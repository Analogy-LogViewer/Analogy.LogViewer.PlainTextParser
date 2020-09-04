using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Analogy.Interfaces;
using Analogy.LogViewer.PlainTextParser.Properties;

namespace Analogy.LogViewer.PlainTextParser.IAnalogy
{
    public class ComponentImages : IAnalogyComponentImages
    {
        public Image GetLargeImage(Guid analogyComponentId)
        {
            if (analogyComponentId == PlainTextFactory.AnalogyPlainTextGuid)
                return Resources.New_32x32;
            return null;
        }

        public Image GetSmallImage(Guid analogyComponentId)
        {
            if (analogyComponentId == PlainTextFactory.AnalogyPlainTextGuid)
                return Resources.New_16x16;
            return null;
        }

        public Image GetOnlineConnectedLargeImage(Guid analogyComponentId) => null;

        public Image GetOnlineConnectedSmallImage(Guid analogyComponentId) => null;

        public Image GetOnlineDisconnectedLargeImage(Guid analogyComponentId) => null;

        public Image GetOnlineDisconnectedSmallImage(Guid analogyComponentId) => null;
    }
}
