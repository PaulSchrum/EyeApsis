using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeApsisApp
{
   public class DashboardViewModel : INotifyPropertyChanged
   {
      public VisionTestRecord CurrentTest { get; set; }
      public EyeChartViewModel EyeChartViewModel { get; set; }

      public DashboardViewModel()
      {
         InitializeNewTest();
      }

      internal void InitializeNewTest()
      {
         CurrentTest = new VisionTestRecord();
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
