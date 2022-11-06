using PlanSimple.Core;
using PlanSimple.Database.Model;
using PlanSimple.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanSimple.MVVM.ViewModel
{

    public class CalendarViewModel
    {       
        public List<WeekDayModel> Week { get; set; } = new List<WeekDayModel>();
        public List<ToDoModel> ToDoList { get; set; } = new List<ToDoModel>();
        public List<ToDoListDisplayModel> Days { get; set; } = new List<ToDoListDisplayModel>();

        public CalendarViewModel()
        {
            SetTestData();
        }

        private void SetTestData()
        {
            Week = new List<WeekDayModel>
            {
               new WeekDayModel{ DayName="Mon", DayNumber=31 },
               new WeekDayModel{ DayName="Tues", DayNumber=1 },
               new WeekDayModel{ DayName="Wed", DayNumber=2 },
               new WeekDayModel{ DayName="Thurs", DayNumber=3 },
               new WeekDayModel{ DayName="Fri", DayNumber=4 },
               new WeekDayModel{ DayName="Sat", DayNumber=5 },
               new WeekDayModel{ DayName="Sun", DayNumber=6 }
            };
            ToDoList = new List<ToDoModel>
            {
                new ToDoModel
                {
                    Title = "Learn English",
                    Description = "Do english quizlet",
                    Date = DateTime.Parse("06.11.2022"),
                    Priority = Priority.Medium
                },    
                new ToDoModel
                {
                    Title = "Learn German",
                    Description = "Do german quizlet",
                    Date = DateTime.Parse("06.11.2022"),
                    Priority = Priority.Medium
                },                
                new ToDoModel
                {
                    Title = "Learn Math",
                    Description = "Learn calculus",
                    Date = DateTime.Parse("03.11.2022"),
                    Priority = Priority.Medium
                },                
                new ToDoModel
                {
                    Title = "Learn Coding",
                    Description = "Do project",
                    Date = DateTime.Parse("01.11.2022"),
                    Priority = Priority.High
                },
            };
            var groups = ToDoList.GroupBy(x => x.Date);

            foreach (var g in groups)
            {
                ToDoListDisplayModel model = new ToDoListDisplayModel { Date = g.Key };
                foreach (var element in g)
                {
                    model.ToDoList.Add(element);
                }

                Days.Add(model);
            }
        }
    }
}
