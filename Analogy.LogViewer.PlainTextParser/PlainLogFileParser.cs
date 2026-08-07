using Analogy.Interfaces;
using Analogy.Interfaces.DataTypes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Analogy.LogViewer.PlainTextParser
{
    public class PlainLogFileParser
    {
        private readonly ISplitterLogParserSettings _logFileSettings;
        public readonly string[] splitters;
        public static string[] SplitterValues { get; } = { "#*#" };
        private static readonly string[] SupportedDateFormats =
        {
            "yyyy-MM-dd HH:mm:ss:fff",
            "yyyy-MM-dd HH:mm:ss.fff",
            "yyyy-MM-dd HH:mm:ss,fff",
            "yyyy-MM-dd HH:mm:ss",
            "yyyy-MM-ddTHH:mm:ss.fff",
            "yyyy-MM-ddTHH:mm:ss,fff",
            "yyyy-MM-ddTHH:mm:ss",
            "yyyy-MM-ddTHH:mm:ss.fffK",
            "yyyy-MM-ddTHH:mm:ssK",
            "yyyy-MM-ddTHH:mm:sszzz",
            "yyyy-MM-dd HH:mm:sszzz",
        };

        public PlainLogFileParser(ISplitterLogParserSettings logFileSettings)
        {
            _logFileSettings = logFileSettings;
            splitters = _logFileSettings.Splitter.Split(SplitterValues, StringSplitOptions.None);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AnalogyLogMessage Parse(string line)
        {
            var items = line.Split(splitters, StringSplitOptions.RemoveEmptyEntries).ToList();
            List<(AnalogyLogMessagePropertyName, string)> map = new List<(AnalogyLogMessagePropertyName, string)>();
            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];
                if (_logFileSettings.Maps.TryGetValue(i, out AnalogyLogMessagePropertyName value))
                {
                    map.Add((value, NormalizeMappedValue(value, item)));
                }
            }
            return AnalogyLogMessage.Parse(map);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string NormalizeMappedValue(AnalogyLogMessagePropertyName property, string value)
        {
            if (property != AnalogyLogMessagePropertyName.Date || string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            if (DateTimeOffset.TryParseExact(value, SupportedDateFormats, CultureInfo.InvariantCulture,
                    DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.AssumeLocal, out DateTimeOffset parsedDateOffset))
            {
                return parsedDateOffset.LocalDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
            }

            if (DateTime.TryParseExact(value, SupportedDateFormats, CultureInfo.InvariantCulture,
                    DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.AssumeLocal, out DateTime parsedDate))
            {
                return parsedDate.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
            }

            return value;
        }
    }
}