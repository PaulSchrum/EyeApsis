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

namespace EyeApsisApp
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
   {
      EyeChartWindow chartWindow;
      public MainWindow()
      {
         InitializeComponent();
         chartWindow = new EyeChartWindow();
      }

      protected bool IsSingleScreen
      {
         get 
         {
            return
               (Math.Abs(
               SystemParameters.PrimaryScreenWidth - SystemParameters.VirtualScreenWidth) < 2.0);
         }
         set { }
      }
      public void openChartWindowOnTheRightScreen()
      {
         ((Grid)chartWindow.Content).DataContext = ((Grid)this.Content).DataContext;
         chartWindow.DataContext = this.DataContext;

         if (this.IsSingleScreen == false)
         {
            if (SystemParameters.VirtualScreenLeft < 0.0)
            { // get the window onto the secondary screen (left screen)
               chartWindow.Left = SystemParameters.VirtualScreenLeft + 20;
            }
            else
            {   // get the window onto the secondary screen (right screen)
               chartWindow.Left =
                  SystemParameters.MaximizedPrimaryScreenWidth + 20;
            }
         }
         // else single screen, no changes neccessary to window locations.
         // it just works.

         chartWindow.Show();
         chartWindow.WindowState = System.Windows.WindowState.Maximized;
      }

      private void ControlWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
      {
         Environment.Exit(0);
      }

      private void ControlWindow_ContentRendered(object sender, EventArgs e)
      {
         openChartWindowOnTheRightScreen();
      }

      private void ControlWindow_KeyUp(object sender, KeyEventArgs e)
      {
         if (e.Key == Key.Escape)
            Environment.Exit(0);
      }
   }

}
