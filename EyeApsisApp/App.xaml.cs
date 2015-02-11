﻿using System;
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

      public void Step2_UserAgreesSoOpenCalibrationWindow()
      {
         chartVM = new EyeChartViewModel();
         chartWindow = new EyeChartWindow();
         openChartWindowOnTheCorrectScreen();
      }

      public void openChartWindowOnTheCorrectScreen()
      {
         ((Grid)chartWindow.Content).DataContext = chartVM;
      //   chartWindow.DataContext = this.DataContext;
      //   chartVM.VerticalCalibration.AdjustmentMultiplier =
      //      Convert.ToDouble(Properties.Settings.Default.VerticalAdjustmentFactor);

      //   if (this.IsSingleScreen == false)
      //   {
      //      if (SystemParameters.VirtualScreenLeft < 0.0)
      //      { // get the window onto the secondary screen (left screen)
      //         chartWindow.Left = SystemParameters.VirtualScreenLeft + 20;
      //      }
      //      else
      //      {   // get the window onto the secondary screen (right screen)
      //         chartWindow.Left =
      //            SystemParameters.MaximizedPrimaryScreenWidth + 20;
      //      }
      //      chartWindow.Topmost = true;
      //   }
      //   else //single screen, no changes neccessary to window location
      //   {  // but lets move it a little anyway so it is not on top
      //      // of the main window
      //      chartWindow.Left = this.Left + 96;
      //      chartWindow.Topmost = false;
      //      this.Topmost = true;
      //   }


      }

   }
}
