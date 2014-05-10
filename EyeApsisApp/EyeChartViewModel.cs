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

namespace EyeApsisApp
{
   public class EyeChartViewModel : INotifyPropertyChanged
   {
      public EyeChartViewModel()
      {
         this.SubjectDistance = 20;
         ChartLines = new ObservableCollection<ChartLine>();
         ChartLines.Add(new ChartLine(initialSnellenDenominator: 100.0));
         ChartLines.Add(new ChartLine(initialSnellenDenominator: 60.0));
         ChartLines.Add(new ChartLine(initialSnellenDenominator: 40.0));
         ChartLines.Add(new ChartLine(initialSnellenDenominator: 20.0));
         ChartLines.Add(new ChartLine(initialSnellenDenominator: 15.0));
         ChartLines.Add(new ChartLine(initialSnellenDenominator: 10.0));
         updateAllChartLines();
         LeftBackgroundBrush = Brushes.White;
         RightBackgroundBrush = Brushes.White;
         //LeftBackgroundBrush = Brushes.Red;
         //RightBackgroundBrush = Brushes.Green;
         BackgroundIsVisible = true;

         ToggleBackgroundCmd = new RelayCommand(toggleBicolorBackground, () => canToggleBackground);
         ShuffleLettersCmd = new RelayCommand(shuffleLetters, () => canShuffleLetters);

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

      public Visibility BackgroundVisibility_;
      public Visibility BackgroundVisibility
      {
         get { return BackgroundVisibility_; }
         set { BackgroundVisibility_ = value; RaisePropertyChanged("BackgroundVisibility"); }
      }

      private Boolean backgroundIsVisible_;
      public Boolean BackgroundIsVisible
      {
         get { return backgroundIsVisible_; }
         set 
         { 
            backgroundIsVisible_ = value;
            if (backgroundIsVisible_ == true)
               BackgroundVisibility = Visibility.Visible;
            else
               BackgroundVisibility = Visibility.Hidden;

            RaisePropertyChanged("BackgroundIsVisible"); 
         }
      }

      private void updateAllChartLines()
      {
         if (null != ChartLines)
         {
            foreach (var aLine in ChartLines)
            {
               aLine.SubjectDistance = this.subjectDistance_;
            }
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
         shuffle();
      }

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
            Letterset.Add('♠');
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

      private readonly static Double degreeToRadian = Math.PI / 180.0;
      private Double computeLetterHeightInInches()
      {
         Double retVal = SubjectDistance * 24.0 * Math.Tan(degreeToRadian * SnellenDenominator / 480);
         return retVal;
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

}
