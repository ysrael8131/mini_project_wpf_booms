using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using BL;
namespace UI.model
{
    public class MainModel
    {
     public List<Report> boomim { get; set; }
        public List<BoomLocation> kmeans { get; set; }
        public List<string> dates { get; set; }
        public List<string> address { get; set; }
        public void addReport(Report report)
        {
            using (bl_imp bl=new bl_imp() )
            {
                bl.addReport(report);
            }
        }
        public MainModel()
        {
            using (bl_imp bl = new bl_imp())
            {
                boomim = bl.getListReports().ToList();
                kmeans = bl.getListBoomLocation().ToList();
                dates = (from item in boomim select item.date.Date.ToShortDateString()).Distinct().ToList();
                address = new List<string>();
            }
               
        }
        public void addAddress(String newAddress)
        {
            address.Add(newAddress);
        }
    }
}
