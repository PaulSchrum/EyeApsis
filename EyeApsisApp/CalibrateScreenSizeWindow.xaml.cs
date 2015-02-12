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
using System.Windows.Shapes;

namespace EyeApsisApp
{
   /// <summary>
   /// Interaction logic for CalibrateScreenSize.xaml
   /// </summary>
   public partial class CalibrateScreenSizeWindow : Window
   {
      public CalibrateScreenSizeWindow()
      {
         InitializeComponent();
      }

      private void ScreenAdjustmentFactor_TextChanged(object sender, TextChangedEventArgs e)
      {

      }

      private void btn_resetTo1_Click(object sender, RoutedEventArgs e)
      {
         var viewModel = this.grd_mainGrid.DataContext as EyeChartViewModel;
         viewModel.VerticalCalibration.AdjustmentMultiplier = 1.0;
      }
   }
}
