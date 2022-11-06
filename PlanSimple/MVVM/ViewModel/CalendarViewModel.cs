using PlanSimple.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanSimple.MVVM.ViewModel
{
    public class WeekDayModel
    {
        public string DayName { get; set; }
        public int DayNumber { get; set; }
    }

    public class Day

    public class CalendarViewModel
    {       
        public List<WeekDayModel> WeekDays { get; set; } = new List<WeekDayModel>();
        public CalendarViewModel()
        {
            SetTestData();
        }

        private void SetTestData()
        {
            WeekDays = new List<WeekDayModel>
            {
               new WeekDayModel{ DayName="Mon", DayNumber=25 },
               new WeekDayModel{ DayName="Tues", DayNumber=26 },
               new WeekDayModel{ DayName="Wed", DayNumber=27 },
               new WeekDayModel{ DayName="Thurs", DayNumber=28 },
               new WeekDayModel{ DayName="Fri", DayNumber=29 },
               new WeekDayModel{ DayName="Sat", DayNumber=30 },
               new WeekDayModel{ DayName="Sun", DayNumber=1 }
            };
        }
    }
}
