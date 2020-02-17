using System;
using System.Collections.Generic;
using System.Device.Location;
using GoogleMapsApi;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Configuration;
using System.Net;
using System.Web.Script.Serialization;
using GoogleMapsApi.Entities.Geocoding.Request;
using GoogleMapsApi.Entities.Geocoding.Response;
using GoogleMapsApi.Entities.Common;



namespace UI.converter
{
    class AddReportConverter : IMultiValueConverter
    {
        private static string googleKey = ConfigurationSettings.AppSettings.Get("GoogleMapsKey");
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
        object[] val = new object[5];
        DateTime d = new DateTime();
       
         return values.Clone();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }


        private async void stringToGeoCoordinate(string address)
        {
            try
            {
                GeocodingRequest geocodeRequest = new GeocodingRequest();
                geocodeRequest.Address = address;
                geocodeRequest.ApiKey = googleKey;
                GeocodingResponse geocode = await GoogleMaps.Geocode.QueryAsync(geocodeRequest);
                //GeocodingResponse geocode = GoogleMaps.Geocode.Query(geocodeRequest);
                if (geocode.Status == Status.OK)
                {
                    IEnumerator<Result> iter = geocode.Results.GetEnumerator();
                    iter.MoveNext();
                    Location tempLocation = iter.Current.Geometry.Location;
                  //  CurrentLocation.Latitude = tempLocation.Latitude;
                   // CurrentLocation.Longitude = tempLocation.Longitude;
                   // flag = true;
                }
            }
            catch (Exception) { }
        }
    }
}
