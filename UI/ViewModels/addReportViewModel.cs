using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.command;
using UI.model;
using BE;
namespace UI.ViewModels
{
    public class AddReportViewModel:IReportViewModel, INotifyPropertyChanged
    {
        public AddReportCommand addReportCommand { set; get; }
        public MainModel CurentModel { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void AddReport(Report report)
        {
            CurentModel.addReport(report);
        }
        public AddReportViewModel()
        {
            CurentModel = new MainModel();
            addReportCommand = new AddReportCommand(this);
        }
    }
}
