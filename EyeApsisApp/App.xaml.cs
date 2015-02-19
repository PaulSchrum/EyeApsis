using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EyeApsisApp
{
   /// <summary>
   /// Interaction logic for App.xaml
   /// </summary>
   public partial class App : Application
   {
      public EyeChartViewModel chartVM { get; private set; }
      public EyeChartWindow chartWindow { get; private set; }
      public CalibrateScreenSizeWindow calibrateScreenSizeWindow { get; private set; }
      public int testingScreenNumber;
      public EndUserDashboardWindow dashboardWindow { get; private set; }

      public void Step2_UserAgreesSoOpenCalibrationWindow()
      {
         testingScreenNumber = 2;
         chartVM = new EyeChartViewModel();
         chartWindow = new EyeChartWindow();
         calibrateScreenSizeWindow = new CalibrateScreenSizeWindow();
         openChartWindowOnTheCorrectScreen();
      }

      public void Step3_CalibrationAccepted_OpenDashboard()
      {
         chartVM.InCalibrationMode = false;
         dashboardWindow = new EndUserDashboardWindow();
         ((DashboardViewModel)dashboardWindow.TopLevelGrid.DataContext).EyeChartViewModel = chartVM;
         dashboardWindow.Show();
      }

      private void openChartWindowOnTheCorrectScreen()
      {
         chartVM.InCalibrationMode = true;
         Calibration calibration = chartVM.VerticalCalibration;
         ((Grid)chartWindow.Content).DataContext = chartVM;
         ((Grid)calibrateScreenSizeWindow.Content).DataContext = chartVM;
         calibrateScreenSizeWindow.Show();
         chartWindow.DataContext = chartVM;

         if (this.IsSingleScreen == false)
         {
            testingScreenNumber = 2;
            calibration.AdjustmentMultiplier = calibrateScreenSizeWindow.adjFactr_Screen2;
            if (SystemParameters.VirtualScreenLeft < 0.0)
            { // get the window onto the secondary screen (left screen)
               chartWindow.Left = SystemParameters.VirtualScreenLeft + 20;
            }
            else
            {   // get the window onto the secondary screen (right screen)
               chartWindow.Left =
                  SystemParameters.MaximizedPrimaryScreenWidth + 20;
            }
            chartWindow.Topmost = true;
         }
         else //single screen, no changes neccessary to window location
         {  // but lets move it a little anyway so it is not on top
            // of the main window
            testingScreenNumber = 1;
            calibration.AdjustmentMultiplier = calibrateScreenSizeWindow.adjFactr_Screen1;
            chartWindow.Left = calibrateScreenSizeWindow.Left + 96;
            chartWindow.Topmost = false;
            calibrateScreenSizeWindow.Topmost = true;
         }
         chartVM.VerticalCalibration = calibration;

         chartWindow.Show();
         chartWindow.WindowState = System.Windows.WindowState.Maximized;
         calibrateScreenSizeWindow.Focus();
         calibrateScreenSizeWindow.btn_AcceptProceed.Focus();
      }

      protected bool IsSingleScreen
      {
         get
         {
            return
               (Math.Abs(
               SystemParameters.PrimaryScreenWidth - SystemParameters.VirtualScreenWidth) < 2.0);
         }
         private set { }
      }

   }
}
