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
         var visualAcuitySelection = lbx_VisualAcuity.SelectedIndex;
         var selection = e.AddedItems.Cast<VisualAcuityRow>();
         this.dataContxt.VisualAcuitySelectionChanged(selection);
         lbx_VisualAcuity.SelectedIndex = visualAcuitySelection;
         lbx_VisualAcuity.ScrollIntoView(lbx_VisualAcuity.SelectedItem);
      }

      private void TopLevelGrid_MouseWheel(object sender, MouseWheelEventArgs e)
      {
         moveListviewSelction(e.Delta);
      }

      private void moveListviewSelction(int delta)
      {
         int i = 1;
         if (delta > 0)
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

      private void lbx_VisualAcuity_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
      {
         moveListviewSelction(e.Delta);
         e.Handled = true;
      }

      private void cbx_visualAcuityState_SelectionChanged(object sender, 
         SelectionChangedEventArgs e)
      {
         if (null == dataContxt) return;
         dataContxt.VAstateSelectionChanged(e.AddedItems[0] as String);
      }

      private void TopLevelGrid_KeyDown(object sender, KeyEventArgs e)
      {
      }

      private void TopLevelGrid_PreviewKeyDown(object sender, KeyEventArgs e)
      {
         switch (e.Key)
         {
            case Key.Down: { moveListviewSelction(-1); break; }
            case Key.Up: { moveListviewSelction(1); break; }
         }
      }

      private void EndUserControlDashboard_Loaded(object sender, RoutedEventArgs e)
      {
         txt_subjectDistance.Focus();
      }
   }
}
