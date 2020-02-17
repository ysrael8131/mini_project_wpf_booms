using DevExpress.Xpf.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UI.ViewModels;

namespace UI.Views
{
    /// <summary>
    /// Interaction logic for explosionMap.xaml
    /// </summary>
    public partial class explosionMap : UserControl
    {
        public ExplosionMapViewModel curentVM { get; set; }
        public explosionMap()
        {
            InitializeComponent();
            curentVM = new ExplosionMapViewModel();
            this.DataContext = curentVM;
            
            
        }

        private void ComboBoxAdv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            provider.RequestPointsElevations(curentVM.collection(e.AddedItems[0].ToString()).ToList());

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(datim.SelectedItem != null)
            {
                provider.RequestPointsElevations(curentVM.collection2(datim.SelectedItem.ToString()).ToList());
                
            }

        }
    }
}
