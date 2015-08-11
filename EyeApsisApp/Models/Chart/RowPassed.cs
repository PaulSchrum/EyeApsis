using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeApsisApp.Models.Chart
{
   public class RowPassed
   {
      public RowPassed()
      {
         DidPass = RowPassedStringList.Values[0];
      }

      private String didPass_;
      public String DidPass
      {
         get { return didPass_; }
         set
         {
            if (value.IsAllowedInRowPassedStringList() == true)
               didPass_ = value;
            else
               throw new ArgumentOutOfRangeException("Value of \"{0}\" not allowed.", value);
         }
      }
   }
}
