using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PlanSimple.Database.Context;
using PlanSimple.Database.Model;
using PlanSimple.MVVM.Model;

namespace PlanSimple.MVVM.ViewModel
{

    public class CalendarViewModel : BaseViewModel
    {       
        public List<WeekDayModel> Week { get; set; } = new();
        public List<ToDoListDisplayModel> Days { get; set; } = new();
        
        private static readonly ToDoNoteContext ToDoNoteContext = new();
        private DbSet<ToDoNote> ToDoNotes => ToDoNoteContext.ToDoNotes;
        
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

            // If Note database is empty, add dummy data for test purposes
            if (!ToDoNotes.Any())
            {
                ToDoNotes.Add(new ToDoNote
                {
                    Title = "Learn English",
                    Description = "Do english quizlet",
                    Date = new DateOnly(2022, 11, 6),
                    Priority = Priority.Medium
                });
                
                ToDoNotes.Add(new ToDoNote
                {
                    Title = "Learn German",
                    Description = "Do german quizlet",
                    Date = new DateOnly(2022, 11, 6),
                    Priority = Priority.Medium
                });
                
                
                ToDoNotes.Add(new ToDoNote
                {
                    Title = "Learn Math",
                    Description = "Learn calculus",
                    Date = new DateOnly(2022, 11, 3),
                    Priority = Priority.Medium
                });

                ToDoNotes.Add(new ToDoNote
                {
                    Title = "Learn Coding",
                    Description = "Do project",
                    Date = new DateOnly(2022, 11, 7),
                    Priority = Priority.High
                });
                
                ToDoNoteContext.SaveChanges();
            }
            
            // filter out ToDoNotes that don't have any set date before grouping them into days 
            var groups = ToDoNotes.Where(x => x.Date != null).GroupBy(x => x.Date);
        
            foreach (var g in groups)
            {
                ToDoListDisplayModel model = new ToDoListDisplayModel { Date = (DateOnly) g.Key! };
                foreach (var element in g)
                {
                    model.ToDoNotes.Add(element);
                }
        
                Days.Add(model);
            }
        }
    }
}
