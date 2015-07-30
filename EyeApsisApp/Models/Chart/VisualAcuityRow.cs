using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeApsisApp.Models.Chart
{
   public class VisualAcuityRow
   {
      public Double snellenDenominator { get; set; }
      public String Foot { get; set; }
      public String Meter { get; set; }
      public String Decimale { get; set; }
      public String LogMAR { get; set; }

      public VisualAcuityRow(Double snellenDenom, String ft, String mtr, String decml, String lgMar)
      {
         snellenDenominator = snellenDenom;
         Foot = ft;
         Meter = mtr;
         Decimale = decml;
         LogMAR = lgMar;
      }

      public String Value_
      {
         get { return getValue(); }
      }

      public String getValue()
      {
         if (snellenDenominator < 0.0001) return String.Empty;
         switch(CurrentState)
         {
         case VisualAcuityState.meter:
         {
            return Meter;
         }
         case VisualAcuityState.decimale:
         {
            return Decimale;
         }
         case VisualAcuityState.logMAR:
         {
            return LogMAR;
         }
         }
         return Foot;
      }

      public override string ToString()
      {
         return this.getValue();
      }

      public static VisualAcuityState CurrentState
      { get; set; }
   }

   public enum VisualAcuityState
   {
      foot, meter, decimale, logMAR
   }
}
