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
   /// Interaction logic for SplashScreen.xaml
   /// </summary>
   public partial class SplashScreen : Window
   {
      public SplashScreen()
      {
         InitializeComponent();
         btn_splashAgreeProceed.Focus();
      }

      private void btn_splashExit_Click(object sender, RoutedEventArgs e)
      {
         Application.Current.Shutdown();
      }

      private void btn_splashAgreeProceed_Click(object sender, RoutedEventArgs e)
      {
         Application.Current.Shutdown();
      }
   }
}
