using BE;
using DAL;
using GoogleMaps.LocationServices;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BL
{
    public class bl_imp : IBL, IDisposable
    {

        public bl_imp() { }
        public void addBoomLocation(BoomLocation boomLocation)
        {
            using (dal_imp dal = new dal_imp())
                dal.addBoomLocation(boomLocation);
        }

        public async void addReport(Report report)
        {
            using (dal_imp dal = new dal_imp())
            {
              

//                dal.addReport(report);
                List<Event> events = await GetEvents();
                List<Event> ev = events.FindAll(item => report.date.Date == item.start.Date && report.date >= item.start && item.start.AddMinutes(10) > report.date);
                if (ev.Count==0)
                {
                    Event newevent = new Event();
                    newevent.numbooms = report.numBooms;
                    newevent.start = report.date;

                    List<GeoCoordinate> coordinates = k_Means(new List<Report>() { report}, 1);
                    newevent.BoomLocations = new List<BoomLocation>();
                    newevent.reports = new List<Report>();
                    foreach (var item in coordinates)
                    {
                        BoomLocation boomLocation = new BoomLocation();
                        boomLocation.addressLatitude = item.Latitude;
                        boomLocation.addresslongitude = item.Longitude;
                        boomLocation.time = report.date;
                        boomLocation.update = false;
                        boomLocation.address = report.address;
                        boomLocation.@event = newevent;
                       
                        newevent.BoomLocations.Add(boomLocation);
                    }
                    report.@event = newevent;
                    newevent.reports.Add(report);

              
                    dal.addEvent(newevent);
                }
                else
                {

                    List<Report> report11 = ev[0].reports.ToList();
                    report11.Add(report);
                    List<GeoCoordinate> coordinates = k_Means(report11,report11.Count);
                    List<BoomLocation> booms = new List<BoomLocation>();
                    foreach (var item in coordinates)
                    {
                        BoomLocation boomLocation = new BoomLocation();
                        boomLocation.addressLatitude = item.Latitude;
                        boomLocation.addresslongitude = item.Longitude;
                        boomLocation.time = report.date;
                        
                        boomLocation.update = false;
                        booms.Add(boomLocation);
                        // boomLocation.@event = tempevent;
                       // tempevent.BoomLocations.Add(boomLocation);
                    }

                    updateEvent(ev[0].NumEvent,report,booms);
                }
            }
        }

        public void deleteBoomLocation(BoomLocation boomLocation)
        {
            using (dal_imp dal = new dal_imp())
            {
                dal.deleteBoomLocation(boomLocation);
            }
        }

        public void deleteReport(Report report)
        {
            using (dal_imp dal = new dal_imp())
                dal.deleteReport(report);
        }

        public BoomLocation GetBoomLocation(BoomLocation boomLocation)
        {
            using (dal_imp dal = new dal_imp())
                return dal.GetBoomLocation(boomLocation);
        }

        public IEnumerable<BoomLocation> getListBoomLocation()
        {
            using (dal_imp dal = new dal_imp())
                return dal.getListBoomLocation();
        }

        public IEnumerable<Report> getListReports()
        {
            using (dal_imp dal = new dal_imp())
                return dal.getListReports();
        }

        public Report GetReport(Report report)
        {
            using (dal_imp dal = new dal_imp())
                return dal.GetReport(report);
        }

        public void updateBoomLocation(BoomLocation boomLocation)
        {
            using (dal_imp dal = new dal_imp())
                dal.updateBoomLocation(boomLocation);
        }

        public void updateReport(Report report)
        {
            using (dal_imp dal = new dal_imp())
                dal.updateReport(report);
        }

        public List<GeoCoordinate> k_Means(List<Report> SequenceOfReports, int NumOfBooms)
        {

            List<GeoCoordinate> ci_List = new List<GeoCoordinate>();
            double latitude_Min;
            double latitude_Max;
            double longitude_Min;
            double longitude_Max;

            if (!SequenceOfReports.Any())
            {
                return null;
            }

            latitude_Min = SequenceOfReports.Min(item => item.Latitude);
            latitude_Max = SequenceOfReports.Max(item => item.Latitude);
            longitude_Min = SequenceOfReports.Min(item => item.Longitude);
            longitude_Max = SequenceOfReports.Max(item => item.Longitude);

            for (int i = 0; i < NumOfBooms; i++)
            {
                Random r = new Random();
                double latitude = latitude_Min + r.NextDouble() * (latitude_Max - latitude_Min);
                double longitude = longitude_Min + r.NextDouble() * (longitude_Max - longitude_Min);
                GeoCoordinate c = new GeoCoordinate(latitude, longitude);
                ci_List.Add(c);
            }

            bool is_Changed;
            do
            {
                is_Changed = false;

                for (int i = 0; i < SequenceOfReports.Count; i++)
                {
                    GeoCoordinate address = new GeoCoordinate(SequenceOfReports[i].Latitude, SequenceOfReports[i].Longitude);
                    double min = address.GetDistanceTo(ci_List[i]);
                    SequenceOfReports[i].stamID = 0;

                    for (int j = 1; j < ci_List.Count; j++)
                    {
                        double temp = address.GetDistanceTo(ci_List[j]);
                        if (temp < min)
                        {
                            min = temp;
                            is_Changed = true;
                            SequenceOfReports[i].stamID = j;
                        }
                    }

                }


                SequenceOfReports.OrderBy(c => c.stamID);
                int id = 0;
                double c_LongitudeSum = 0;
                double c_LatitudeSum = 0;
                int counter = 0;
                for (int i = 0; i < SequenceOfReports.Count; i++)
                {
                    GeoCoordinate address = new GeoCoordinate(SequenceOfReports[i].Latitude, SequenceOfReports[i].Longitude);
                    if (SequenceOfReports[i].stamID == id)
                    {
                        c_LatitudeSum += address.Latitude;
                        c_LongitudeSum += address.Longitude;
                        counter++;
                    }
                    if (SequenceOfReports[i].stamID != id)
                    {
                        ci_List[id].Latitude = c_LatitudeSum / counter;
                        ci_List[id].Longitude = c_LongitudeSum / counter;
                        counter = 0;
                        c_LongitudeSum = 0;
                        c_LatitudeSum = 0;
                        i--;
                        id++;
                    }
                }

            } while (is_Changed);

            return ci_List;
        }

        //public GeoCoordinate ConvertAddressToCoordinate(string address)
        //{
        //    GoogleLocationService locationService = new GoogleLocationService();
        //    MapPoint point = locationService.GetLatLongFromAddress(address);
        //    double latitude = point.Latitude;
        //    double longitude = point.Longitude;
        //    return new GeoCoordinate(latitude, longitude);
        //}

        public void addEvent(Event event1)
        {
            using (dal_imp dal = new dal_imp())
            {
                dal.addEvent(event1);
            }

        }

        public void deleteEvent(Event event1)
        {
            using (dal_imp dal = new dal_imp()) ;
        }

        public void updateEvent(int key, Report report, List<BoomLocation> boomLocations)
        {
            using (dal_imp dal = new dal_imp())
            {
                dal.updateEvent(key,report,boomLocations);
            }
        }

        public async Task<List<Event>> GetEvents()
        {
            using (dal_imp dal = new dal_imp())
            {
                return await Task.Run(() => dal.GetEvents());
            }
        }

        public Event GetEvent(Event event1)
        {
            using (dal_imp dal = new dal_imp())
                return dal.GetEvent(event1);
        }


        public void Dispose()
        {

        }
        public void addReportToEvent(int key, Report report) {
            using (dal_imp dal = new dal_imp())
                dal.addReportToEvent(key, report);
        }

    }
}

