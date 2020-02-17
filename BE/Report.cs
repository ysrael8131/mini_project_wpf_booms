using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE
{
    public class Report
    {
        [Key]
        public int stamID { set; get; }
        [Required]
        public DateTime date { set; get; }
        [Required]
        public double Latitude { set; get; }
        [Required]
        public double Longitude { set; get; }
        public string name { set; get; }
        [Required]
        public int numBooms { set; get; }
        public Event @event { set; get; }
        public string address { set; get; }
        public override string ToString()
        {
            return date.ToString() + '\n' + name   +'\n' +Longitude + '\n' + Latitude+ '\n'+numBooms;
        }
    }
}
