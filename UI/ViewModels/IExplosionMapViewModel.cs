using BE;
using DevExpress.Xpf.Map;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.model;

namespace UI.ViewModels
{
    public interface IExplosionMapViewModel
    {
        ObservableCollection<GeoPoint> boomim { get; set; }
        ObservableCollection<Report> allBoomim { get; set; }
        ObservableCollection<string> dates { get; set; }
        ObservableCollection<string> address { get; set; }
        ObservableCollection<BoomLocation> allKMEANS { get; set; }
        ObservableCollection<GeoPoint> kmeans { get; set; }

        MainModel currentModel { get; set; }

    }
}
