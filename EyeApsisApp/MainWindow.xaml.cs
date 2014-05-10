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

      public void openChartWindowOnTheRightScreen()
      {
         ((Grid) chartWindow.Content).DataContext = ((Grid)this.Content).DataContext;
         chartWindow.DataContext = this.DataContext;
         chartWindow.Left = this.Left + 16 * 96;  // get the window onto the right screen
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
