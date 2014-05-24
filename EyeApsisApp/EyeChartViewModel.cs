using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows;
using System.Windows.Data;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Timers;

namespace EyeApsisApp
{
   public class EyeChartViewModel : INotifyPropertyChanged, IDisposable
   {
      public EyeChartViewModel()
      {
         this.SubjectDistance = 20;
         ChartLines = new ObservableCollection<ChartLine>();
         //ChartLines.Add(new ChartLine(initialSnellenDenominator: 100.0));
         ChartLines.Add(new ChartLine(initialSnellenDenominator: 60.0));
         ChartLines.Add(new ChartLine(initialSnellenDenominator: 40.0));
         ChartLines.Add(new ChartLine(initialSnellenDenominator: 30.0));
         ChartLines.Add(new ChartLine(initialSnellenDenominator: 20.0));
         ChartLines.Add(new ChartLine(initialSnellenDenominator: 15.0));
         ChartLines.Add(new ChartLine(initialSnellenDenominator: 10.0));
         updateAllChartLines();
         LeftBackgroundBrush = Brushes.White;
         RightBackgroundBrush = Brushes.White;
         TextForegroundBrush = Brushes.Black;

         ToggleBackgroundCmd = new RelayCommand(toggleBicolorBackground, () => canToggleBackground);
         ShuffleLettersCmd = new RelayCommand(shuffleLetters, () => canShuffleLetters);
         reshuffleTimer.Elapsed += new ElapsedEventHandler((source, e) => shuffleLetters());
         ReshuffleInterval = 7;
         HorizontalCalibration = new Calibration();  // note, we don't actually use this right now.
         VerticalCalibration = new Calibration();
         InCalibrationMode = true;
         this.VerticalCalibration.notifyAdjustmentMultiplyerChanged += verticalAdjustmentChanged;
         BackgroundGrayScalePercent = 0.0;
         SnellenDenominatorForegroundBrush = Brushes.Black;
      }

      protected Brush snellenDenominatorForegroundBrush_;
      public Brush SnellenDenominatorForegroundBrush
      {
         get { return snellenDenominatorForegroundBrush_; }
         private set { snellenDenominatorForegroundBrush_ = value; RaisePropertyChanged("SnellenDenominatorForegroundBrush"); }
      }

      protected Color backgroundColor_;
      public Color BackgroundColor
      {
         get { return backgroundColor_; }
         protected set { backgroundColor_ = value; RaisePropertyChanged("BackgroundColor"); }
      }

      protected Double backgroundGrayScalePercent_;
      public Double BackgroundGrayScalePercent
      {
         get { return backgroundGrayScalePercent_; }
         set 
         { 
            backgroundGrayScalePercent_ = value;
            if (backgroundGrayScalePercent_ > 100.0) backgroundGrayScalePercent_ = 100.0;
            if (backgroundGrayScalePercent_ < 0.0) backgroundGrayScalePercent_ = 0.0;
            RaisePropertyChanged("BackgroundGrayScalePercent");
            Double test = Byte.MaxValue * (1-(backgroundGrayScalePercent_ / 100.0));
            Byte rgbByte = (Byte)test;
            BackgroundColor = Color.FromRgb(rgbByte, rgbByte, rgbByte);
            if (backgroundGrayScalePercent_ < 56.5)
            {
               SnellenDenominatorForegroundBrush = Brushes.Black;
            }
            else
            {
               SnellenDenominatorForegroundBrush = Brushes.White;
            }
            RaisePropertyChanged("SnellenDenominatorForegroundBrush");
         }
      }

      protected Timer vertAdustTimer;
      protected ElapsedEventHandler verticalAdjustmentDelayMethod;
      protected void verticalAdjustmentChanged(object sender, PropertyChangedEventArgs e)
      {
         verticalAdjustmentDelayMethod = new ElapsedEventHandler(updateAllChartLines);
         vertAdustTimer = new Timer(2.0);
         vertAdustTimer.Elapsed += verticalAdjustmentDelayMethod;
         vertAdustTimer.Enabled = true;
      }

