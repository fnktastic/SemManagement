using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace SemManagement.UWP.Converters
{
    public class BoolToVisibleOrCollapsed : IValueConverter
    {
        public bool Reverse { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool bValue = (bool)value;

            if (bValue != Reverse)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            Visibility visibility = (Visibility)value;

            if (visibility == Visibility.Visible)
                return !Reverse;
            else
                return Reverse;
        }
    }
}
