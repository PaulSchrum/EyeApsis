﻿using System;
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

   public static class vsAcuityStateExtensions
   {
      public const String foot = "Foot";
      public const String meter = "Meter";
      public const String decimale = "Decimal";
      public const String logMAR = "LogMAR";

      public static VisualAcuityState ToVisualAcuityState(this String str)
      {
         switch (str)
         {
            case foot: { return VisualAcuityState.foot; }
            case meter: { return VisualAcuityState.meter; }
            case decimale: { return VisualAcuityState.decimale; }
            case logMAR: { return VisualAcuityState.logMAR; }
         }
         return VisualAcuityState.foot;
      }

      public static String ToString(this VisualAcuityState vas)
      {
         switch (vas)
         {
            case VisualAcuityState.foot: { return foot; }
            case VisualAcuityState.meter: { return meter; }
            case VisualAcuityState.decimale: { return decimale; }
            case VisualAcuityState.logMAR: { return logMAR; }
         }
         return foot;
      }
   }
}