      private void updateAllChartLines(object sender, ElapsedEventArgs e)
      {
         updateAllChartLines();
         vertAdustTimer.Elapsed -= verticalAdjustmentDelayMethod;
         vertAdustTimer.Dispose();
         vertAdustTimer = null;
      }
      
      private bool inCalibrationMode_;
      public bool InCalibrationMode
      {
         get { return inCalibrationMode_; }
         set
         {
            inCalibrationMode_ = value;
            RaisePropertyChanged("InCalibrationMode");
            if(value == true)
            {
               CalibrationBarOpacity = 1.0;
               TestLettersOpacity = 0.10;
            }
            else
            {
               CalibrationBarOpacity = 0.0;
               TestLettersOpacity = 1.0;
            }
            RaisePropertyChanged("TestLettersOpacity");
            updateAllChartLines();
         }
      }

      private Calibration horizontalCalibration_;
      public Calibration HorizontalCalibration
      {
         get { return horizontalCalibration_; }
         set
         {
            horizontalCalibration_ = value;
            RaisePropertyChanged("HorizontalCalibration");
         }
      }

      private Calibration verticalCalibration_;
      public Calibration VerticalCalibration
      {
         get { return verticalCalibration_; }
         set
         {
            verticalCalibration_ = value;
            RaisePropertyChanged("VerticalCalibration");
            updateAllChartLines();
         }
      }

      private Double calibrationBarOpacity_;
      public Double CalibrationBarOpacity
      {
         get { return calibrationBarOpacity_; }
         set
         {
            calibrationBarOpacity_ = value;
            RaisePropertyChanged("CalibrationBarOpacity");
         }
      }


      public ObservableCollection<ChartLine> ChartLines
      { get; private set; }

      private Double subjectDistance_;
      public Double SubjectDistance
      {
         get { return subjectDistance_; }
         set
         {
            subjectDistance_ = value;
            RaisePropertyChanged("SubjectDistance");
         }
      }

      private Brush textForegroundBrush_;
      public Brush TextForegroundBrush
      {
         get
         { return textForegroundBrush_; }
         set
         { textForegroundBrush_ = value; RaisePropertyChanged("TextForegroundBrush"); }
      }

      private Brush leftBackgroundBrush_;
      public Brush LeftBackgroundBrush
      {
         get
         { return leftBackgroundBrush_; }
         set
         {
            leftBackgroundBrush_ = value;
            RaisePropertyChanged("LeftBackgroundBrush");
         }
      }

      private Brush rightBackgroundBrush_;
      public Brush RightBackgroundBrush
      {
         get
         { return rightBackgroundBrush_; }
         set
         {
            rightBackgroundBrush_ = value;
            RaisePropertyChanged("RightBackgroundBrush");
         }
      }

      private Timer reshuffleTimer = new Timer();

      private Double reshuffleInterval_;
      public Double ReshuffleInterval
      {
         get { return reshuffleInterval_; }
         set 
         { 
            reshuffleInterval_ = value;
            if (value == 0.0)
            {
               reshuffleTimer.Stop();
               reshuffleTimer.Enabled = false;
            }
            else
            {
               reshuffleTimer.Interval = value * 1000;
               reshuffleTimer.Enabled = true;
               reshuffleTimer.Start();
            }

            RaisePropertyChanged("ReshuffleInterval");
         }
      }

      private void updateAllChartLines()
      {
         if (null != ChartLines)
         {
            foreach (var aLine in ChartLines)
            {
               aLine.SubjectDistance = this.subjectDistance_;
               aLine.ForegroundBrush = this.TextForegroundBrush;
               aLine.LetterOpacity = this.TestLettersOpacity;// this.LetterOpacity;
               aLine.SnellenDenominatorForegroundBrush = this.SnellenDenominatorForegroundBrush;
               if (null != this.VerticalCalibration)
                  aLine.VerticalModifier = this.VerticalCalibration.AdjustmentMultiplier;
               else
                  aLine.VerticalModifier = 1.0;
            }
         }
      }

      private Double testLettersOpacity_;
      public Double TestLettersOpacity
      {
         get { return testLettersOpacity_; }
         set
         {
            testLettersOpacity_ = value;
            RaisePropertyChanged("TestLettersOpacity");
         }
      }

