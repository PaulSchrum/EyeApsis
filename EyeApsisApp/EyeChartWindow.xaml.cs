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

      private void ChartWindow_KeyUp(object sender, KeyEventArgs e)
      {
         if (e.Key == Key.Escape)
            Environment.Exit(0);
      }
   }
}
