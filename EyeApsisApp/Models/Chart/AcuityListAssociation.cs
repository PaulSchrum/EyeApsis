using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeApsisApp.Models.Chart
{
   public class AcuityListAssociation
   {
      // These values are taken from
      //    https://en.wikipedia.org/wiki/LogMAR_chart
      private static List<VisualAcuityRow> availableRows = new List<VisualAcuityRow>{
            new VisualAcuityRow(200.0, "20/200", "6/60", "0.10", "1.00"),
            new VisualAcuityRow(160.0, "20/160", "6/48", "0.125", "0.90"),
            new VisualAcuityRow(125.0, "20/125", "6/38", "0.16", "0.80"),
            new VisualAcuityRow(100.0, "20/100", "6/30", "0.20", "0.70"),
            new VisualAcuityRow(80.0, "20/80", "6/24", "0.25", "0.60"),
            new VisualAcuityRow(63.0, "20/63", "6/19", "0.32", "0.50"),
            new VisualAcuityRow(50.0, "20/50", "6/15", "0.40", "0.40"),
            new VisualAcuityRow(40.0, "20/40", "6/12", "0.50", "0.30"),
            new VisualAcuityRow(32.0, "20/32", "6/9.5", "0.63", "0.20"),
            new VisualAcuityRow(25.0, "20/25", "6/7.5", "0.80", "0.10"),
            new VisualAcuityRow(20.0, "20/20", "6/6", "1.00", "0.00"),
            new VisualAcuityRow(16.0, "20/16", "6/4.8", "1.25", "-0.10"),
            new VisualAcuityRow(12.5, "20/12.5", "6/3.8", "1.60", "-0.20"),
            new VisualAcuityRow(10.0, "20/10", "6/3", "2.00", "-0.30")
         };

      public static IEnumerable<VisualAcuityRow> AvailableVisualAcuities 
      {
         get {return availableRows;}
      }
   }

   public class AcuityListAssociation<T> : AcuityListAssociation
      where T : class
   {
      private VisualAcuityRow currentRow_ = null;
      public VisualAcuityRow CurrentRow 
      {
         get { return currentRow_; }
         set
         {
            if(value == null)
            {
               currentRow_ = null;
               return;
            }
            var isAllowed = AcuityListAssociation.AvailableVisualAcuities
               .Any(row => 
                  row.snellenDenominator == value.snellenDenominator);

            if (!isAllowed)
               throw new ArgumentOutOfRangeException
                  ("Initial Visual Acuity is not one of the allowed values.");
            else
               currentRow_ = value;
         }
      }

      T CurrentValue { get; set; }
      private Dictionary<VisualAcuityRow, T> allRows { get; set; }

      private AcuityListAssociation()
      {
         setupAllRows();
      }

      private void setupAllRows()
      {
         allRows = new Dictionary<VisualAcuityRow,T>();
         foreach (var row in AvailableVisualAcuities)
            allRows.Add(row, null);
      }

      public AcuityListAssociation<T> Create<T>(
         VisualAcuityRow initialRow = null,
         T initialValue = null
         ) 
         where T: class
      {
         var newList = new AcuityListAssociation<T>();

         newList.CurrentRow = initialRow;
         newList.CurrentValue = initialValue;

         return newList;
      }

   }


}
