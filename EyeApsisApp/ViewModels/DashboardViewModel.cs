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
      public ObservableCollection<String> VisualAcuityStates { get; set; }
      public ObservableCollection<VisualAcuityRow> AvailableVisualAcuities { get; private set; }
      private int selectedVizAcuityIndex_=0;
      public int SelectedVisualAcuityIndex
      {
         get { return selectedVizAcuityIndex_; }
         set
         {
            if (selectedVizAcuityIndex_ == value) return;
            selectedVizAcuityIndex_ = value;
            RaisePropertyChanged("SelectedVisualAcuityIndex");
            if (value >= 0 && value < AvailableVisualAcuities.Count)
            {
               SelectedVisualAcuity = AvailableVisualAcuities[value];
            }
         }
      }

      public DashboardViewModel()
      {
         InitializeNewTest();
         AvailableVisualAcuities = new ObservableCollection<VisualAcuityRow>();
         VisualAcuityRow.CurrentState = VisualAcuityState.foot;
         populateVisualAcuities();
         populateVisualAcuityStates();
      }

      private void populateVisualAcuityStates()
      {
         VisualAcuityStates = new ObservableCollection<string>
            {"Foot", "Meter", "LogMAR", "Decimal"};
      }

      public void VAstateSelectionChanged(String newValue)
      {
         VisualAcuityRow.CurrentState = newValue.ToVisualAcuityState();
         AvailableVisualAcuities.Clear();
         populateVisualAcuities();
      }

      internal void InitializeNewTest()
      {
         CurrentTest = new VisionTestRecord();
      }

      private void populateVisualAcuities()
      {
         foreach (var row in AcuityListAssociation.AvailableVisualAcuities)
            AvailableVisualAcuities.Add(row);

         SelectedVisualAcuity = AvailableVisualAcuities[AvailableVisualAcuities.Count / 2];
      }

      private VisualAcuityRow selectedVisualAcuity_;
      private int vaSentry = 0;
      public VisualAcuityRow SelectedVisualAcuity
      {
         get { return selectedVisualAcuity_; }
         private set
         {
            if (0 == vaSentry) vaSentry = 1;
            selectedVisualAcuity_ = value;
            RaisePropertyChanged("SelectedVisualAcuity");
         }
      }

      internal void VisualAcuitySelectionChanged(IEnumerable<VisualAcuityRow> selection)
      {
         eyeChartViewModel_.SetEyeChartLines(selection);
         if (null == selection)
            this.SelectedVisualAcuity = AvailableVisualAcuities[14 / 2];
         else if (selection.Count() != 1)
            this.SelectedVisualAcuity = AvailableVisualAcuities[14 / 2];
         else if (vaSentry == 1)
            vaSentry = 0;
         else
            this.SelectedVisualAcuity = selection.First();

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
