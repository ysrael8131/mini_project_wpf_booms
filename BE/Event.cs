using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Event
    {
        public Event()
        {
        }
        [Key]
        public int NumEvent { set; get; }
        [Required]
        public DateTime start { set; get; }
        [Required]
        public int numbooms { set; get; }
        public virtual ICollection<BoomLocation> BoomLocations { get; set; }
        public virtual ICollection<Report> reports { get; set; }
       
    }
}
