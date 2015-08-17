using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EyeApsisApp.Properties;

namespace EyeApsisApp
{
   public class VisionTestRecord : INotifyPropertyChanged
   {
      public VisionTestRecord()
      {
         TestDateAndTime = DateTime.Now;
         LeftEyeAcuityCorrection = Resources.DidNotAsk;
         RightEyeAcuityCorrection = Resources.DidNotAsk;
      }

      private String subjectFirstName_;
      public String SubjectFirstName
      {
         get { return subjectFirstName_; }
         set
         {
            subjectFirstName_ = value;
            RaisePropertyChanged("SubjectFirstName");
         }
      }

      private String subjectLastName_;
      public String SubjectLastName
      {
         get { return subjectLastName_; }
         set
         {
            subjectLastName_ = value;
            RaisePropertyChanged("SubjectLastName");
         }
      }


      private DateTime testDateAndTime_;
      public DateTime TestDateAndTime
      {
         get { return testDateAndTime_; }
         set
         {
            testDateAndTime_ = value;
            RaisePropertyChanged("TestDateAndTime");
         }
      }


      private String testLocation_;
      public String TestLocation
      {
         get { return testLocation_; }
         set
         {
            testLocation_ = value;
            RaisePropertyChanged("TestLocation");
         }
      }

      private String leftEyeAcuityCorrection_;
      public String LeftEyeAcuityCorrection
      {
         get { return leftEyeAcuityCorrection_; }
         set
         {
            leftEyeAcuityCorrection_ = value;
            RaisePropertyChanged("LeftEyeAcuityCorrection");
         }
      }


      private Double leftEyeVisualAcuity_;
      public Double LeftEyeVisualAcuity
      {
         get { return leftEyeVisualAcuity_; }
         set
         {
            leftEyeVisualAcuity_ = value;
            RaisePropertyChanged("LeftEyeVisualAcuity");
         }
      }

      //public AcuityListAssociation<RowPassedEnum> leftEyeAcuities {get; set;} = Create<RowPassedEnum>();

      private String rightEyeAcuityCorrection_;
      public String RightEyeAcuityCorrection
      {
         get { return rightEyeAcuityCorrection_; }
         set
         {
            rightEyeAcuityCorrection_ = value;
            RaisePropertyChanged("RightEyeAcuityCorrection");
         }
      }


      private int rightEyeVisualAcuity_;
      public int RightEyeVisualAcuity
      {
         get { return rightEyeVisualAcuity_; }
         set
         {
            rightEyeVisualAcuity_ = value;
            RaisePropertyChanged("RightEyeVisualAcuity");
         }
      }


      private int redGreenColorSensitivity_;
      public int RedGreenColorSensitivity
      {
         get { return redGreenColorSensitivity_; }
         set
         {
            redGreenColorSensitivity_ = value;
            RaisePropertyChanged("RedGreenColorSensitivity");
         }
      }


      private int leftEyeContrastSensitivity_;
      public int LeftEyeContrastSensitivity
      {
         get { return leftEyeContrastSensitivity_; }
         set
         {
            leftEyeContrastSensitivity_ = value;
            RaisePropertyChanged("LeftEyeContrastSensitivity");
         }
      }


      private int rightEyeContrastSensitivity_;
      public int RightEyeContrastSensitivity
      {
         get { return rightEyeContrastSensitivity_; }
         set
         {
            rightEyeContrastSensitivity_ = value;
            RaisePropertyChanged("RightEyeContrastSensitivity");
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