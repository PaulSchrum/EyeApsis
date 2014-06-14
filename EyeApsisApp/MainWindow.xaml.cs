using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
         chartWindow.mainWindow = this;
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
            chartWindow.Topmost = true;
         }
         else //single screen, no changes neccessary to window location
         {  // but lets move it a little anyway so it is not on top
            // of the main window
            chartWindow.Left = this.Left + 96;
            chartWindow.Topmost = false;
            this.Topmost = true;
         }

         chartWindow.Show();
         chartWindow.WindowState = System.Windows.WindowState.Maximized;
         this.Show();
         this.BringIntoView();
         
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
         if (Properties.Settings.Default.VerticalAdjustmentFactor != 
               this.VerticalMultiplierText.Text)
         {
            Double vertAdj = Convert.ToDouble(this.VerticalMultiplierText.Text);
            if (vertAdj > 2.5) vertAdj = 2.5;
            if (vertAdj < 0.2) vertAdj = 0.2;
            String vertAdjStr = String.Format("{0:0.00}", vertAdj);
            Properties.Settings.Default.VerticalAdjustmentFactor =
               vertAdjStr;
            Properties.Settings.Default.Save();
         }
      }

      private void txt_TextOpacity_MouseWheel(object sender, MouseWheelEventArgs e)
      {
         Double textOpacity = Convert.ToDouble(this.txt_TextOpacity.Text);
         var delta = e.Delta;
         Double adder = 0.1;
         if (textOpacity < 0.4) adder = 0.01;
         if (textOpacity < 0.08) adder = 0.001;
         if (delta < 0) adder *= -1;
         Double result = textOpacity + adder;
         if (result > 1) result = 1;
         if (result < 0.001) result = 0.001;
         this.txt_TextOpacity.Text = result.ToString();
      }


      readonly protected Double doubleClickTime = 0.3; // seconds
      protected Timer doubleClickTimer;
      protected bool doubleClickPending = false;
      private void txt_TextOpacity_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
      {
         //System.Diagnostics.Debug.Print("PreviewMouseLeftButtonDown dcPending: " + doubleClickPending.ToString());
         if (true == doubleClickPending)
         {
            Double val = 1.0;
            this.txt_TextOpacity.Text = val.ToString();
            doubleClickTimer.Dispose();
            doubleClickTimer = null;
         }
         doubleClickPending = false;
      }

      private void txt_TextOpacity_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
      {
         doubleClickPending = true;
         doubleClickTimer = new Timer(1000 * doubleClickTime);
         doubleClickTimer.Elapsed += new ElapsedEventHandler((source, ev) => deactivatePendingDoubleClick());
         doubleClickTimer.Start();
      }

      protected void deactivatePendingDoubleClick()
      { 
         doubleClickPending = false;
         //if (null != left)
         doubleClickTimer.Dispose();
         doubleClickTimer = null;
      }

      private void txt_TextOpacity_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
      {
         if (true == doubleClickPending)
         {
            Double val = 0.0;
            this.txt_TextOpacity.Text = val.ToString();
            doubleClickTimer.Dispose();
            doubleClickTimer = null;
         }
         doubleClickPending = false;
      }

      private void txt_TextOpacity_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
      {
         doubleClickPending = true;
         doubleClickTimer = new Timer(1000 * doubleClickTime);
         doubleClickTimer.Elapsed += new ElapsedEventHandler((source, ev) => deactivatePendingDoubleClick());
         doubleClickTimer.Start();
      }

      private void txt_BackgroundGrayscale_MouseWheel(object sender, MouseWheelEventArgs e)
      {
         Double textBackgroundGscale = Convert.ToDouble(this.txt_BackgroundGrayscale.Text);
         var delta = e.Delta;
         Double adder = 5;
         //if (textBackgroundGscale < 0.4) adder = 0.01;
         //if (textBackgroundGscale < 0.08) adder = 0.001;
         if (delta < 0) adder *= -1;
         Double result = textBackgroundGscale + adder;
         if (result > 100) result = 100;
         if (result < 0) result = 0;
         this.txt_BackgroundGrayscale.Text = result.ToString();
      }

      private void txt_BackgroundGrayscale_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
      {
         if (true == doubleClickPending)
         {
            Double val = 100.0;
            this.txt_BackgroundGrayscale.Text = val.ToString();
            doubleClickTimer.Dispose();
            doubleClickTimer = null;
         }
         doubleClickPending = false;
      }

      private void txt_BackgroundGrayscale_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
      {
         doubleClickPending = true;
         doubleClickTimer = new Timer(1000 * doubleClickTime);
         doubleClickTimer.Elapsed += new ElapsedEventHandler((source, ev) => deactivatePendingDoubleClick());
         doubleClickTimer.Start();
      }

      private void txt_BackgroundGrayscale_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
      {
         if (true == doubleClickPending)
         {
            Double val = 0.0;
            this.txt_BackgroundGrayscale.Text = val.ToString();
            doubleClickTimer.Dispose();
            doubleClickTimer = null;
         }
         doubleClickPending = false;
      }

      private void txt_BackgroundGrayscale_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
      {
         doubleClickPending = true;
         doubleClickTimer = new Timer(1000 * doubleClickTime);
         doubleClickTimer.Elapsed += new ElapsedEventHandler((source, ev) => deactivatePendingDoubleClick());
         doubleClickTimer.Start();
      }

   }

}
