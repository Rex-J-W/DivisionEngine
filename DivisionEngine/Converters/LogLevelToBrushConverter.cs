using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace DivisionEngine.Editor.Converters
{
    internal class LogLevelToBrushConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value switch
            {
                null => Brushes.White,
                LogLevel.Error => Brushes.Red,
                LogLevel.Warning => Brushes.Yellow,
                LogLevel.Info => Brushes.White,
                _ => Brushes.White
            };
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
