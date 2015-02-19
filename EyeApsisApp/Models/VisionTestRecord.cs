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
         LeftEyeSurgicalAcuityCorrection = Resources.DidNotAsk;
         LeftEyeCorrection = Resources.DidNotAsk;
         RightEyeSurgicalAcuityCorrection = Resources.DidNotAsk;
         RightEyeCorrection = Resources.DidNotAsk;
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


      private String leftEyeSurgicalAcuityCorrection_;
      public String LeftEyeSurgicalAcuityCorrection
      {
         get { return leftEyeSurgicalAcuityCorrection_; }
         set
         {
            leftEyeSurgicalAcuityCorrection_ = value;
            RaisePropertyChanged("LeftEyeSurgicalAcuityCorrection");
         }
      }


      private String leftEyeCorrection_;
      public String LeftEyeCorrection
      {
         get { return leftEyeCorrection_; }
         set
         {
            leftEyeCorrection_ = value;
            RaisePropertyChanged("LeftEyeCorrection");
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


      private String rightEyeSurgicalAcuityCorrection_;
      public String RightEyeSurgicalAcuityCorrection
      {
         get { return rightEyeSurgicalAcuityCorrection_; }
         set
         {
            rightEyeSurgicalAcuityCorrection_ = value;
            RaisePropertyChanged("RightEyeSurgicalAcuityCorrection");
         }
      }


      private String rightEyeCorrection_;
      public String RightEyeCorrection
      {
         get { return rightEyeCorrection_; }
         set
         {
            rightEyeCorrection_ = value;
            RaisePropertyChanged("RightEyeCorrection");
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


      private int colorSensitivity_;
      public int ColorSensitivity
      {
         get { return colorSensitivity_; }
         set
         {
            colorSensitivity_ = value;
            RaisePropertyChanged("ColorSensitivity");
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