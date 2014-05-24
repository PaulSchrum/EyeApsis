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
         EyeChartViewModel vm = (((Grid)this.Content).DataContext) as EyeChartViewModel;
         ((Grid)chartWindow.Content).DataContext = ((Grid)this.Content).DataContext;
         chartWindow.DataContext = this.DataContext;
         vm.VerticalCalibration.AdjustmentMultiplier = 
            Convert.ToDouble(Properties.Settings.Default.VerticalAdjustmentFactor);

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
         saveVerticalMultiplier();
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

      private void TextBox_MouseWheel_1(object sender, MouseWheelEventArgs e)
      {
         var delta = e.Delta;
         Double adder = 0.01;
         if (delta < 0) adder = -0.01;
         this.VerticalMultiplierText.Text = ((Convert.ToDouble(this.VerticalMultiplierText.Text)) + adder).ToString();
      }

      private void Expander_Collapsed(object sender, RoutedEventArgs e)
      {
         if (((Expander)sender).Name == "CalibrationExpander")
         {
            saveVerticalMultiplier();
         }
      }

      protected void saveVerticalMultiplier()
      {
         if (Properties.Settings.Default.VerticalAdjustmentFactor != this.VerticalMultiplierText.Text)
         {
            Properties.Settings.Default.VerticalAdjustmentFactor = this.VerticalMultiplierText.Text;
            Properties.Settings.Default.Save();
         }
      }

      private void txt_TextOpacity_MouseWheel(object sender, MouseWheelEventArgs e)
      {
         Double textOpacity = Convert.ToDouble(this.txt_TextOpacity.Text);
         var delta = e.Delta;
         Double adder = 0.1;
         if (textOpacity < 0.4) adder = 0.01;
         if (textOpacity < 0.12) adder = 0.001;
         if (delta < 0) adder *= -1;
         Double result = textOpacity + adder;
         if (result > 1) result = 1;
         if (result < 0.001) result = 0.001;
         this.txt_TextOpacity.Text = result.ToString();
      }

   }

}
