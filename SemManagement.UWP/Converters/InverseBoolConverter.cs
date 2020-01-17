using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace SemManagement.UWP.Converters
{
    public class InverseBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool bValue = (bool)value;

            return !bValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            bool bValue = (bool)value;

            return !bValue;
        }
    }
}
