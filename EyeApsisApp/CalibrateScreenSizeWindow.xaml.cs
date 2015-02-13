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
      internal Double adjFactr_Screen1 { get; set; }
      internal Double adjFactr_Screen2 { get; set; }
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
         saveVerticalMultiplier();
         thisApp.Shutdown();
      }

      private void calibrateScreenSize_Closed(object sender, EventArgs e)
      {
         saveVerticalMultiplier();
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

      protected void saveVerticalMultiplier()
      {
         //if (Properties.Settings.Default.VerticalAdjustmentFactor !=
         //      this.ScreenAdjustmentFactor.Text)
         {
            Double vertAdj = Convert.ToDouble(this.ScreenAdjustmentFactor.Text);
            if (vertAdj > 2.5) vertAdj = 2.5;
            if (vertAdj < 0.05) vertAdj = 0.05;
            String vertAdjStr = String.Format("{0:0.00}", vertAdj);
            //Properties.Settings.Default.VerticalAdjustmentFactor =
            //   vertAdjStr;
            //Properties.Settings.Default.Save();
         }
      }

      private void stx_AdjustmentFactor_MouseWheel(object sender, MouseWheelEventArgs e)
      {
         var delta = e.Delta;
         Double adder = 0.01;
         if (delta < 0) adder = -0.01;
         this.ScreenAdjustmentFactor.Text =
            ((Convert.ToDouble(this.ScreenAdjustmentFactor.Text)) + adder)
            .ToString();
      }

      private void calibrateScreenSize_SourceInitialized(object sender, EventArgs e)
      {
         var viewModel = this.grd_mainGrid.DataContext as EyeChartViewModel;
         try
         {
            adjFactr_Screen1 =
               ((Double)Properties.Settings.Default.AdjustmentMultiplier_Screen1) / 100.0;
            adjFactr_Screen2 =
               ((Double)Properties.Settings.Default.AdjustmentMultiplier_Screen2) / 100.0;
         }
         catch
         {
            adjFactr_Screen1 = adjFactr_Screen2 = 1.0;
         }
      }

      private void btn_AcceptProceed_Click(object sender, RoutedEventArgs e)
      {
         int adjFactor;
         var viewModel = this.grd_mainGrid.DataContext as EyeChartViewModel;
         adjFactor = (int)(viewModel.VerticalCalibration.AdjustmentMultiplier * 100);
         if(thisApp.testingScreenNumber == 2)
            Properties.Settings.Default.AdjustmentMultiplier_Screen2 = adjFactor;
         else
            Properties.Settings.Default.AdjustmentMultiplier_Screen1 = adjFactor;
         Properties.Settings.Default.Save();
      }

   }
}
