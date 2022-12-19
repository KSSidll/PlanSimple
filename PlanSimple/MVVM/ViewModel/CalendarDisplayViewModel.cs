using System;
using System.ComponentModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PlanSimple.Core;
using PlanSimple.Database.Context;
using PlanSimple.Database.Model;
using PlanSimple.HelperClasses;
using PlanSimple.MVVM.Model;

namespace PlanSimple.MVVM.ViewModel
{

    public class CalendarDisplayViewModel : BaseViewModel
    {       
        public BindingList<WeekDayModel> Week { get; set; } = new();
        public string? CurrentMonth { get => CalendarHelper.GetMonthName(Week.FirstOrDefault()?.Date); }
        public BindingList<ToDoListDisplayModel> Days { get; set; } = new();
        public RelayCommand? PreviousWeek { get; }
        public RelayCommand? NextWeek { get; }

        private static readonly ToDoNoteContext ToDoNoteContext = new();
        private DbSet<ToDoNote> ToDoNotes => ToDoNoteContext.ToDoNotes;
        
        public CalendarDisplayViewModel()
        {
            SetWeekDays();

            GetNotesForWeek();

            PreviousWeek = new RelayCommand(_ =>
            {
                OffSetWeek(-7);
            });

            NextWeek = new RelayCommand(_ =>
            {
                OffSetWeek(7);
            });

        }

        private void OffSetWeek(int days)
        {
            for (int i = 0; i < Week.Count; i++)
            {
                Week[i].Date = Week[i].Date.AddDays(days);
            }
            Week.ResetBindings();
            GetNotesForWeek();
            OnPropertyChanged(nameof(CurrentMonth));
        }

        private void SetWeekDays()
        {
            var currentDay = DateTime.Now;

            var week = CalendarHelper.GetWeek(currentDay)
                .Select(x => new WeekDayModel { Date = DateOnly.FromDateTime(x) })
                .ToList();

            Week = new BindingList<WeekDayModel>(CalendarHelper.GetWeek(currentDay)
                .Select(x => new WeekDayModel { Date = DateOnly.FromDateTime(x) })
                .ToList()
            );
        }

        private void GetNotesForWeek()
        {
            // filter out ToDoNotes that don't have any set date before grouping them into days 
            var groups = ToDoNotes
                .Where(x => x.Date != null && Week.First().Date <= x.Date && x.Date <= Week.Last().Date)
                .GroupBy(x => x.Date);

            Days.Clear();

            foreach (var g in groups)
            {
                ToDoListDisplayModel model = new ToDoListDisplayModel { Date = (DateOnly)g.Key! };
                foreach (var element in g)
                {
                    model.ToDoNotes.Add(element);
                }

                Days.Add(model);
            }
        }
    
        private void SetTestData()
        {

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

        }
    }
}
