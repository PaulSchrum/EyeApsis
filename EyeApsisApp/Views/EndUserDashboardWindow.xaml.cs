using EyeApsisApp.Models.Chart;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
      private DashboardViewModel dataContxt { get; set; }
      private App thisApp { get; set; }
      public EndUserDashboardWindow()
      {
         InitializeComponent();
         thisApp = (App)Application.Current;
         dataContxt = (DashboardViewModel)this.TopLevelGrid.DataContext;
      }

      private void userControlWindow_Closed(object sender, EventArgs e)
      {
         thisApp.Shutdown();
      }

      public void InitializeNewTest()
      {
         ((DashboardViewModel)this.TopLevelGrid.DataContext).InitializeNewTest();
      }

      private void tempButton_Click(object sender, RoutedEventArgs e)
      {
         
      }

      private void txt_subjectDistance_GotFocus(object sender, RoutedEventArgs e)
      {

      }

      private void lbx_VisualAcuity_SelectionChanged(
         object sender, SelectionChangedEventArgs e)
      {
         var selection = e.AddedItems.Cast<VisualAcuityRow>();
         this.dataContxt.VisualAcuitySelectionChanged(selection);
      }

      private void TopLevelGrid_MouseWheel(object sender, MouseWheelEventArgs e)
      {
         int i = 1;
         if (e.Delta > 0)
            i *= -1;

         var newIndex = this.lbx_VisualAcuity.SelectedIndex + i;
         if (newIndex < 0) newIndex = 0;
         if (newIndex > lbx_VisualAcuity.Items.Count - 1)
            newIndex = lbx_VisualAcuity.Items.Count - 1;
         lbx_VisualAcuity.SelectedIndex = newIndex;
         lbx_VisualAcuity.ScrollIntoView(lbx_VisualAcuity.SelectedItem);
      }

      private void lbx_VisualAcuity_Loaded(object sender, RoutedEventArgs e)
      {
         lbx_VisualAcuity.SelectedIndex = 7;
         lbx_VisualAcuity.ScrollIntoView(lbx_VisualAcuity.SelectedItem);
      }
   }
}
