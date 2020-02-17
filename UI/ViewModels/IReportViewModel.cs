using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.model;
using BE;
namespace UI.ViewModels
{
    public interface IReportViewModel
    {
        MainModel CurentModel { set; get; }
        void AddReport(Report report);

    }
}
