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
   /// Interaction logic for EyeChartWindow.xaml
   /// </summary>
   public partial class EyeChartWindow : Window
   {
      public EyeChartWindow()
      {
         InitializeComponent();
      }

      public MainWindow mainWindow { get; set; }

      private void ChartWindow_KeyUp(object sender, KeyEventArgs e)
      {
         if (e.Key == Key.Escape)
            Environment.Exit(0);
         Type t = sender.GetType();
      }

      private bool thisWindowShouldBeTopmost_;
      private bool ThisWindowShouldBeTopmost
      {
         get { return thisWindowShouldBeTopmost_; }
         set
         {
            if (thisWindowShouldBeTopmost_ == value) return;
            thisWindowShouldBeTopmost_ = value;
            if (true == thisWindowShouldBeTopmost_)
            {
               //this.mainWindow.Topmost = false;
               this.Topmost = true;
            }
            else
            {
               this.Topmost = false;
               //this.mainWindow.Topmost = true;
            }
         }
      }

      private void ChartWindow_MouseMove(object sender, MouseEventArgs e)
      {
         if ((e.GetPosition(this).X > this.ActualWidth/2.0))
            this.ThisWindowShouldBeTopmost = true;
         else
            this.ThisWindowShouldBeTopmost = false;
      }

   }

   public class ColorToSolidColorBrushValueConverter : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
      {
         if (null == value)
         {
            return null;
         }
         // For a more sophisticated converter, check also the targetType and react accordingly..
         if (value is Color)
         {
            Color color = (Color)value;
            return new SolidColorBrush(color);
         }
         else return null;
      }

      public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
      {
         return null;
      }
   }
}
