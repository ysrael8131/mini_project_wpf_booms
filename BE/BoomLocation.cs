using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BoomLocation
    {
        [Key]
        public int keyboom { set; get; }
        [Required]
        public double addressLatitude { set; get; }
        [Required]
        public double addresslongitude { set; get; }
        public bool update { set; get; }
        [Required]
        public DateTime time { set; get; }
        public Event @event { set; get; }
        public string address { set; get; }


    }
}
