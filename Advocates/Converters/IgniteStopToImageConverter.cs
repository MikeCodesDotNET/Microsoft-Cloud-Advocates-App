using System;
using System.Globalization;
using Xamarin.Forms;

namespace Advocates.Converters
{
    public class IgniteStopToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var city = value.ToString();

            switch(city)
            {
                case "Toronto":
                    return ImageSource.FromUri(new Uri("Toronto.png"));
                    
                case "Washington, D.C":
                    return ImageSource.FromUri(new Uri("Washington-DC.png"));
                    
                case "Singapore":
                    return ImageSource.FromUri(new Uri("Singapore.png"));

                case "Sydney":
                    return ImageSource.FromUri(new Uri("Sydney.png"));

                case "Hong Kong":
                    return ImageSource.FromUri(new Uri("Hongkong.png"));

                case "Seoul":
                    return ImageSource.FromUri(new Uri("Seoul.png"));

                case "Mumbai":
                    return ImageSource.FromUri(new Uri("Mumbai.png"));

                case "Berlin":
                    return ImageSource.FromUri(new Uri("Berlin.png"));

                case "Tel Aviv":
                    return ImageSource.FromUri(new Uri("Tel-Aviv.png"));

                case "Johannesburg":
                    return ImageSource.FromUri(new Uri("Johannesburg.png"));
                
                case "Milan":
                    return ImageSource.FromUri(new Uri("Milan.png"));

                case "London":
                    return ImageSource.FromUri(new Uri("London.png"));

                case "Amsterdam":
                    return ImageSource.FromUri(new Uri("Amsterdam.png"));

                case "Dubai":
                    return ImageSource.FromUri(new Uri("Dubai.png"));

                case "Stockholm":
                    return ImageSource.FromUri(new Uri("Stockholm.png"));

                case "São Paulo":
                    return ImageSource.FromUri(new Uri("SaoPaulo.png"));

                case "Mexico City":
                    return ImageSource.FromUri(new Uri("Mexico-City.png"));

                default:
                    return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
