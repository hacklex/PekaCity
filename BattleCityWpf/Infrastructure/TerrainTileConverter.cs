using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using BattleCityWpf.Model;

namespace BattleCityWpf.Infrastructure
{
    public class TerrainTileConverter : IValueConverter
    {
        private static Dictionary<TerrainTileType, ImageSource> _cache;

        Dictionary<TerrainTileType, ImageSource> GetCache()
        {
            var conv = new ImageSourceConverter();
            return
                _cache ??
                (_cache = Enum.GetValues(typeof (TerrainTileType)).OfType<TerrainTileType>().ToDictionary(t => t, t =>
                    (ImageSource) conv.ConvertFrom(new Uri(
                        $"pack://application:,,,/{Assembly.GetAssembly(typeof (TerrainTileType))};component/Resources/{t}.png",
                        UriKind.RelativeOrAbsolute))));
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => GetCache()[(TerrainTileType) value];

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
