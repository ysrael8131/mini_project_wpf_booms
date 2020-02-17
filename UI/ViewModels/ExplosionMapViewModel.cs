using BE;
using DevExpress.Xpf.Map;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using UI.model;

namespace UI.ViewModels
{
    public class ExplosionMapViewModel : IExplosionMapViewModel
    {
        public ObservableCollection<GeoPoint> boomim { get; set; }
        public MainModel currentModel { get; set; }
        public ObservableCollection<string> dates { get; set; }
        public ObservableCollection<Report> allBoomim { get; set; }
        public ObservableCollection<string> address { get; set; }
        public ObservableCollection<BoomLocation> allKMEANS { get; set; }
        public ObservableCollection<GeoPoint> kmeans { get; set; }

        public ExplosionMapViewModel()
        {
            currentModel = new MainModel();
            boomim = new ObservableCollection<GeoPoint>();
            dates = new ObservableCollection<string>(currentModel.dates);
            allBoomim = new ObservableCollection<Report>(currentModel.boomim);
            address = new ObservableCollection<string>();
            allKMEANS = new ObservableCollection<BoomLocation>(currentModel.kmeans);
            kmeans = new ObservableCollection<GeoPoint>();
            address.CollectionChanged += address_CollectionChanged;

        }

        public ObservableCollection<GeoPoint> collection(string time)
        {
            boomim.Clear(); ;
            address.Clear();
            foreach (var item in allBoomim)
            {
                if (item.date.ToShortDateString() == time)
                {
                    boomim.Add(new GeoPoint(item.Latitude, item.Longitude));
                    address.Add(item.address);
                }

            }
            return boomim;
        }
        public ObservableCollection<GeoPoint> collection2(string time)
        {
            foreach (var item in allKMEANS)
            {
                if (item.time.ToShortDateString() == time)
                {
                    kmeans.Add(new GeoPoint(item.addressLatitude, item.addresslongitude));
                }

            }
            return kmeans;
        }
        void address_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if(e.Action == NotifyCollectionChangedAction.Add)
            {
                var address1 = e.NewItems[0] as string;
                currentModel.addAddress(address1);
            }
        }

    }
}
