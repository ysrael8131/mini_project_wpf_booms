using BE;
using DS;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class dal_imp : IDAL, IDisposable
    {
        dataSource ds;
        public dal_imp()
        {
            ds = new dataSource();
        }

        public async void addBoomLocation(BoomLocation boomLocation)
        {

            ds.boomlocations.Add(boomLocation);
            await ds.SaveChangesAsync();


        }

        public async void addEvent(Event event1)
        {

            ds.events.Add(event1);
            await ds.SaveChangesAsync();



        }

        public async void addReport(Report report)
        {
            using (var ds = new dataSource())
            {
                ds.reports.Add(report);
                await ds.SaveChangesAsync();
            }

        }

        public void deleteBoomLocation(BoomLocation boomLocation)
        {
            using (var ds = new dataSource())
            {
                ds.Configuration.ValidateOnSaveEnabled = false;
                ds.boomlocations.Attach(boomLocation);
                ds.Entry(boomLocation).State = EntityState.Deleted;
                ds.SaveChanges();
            }

        }

        public void deleteEvent(Event event1)
        {
            throw new NotImplementedException();
        }

        public void deleteReport(Report report)
        {
            using (var ds = new dataSource())
            {
                ds.reports.Remove(report);
                ds.SaveChanges();
            }


        }

        public void Dispose()
        {
        }

        public BoomLocation GetBoomLocation(BoomLocation boomLocation)
        {
            using (var ds = new dataSource())
                return ds.boomlocations.Find(boomLocation);
        }

        public Event GetEvent(Event event1)
        {
            using (var ds = new dataSource())
                return ds.events.Find(event1.NumEvent);
        }

        public async Task<List<Event>> GetEvents()
        {

            return await (from d in ds.events select d).ToListAsync();
        }

        public IEnumerable<BoomLocation> getListBoomLocation()
        {
            using (var ds = new dataSource())
                return ds.boomlocations.ToList();
        }

        public IEnumerable<Report> getListReports()
        {
            using (var ds = new dataSource())
                return ds.reports.ToList();
        }

        public Report GetReport(Report report)
        {
            using (var ds = new dataSource())
                return ds.reports.Find(report);
        }

        public void updateBoomLocation(BoomLocation boomLocation)
        {
            throw new NotImplementedException();
        }

        public async void  updateEvent(int key, Report report, List<BoomLocation> boomLocations)
        {

            if (ds.events.Find(key) != null)
            {
                Event event2 = ds.events.Find(key);
                ds.boomlocations.RemoveRange(event2.BoomLocations);
                Report re = report;
                report.@event = event2;
                event2.reports.Add(re);

                foreach(var item in boomLocations)
                {
                    item.@event = event2;
                    event2.BoomLocations.Add(item);
                }
                event2.numbooms += report.numBooms;

                await ds.SaveChangesAsync();

            }
            else
            {
                throw new Exception("no event");
            }



        }

        public void updateReport(Report report)
        {
            throw new NotImplementedException();
        }
        public void addReportToEvent(int key, Report report)
        {
            using (var ds = new dataSource())
            {
                Event ev = ds.events.Find(key);
                ev.reports.Add(report);
                ds.SaveChanges();
            }

        }

    }
}
