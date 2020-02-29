using SemManagement.UWP.Model.Local.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace SemManagement.UWP.Converters
{
    public class FeedItemToMessageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if(value is FeedItem feedItem)
            {
                if(string.IsNullOrWhiteSpace(feedItem.Parent) == false)
                {
                    return $"{feedItem.Message} ({feedItem.Parent})";
                }

                return feedItem.Message;
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
