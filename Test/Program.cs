using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS;
using System.Data.Entity;
using BE;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            DS.dataSource ds = new dataSource();
           Event ev= ds.events.Find(34);
            ds.boomlocations.Remove(ev.BoomLocations.First());
             ev.BoomLocations.Clear();
            ds.SaveChanges();
                }
    }
}
