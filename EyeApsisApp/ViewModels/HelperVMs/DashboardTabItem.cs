using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeApsisApp.ViewModels.HelperVMs
{
   public class DashboardTabItem : INotifyPropertyChanged
   {
      public EyeChartViewModel EyeChartVM { get; private set; }
      public DashboardTabItem()
      {
         TabItemName = "Base Tab Item";
      }

      private String tabItemName_;
      public String TabItemName
      {
         get { return tabItemName_; }
         set
         {
            tabItemName_ = value;
            RaisePropertyChanged("TabItemName");
         }
      }

      public event PropertyChangedEventHandler PropertyChanged;

      public void RaisePropertyChanged(String prop)
      {
         if (null != PropertyChanged)
         {
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
         }
      }

   }
}
