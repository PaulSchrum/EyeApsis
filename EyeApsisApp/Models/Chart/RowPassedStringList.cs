using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeApsisApp.Models.Chart
{
   public static class RowPassedStringList
   {
      // Note: This class acts like an enum, but the values are strings

      private static List<String> values { get; set; }
      public static IReadOnlyList<String> Values
      {
         get { return values; }
      }

      static RowPassedStringList()
      {
         init();
      }

      public static bool IsAllowedInRowPassedStringList(this String candidate)
      {
         return values.Any(val => val.Equals(candidate));
      }

      private static void init()
      {
         values = new List<string> 
            {
               "Did Not Ask", "Yes", "No"
            };
      }

      public static void Init()
      {
         if (null == values)
            init();
      }

      public static String IsAvailable(String str)
      {
         return Values.Where(strng => strng.Equals(str)).Single();
      }
      /* * / Next: Hook up this string list with the radio buttons on the dashboard window /* */
      // this requires adding an instance of AcuityListAssociation<RowPassedEnum> leftEyeAcuities
      // and binding to it using GroupName= or what ever you can get to work
   }
}
