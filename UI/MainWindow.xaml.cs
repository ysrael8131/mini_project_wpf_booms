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
using UI.Views;
using BE;
namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window  
    {
        public MainWindow()
        {
            InitializeComponent();
         

            
        }

        private void addReportwclick(object sender, MouseButtonEventArgs e)
        {
            AddReport addReport = new AddReport();
            this.page.Content = addReport;
        }

        private void explosionMapWclick(object sender, MouseButtonEventArgs e)
        {
            explosionMap explosionMap = new explosionMap();
            this.page.Content = explosionMap;
        }

      

        private void statistics_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
