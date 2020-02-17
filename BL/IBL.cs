using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface IBL
    {
        void addReport(Report report);
        void deleteReport(Report report);
        void updateReport(Report report);


        void addBoomLocation(BoomLocation boomLocation);
        void deleteBoomLocation(BoomLocation boomLocation);
        void updateBoomLocation(BoomLocation boomLocation);

        void addEvent(Event event1);
        void deleteEvent(Event event1);
        void updateEvent(int key,Report report,List<BoomLocation> boomLocations);

        void addReportToEvent(int key, Report report);


        IEnumerable<BoomLocation> getListBoomLocation();
        IEnumerable<Report> getListReports();
        Task<List<Event>> GetEvents();

        BoomLocation GetBoomLocation(BoomLocation boomLocation);
        Report GetReport(Report Report);
        Event GetEvent(Event event1);
    }
}
