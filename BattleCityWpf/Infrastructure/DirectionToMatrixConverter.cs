using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using BattleCityWpf.Model;

namespace BattleCityWpf.Infrastructure
{
    class DirectionToMatrixConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var direction = (Facing) value;
            var matrix = Matrix.Identity;
            if (direction == Facing.South)
            {
                matrix.Scale(1, -1);
                matrix.Translate(0, GameField.CellSize);
            }
            if (direction == Facing.East)
            {
                matrix.Rotate(90);
                matrix.Translate(GameField.CellSize, 0);
            }
            if (direction == Facing.West)
            {
                matrix.Rotate(90);
                matrix.Scale(-1, 1);
            }
            return matrix;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