      #region Command_ToggleBackground
      public ICommand ToggleBackgroundCmd { get; private set; }
      private bool canToggleBackground { get { return true; } }
      private void toggleBicolorBackground()
      {
         if (LeftBackgroundBrush == Brushes.White && RightBackgroundBrush == Brushes.White)
         {
            LeftBackgroundBrush = Brushes.Red; RightBackgroundBrush = Brushes.Green;
         }
         else
         {
            LeftBackgroundBrush = Brushes.White; RightBackgroundBrush = Brushes.White;
         }
      }
      #endregion

      #region Command_ShuffleLetters
      public ICommand ShuffleLettersCmd { get; private set; }
      private bool canShuffleLetters { get { return true; } }
      private void shuffleLetters()
      {
         foreach (var line in this.ChartLines)
         {
            line.shuffle();
         }
      }
      #endregion

      public event PropertyChangedEventHandler PropertyChanged;
      public void RaisePropertyChanged(String prop)
      {
         if (null != PropertyChanged)
         {
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
            updateAllChartLines();
         }
      }

      public void Dispose()
      {
         if (null != reshuffleTimer) reshuffleTimer.Dispose();
         if (null != vertAdustTimer) vertAdustTimer.Dispose();
      }
   }

   // Copied from http://stackoverflow.com/questions/4160510/new-extended-wpftoolkit-colorpicker
   public class BrushColorConverter : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
      {
         if (null == value) return null;
         SolidColorBrush brush = value as SolidColorBrush;
         return brush.Color;
      }
      public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
      {
         if (null == value) return null;
         Color color = (Color)value;
         return new SolidColorBrush(color);
      }
   }

   public class ChartLine : INotifyPropertyChanged
   {
      private const Double maxChars = 6;
      private const Double minChars = 1;
      private Double numberOfChars;
      public ChartLine()
      {
         SnellenDenominator = 20.0;
         finishConstructors();
      }

      public ChartLine(Double initialSnellenDenominator)
      {
         SnellenDenominator = initialSnellenDenominator;
         finishConstructors();
      }

      private void finishConstructors()
      {
         initializeStaticList();
         //this.TextOpacity = 1.0;
         shuffle();
      }

      // See http://en.wikipedia.org/wiki/Dingbat#Unicode_dingbats
      //    for a bunch of good ones to choose from for children
      private void initializeStaticList()
      {
         if (null == Letterset) Letterset = new List<char>();
         if(Letterset.Count == 0)
         {
            Letterset.Add('C');
            Letterset.Add('D');
            Letterset.Add('E');
            Letterset.Add('F');
            Letterset.Add('H');
            Letterset.Add('K');
            Letterset.Add('N');
            Letterset.Add('P');
            Letterset.Add('R');
            Letterset.Add('U');
            Letterset.Add('V');
            Letterset.Add('Z');
            Letterset.Add('♠');  // Not standard.  Example of Unicode dingbat
                                 // for use with children -- to show how
                                 // easy it is.
            //Letterset.Add('✄');
            //Letterset.Add('✈');
            //Letterset.Add('✎');
            //Letterset.Add('✔');
            //Letterset.Add('✰');
            //Letterset.Add('♥');
            //Letterset.Add('⊨');
         }
      }

      private void setNumberofChars()
      {
         var num = Math.Floor(400.0 / (Double)SnellenDenominator);
         if (num < minChars) num = minChars;
         if (num > maxChars) num = maxChars;
         numberOfChars = num;
      }

      private Double snellenDenominator_;
      public Double SnellenDenominator
      {
         get {return snellenDenominator_;}
         set 
         {
            snellenDenominator_ = value;
            setNumberofChars();
            RaisePropertyChanged("SnellenDenominator");
         }
      }

      private Double subjectDistance_;
      public Double SubjectDistance
      {
         get { return subjectDistance_; }
         set { subjectDistance_ = value; RaisePropertyChanged("LetterFontSize"); }
      }

      public Double LetterHeight
      {
         get { return computeLetterHeightInInches(); }
         private set { }
      }

      private Double letterOpacity_;
      public Double LetterOpacity
      {
         get { return letterOpacity_; }
         set { letterOpacity_ = value; RaisePropertyChanged("LetterOpacity"); }
      }

      private Brush foregroundBrush_;
      public Brush ForegroundBrush
      {
         get { return foregroundBrush_; }
         set { foregroundBrush_ = value; RaisePropertyChanged("ForegroundBrush"); }
      }

      private Brush snellenDenominatorForegroundBrush_;
      public Brush SnellenDenominatorForegroundBrush
      {
         get { return snellenDenominatorForegroundBrush_; }
         internal set
         {
            snellenDenominatorForegroundBrush_ = value;
            RaisePropertyChanged("SnellenDenominatorForegroundBrush");
         }
      }

      private readonly static Double degreeToRadian = Math.PI / 180.0;
      private Double computeLetterHeightInInches()
      {
         Double retVal = this.verticalModifier_ *
            SubjectDistance * 24.0 * Math.Tan(degreeToRadian * SnellenDenominator / 480);
         return retVal;
      }

      private Double verticalModifier_;
      internal Double VerticalModifier
      {
         get { return verticalModifier_; }
         set
         { verticalModifier_ = value; RaisePropertyChanged("VerticalModifier"); }
      }

      public Double LetterFontSize
      {
         get { return computeLetterHeightInInches() * 96 * 4 / 3; }
         private set { }
      }

      private HashSet<char> letters_ = new HashSet<char>();
      public HashSet<char> Letters
      {
         get { return letters_; }
         set 
         {
            letters_ = value;
            RaisePropertyChanged("Letters");
         }
      }

      public String Letters_String
      {
         get { return this.ToString(); }
         private set { }
      }

      public event PropertyChangedEventHandler PropertyChanged;
      public void RaisePropertyChanged(String prop)
      {
         if (null != PropertyChanged)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
      }

      public void shuffle()
      {
         setNumberofChars();
         bool needsShufling = true;
         while (true == needsShufling)
         {
            Letters.Clear();
            int index = 0;
            while (index < numberOfChars)
            {
               var candidateLetterToAdd = getRandomLetter();
               if (false == Letters.Contains(candidateLetterToAdd))
               {
                  Letters.Add(candidateLetterToAdd);
                  index++;
               }
            }
            needsShufling = false;
            if(Letters.Contains('F') &&
               Letters.Contains('U') ||

               Letters.Contains('K') &&
               Letters.Contains('F'))
            {
               needsShufling = true;
            }
         }
         RaisePropertyChanged("Letters_String");
      }

      private static Random randomNumber = new Random();
      private char getRandomLetter()
      {
         char aLetter = '↯';

         int index=0;
         index = randomNumber.Next(0, Letterset.Count);
         aLetter = Letterset[index];
         return aLetter;
      }

      private List<char> Letterset {get; set;}

      public override string ToString()
      {
         StringBuilder retString = new StringBuilder();
         int counter = 0;
         foreach (var letter in Letters)
         {
            if (counter > 0 && counter < Letters.Count)
            { retString.Append(' '); retString.Append(' '); }
            retString.Append(letter);
            counter++;
         }

         return retString.ToString();
      }
   }

   public class Calibration : INotifyPropertyChanged
   {

      public Calibration()
      {
         DesiredLength = 3.0;
         AdjustmentMultiplier = 1.0;
      }

      private Double desiredLength_;
      public Double DesiredLength
      {
         get { return desiredLength_; }
         set 
         {
            desiredLength_ = value;
            RaisePropertyChanged("DesiredLength");
            RaisePropertyChanged("AdjustedLength");
         }
      }

      public Double adjustmentMultiplier_;
      public Double AdjustmentMultiplier
      {
         get { return adjustmentMultiplier_; }
         set
         {
            adjustmentMultiplier_ = value;
            RaisePropertyChanged("AdjustmentMultiplier");
            RaisePropertyChanged("AdjustedLength");
            if (null != notifyAdjustmentMultiplyerChanged)
               notifyAdjustmentMultiplyerChanged(null, null);
         }
      }

      internal event PropertyChangedEventHandler notifyAdjustmentMultiplyerChanged;

      public Double adjustedLength_;
      public Double AdjustedLength
      {
         get { return 96.0 * desiredLength_ * adjustmentMultiplier_; }
         set { }
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
