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
   /// Interaction logic for UserControlWindow.xaml
   /// </summary>
   public partial class EndUserDashboardWindow : Window
   {
      private App thisApp { get; set; }
      public EndUserDashboardWindow()
      {
         InitializeComponent();
         thisApp = (App)Application.Current;
      }

      private void userControlWindow_Closed(object sender, EventArgs e)
      {
         thisApp.Shutdown();
      }
   }
}
