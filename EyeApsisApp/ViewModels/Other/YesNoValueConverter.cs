using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using EyeApsisApp.Models.Chart;

namespace EyeApsisApp.ViewModels.Other
{
   public class YesNoValueConverter : IValueConverter
   {
      private String didNotAsk_ { get; set; }
      private String yes_ { get; set; }
      private String no_ { get; set; }

      private readonly String englishDidNotAsk_ = RowPassedStringList.IsAvailable("Did Not Ask");
      private readonly String englishYes_ = RowPassedStringList.IsAvailable("Yes");
      private readonly String englishNo_ = RowPassedStringList.IsAvailable("No");

      public YesNoValueConverter()
      {
         didNotAsk_ =
            Properties.Resources.ResourceManager
            .GetString("DidNotAsk", CultureInfo.CurrentUICulture);
         yes_ =
            Properties.Resources.ResourceManager
            .GetString("Yes", CultureInfo.CurrentUICulture);
         no_ =
            Properties.Resources.ResourceManager
            .GetString("No", CultureInfo.CurrentUICulture);
      }

      public object Convert(
         object value, 
         Type targetType, 
         object parameter, 
         System.Globalization.CultureInfo culture)
      {
         String source = value as String;
         String target = String.Empty;

         if (source.Equals(englishDidNotAsk_))
            target = didNotAsk_;
         else if (source.Equals(englishYes_))
            target = yes_;
         else if (source.Equals(no_))
            target = no_;
         else
            target = didNotAsk_;

         return target;
      }

      public object ConvertBack(object value, 
         Type targetType, 
         object parameter, 
         System.Globalization.CultureInfo culture)
      {
         String source = String.Empty;
         String target = parameter as string;

         if (target.Equals(didNotAsk_))
            source = englishDidNotAsk_;
         else if (target.Equals(yes_))
            source = englishYes_;
         else if (target.Equals(no_))
            source = englishNo_;
         else
            source = englishDidNotAsk_;

         return source;
      }
   }
}
