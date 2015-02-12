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
      private App thisApp { get; set; }
      public CalibrateScreenSizeWindow()
      {
         InitializeComponent();
         thisApp = (App)Application.Current;
      }

      private void ScreenAdjustmentFactor_TextChanged(object sender, TextChangedEventArgs e)
      {

      }

      private void btn_resetTo1_Click(object sender, RoutedEventArgs e)
      {
         var viewModel = this.grd_mainGrid.DataContext as EyeChartViewModel;
         viewModel.VerticalCalibration.AdjustmentMultiplier = 1.0;
      }

      private void btn_ExitEyeApsis_Click(object sender, RoutedEventArgs e)
      {
         thisApp.Shutdown();
      }

      private void calibrateScreenSize_Closed(object sender, EventArgs e)
      {
         thisApp.Shutdown();
      }

      private void ScreenAdjustmentFactor_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
      {
         if (!(sender is TextBox)) return;

         TextBox thisTextBox = (TextBox)sender;
         selectAllText(thisTextBox);
      }

      private void selectAllText(TextBox aTextBox)
      {
         aTextBox.SelectAll();
      }

   }
}
