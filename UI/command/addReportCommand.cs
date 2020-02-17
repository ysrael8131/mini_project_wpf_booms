using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UI.ViewModels;
using BE;
using GoogleMapsApi.Entities.Geocoding.Request;
using GoogleMapsApi.Entities.Geocoding.Response;
using GoogleMapsApi;
using GoogleMapsApi.Entities.Common;
using System.Configuration;

namespace UI.command
{
    public class AddReportCommand : ICommand
    {
        private static string googleKey = ConfigurationSettings.AppSettings.Get("GoogleMapsKey");
        double Longitude = 0;
        double Latitude = 0;
        private IReportViewModel curentVM;

        public AddReportCommand(IReportViewModel VM)
        {
            this.curentVM = VM;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            var param = (object[])parameter;

           
            try
            {
                GeocodingRequest geocodeRequest = new GeocodingRequest();
                geocodeRequest.Address = (string)param[3];
                geocodeRequest.ApiKey = googleKey;
                GeocodingResponse geocode = await GoogleMaps.Geocode.QueryAsync(geocodeRequest);
                //GeocodingResponse geocode = GoogleMaps.Geocode.Query(geocodeRequest);
                if (geocode.Status == Status.OK)
                {
                    IEnumerator<Result> iter = geocode.Results.GetEnumerator();
                    iter.MoveNext();
                    Location tempLocation = iter.Current.Geometry.Location;
                    Latitude = tempLocation.Latitude;
                    Longitude = tempLocation.Longitude;
                    //flag = true;
                }
                else { throw new Exception("wqcddqcdq"); }
            }
            catch (Exception e) { MessageBox.Show(e.ToString()); }

            Report report = new Report();
           // report.stamID = 1111111;
            report.name = (string)param[2];
            report.Latitude = Latitude;
            report.Longitude = Longitude;
            report.address = (string)param[3];
            DateTime date = DateTime.Parse(param[0].ToString());
            DateTime time = DateTime.Parse(param[1].ToString());
            DateTime dateTime = new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second);
            report.date = dateTime;
            report.numBooms = int.Parse((string)param[4]);
            
            curentVM.AddReport(report);
            
        }

    }
}
