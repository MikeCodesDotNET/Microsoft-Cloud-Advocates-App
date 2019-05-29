using System;
using System.Globalization;
using Advocates.Models;
using Xamarin.Forms;

namespace Advocates.Converters
{
    public class OffsetToLocalTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var referenceTime = (ReferenceTime)value;

            var standardOffset = referenceTime.StandardOffset;
            var dls = referenceTime.DaylightSavings;

            TimeSpan sOffset;
            TimeSpan.TryParse(standardOffset, out sOffset);

            TimeSpan dlsOffset;
            TimeSpan.TryParse(dls, out dlsOffset);

            var localTime = DateTime.Now.ToUniversalTime().Add(sOffset);
            localTime = localTime.Add(dlsOffset);
            return localTime.ToString("t", CultureInfo.CreateSpecificCulture("en-US"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
