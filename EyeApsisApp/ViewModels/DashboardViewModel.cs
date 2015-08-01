using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EyeApsisApp.ViewModels.HelperVMs;
using System.Collections.ObjectModel;
using EyeApsisApp.Models.Chart;
using System.Collections;

namespace EyeApsisApp
{
   public class DashboardViewModel : INotifyPropertyChanged
   {
      public VisionTestRecord CurrentTest { get; set; }
      public List<DashboardTabItem> TopLevelTabItems { get; set; }
      public ObservableCollection<VisualAcuityRow> AvailableVisualAcuities { get; private set; }
      public ObservableCollection<VisualAcuityRow> SelectedVisualAcuity { get; private set; }

      public DashboardViewModel()
      {
         InitializeNewTest();
         AvailableVisualAcuities = new ObservableCollection<VisualAcuityRow>();
         SelectedVisualAcuity = new ObservableCollection<VisualAcuityRow>();
         VisualAcuityRow.CurrentState = VisualAcuityState.logMAR;
         populateVisualAcuities();
      }

      internal void InitializeNewTest()
      {
         CurrentTest = new VisionTestRecord();
      }

      // These values are taken from
      //    https://en.wikipedia.org/wiki/LogMAR_chart
      private void populateVisualAcuities()
      {
         AvailableVisualAcuities.Add(
            new VisualAcuityRow(200.0, "20/200", "6/60", "0.10", "1.00"));
         AvailableVisualAcuities.Add(
            new VisualAcuityRow(160.0, "20/160", "6/48", "0.125", "0.90"));
         AvailableVisualAcuities.Add(
            new VisualAcuityRow(125.0, "20/125", "6/38", "0.16", "0.80"));
         AvailableVisualAcuities.Add(
            new VisualAcuityRow(100.0, "20/100", "6/30", "0.20", "0.70"));
         AvailableVisualAcuities.Add(
            new VisualAcuityRow(80.0, "20/80", "6/24", "0.25", "0.60"));
         AvailableVisualAcuities.Add(
            new VisualAcuityRow(63.0, "20/63", "6/19", "0.32", "0.50"));
         AvailableVisualAcuities.Add(
            new VisualAcuityRow(50.0, "20/50", "6/15", "0.40", "0.40"));
         AvailableVisualAcuities.Add(
            new VisualAcuityRow(40.0, "20/40", "6/12", "0.50", "0.30"));
         AvailableVisualAcuities.Add(
            new VisualAcuityRow(32.0, "20/32", "6/9.5", "0.63", "0.20"));
         AvailableVisualAcuities.Add(
            new VisualAcuityRow(25.0, "20/25", "6/7.5", "0.80", "0.10"));
         AvailableVisualAcuities.Add(
            new VisualAcuityRow(20.0, "20/20", "6/6", "1.00", "0.00"));
         AvailableVisualAcuities.Add(
            new VisualAcuityRow(16.0, "20/16", "6/4.8", "1.25", "-0.10"));
         AvailableVisualAcuities.Add(
            new VisualAcuityRow(12.5, "20/12.5", "6/3.8", "1.60", "-0.20"));
         AvailableVisualAcuities.Add(
            new VisualAcuityRow(10.0, "20/10", "6/3", "2.00", "-0.30"));
      }

      internal void VisualAcuitySelectionChanged(IEnumerable<VisualAcuityRow> selection)
      {
         eyeChartViewModel_.SetEyeChartLines(selection);
      }

      private EyeChartViewModel eyeChartViewModel_;
      public EyeChartViewModel EyeChartViewModel
      {
         get { return eyeChartViewModel_; }
         set
         {
            eyeChartViewModel_ = value;
            SubjectDistance = eyeChartViewModel_.SubjectDistance;
         }
      }

      private Double subjectDistance_;
      public Double SubjectDistance
      {
         get { return subjectDistance_; }
         set
         {
            subjectDistance_ = value;
            RaisePropertyChanged("SubjectDistance");
            EyeChartViewModel.SubjectDistance = subjectDistance_;
         }
      }

      public event PropertyChangedEventHandler PropertyChanged;
      public void RaisePropertyChanged(String prop)
      {
         if (null != PropertyChanged)
         {
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
         }
      }

   }
}
