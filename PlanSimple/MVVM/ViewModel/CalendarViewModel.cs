using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using PlanSimple.Core;
using PlanSimple.Database.Context;
using PlanSimple.Database.Model;
using PlanSimple.MVVM.Model;
using PlanSimple.HelperClasses;

namespace PlanSimple.MVVM.ViewModel
{

    public class CalendarViewModel : BaseViewModel
    {       
        public BindingList<WeekDayModel> Week { get; set; } = new();
        public BindingList<ToDoListDisplayModel> Days { get; set; } = new();

        public RelayCommand? PreviousWeek { get; }
        public RelayCommand? NextWeek { get; }

        private static readonly ToDoNoteContext ToDoNoteContext = new();
        private DbSet<ToDoNote> ToDoNotes => ToDoNoteContext.ToDoNotes;
        
        public CalendarViewModel()
        {
            SetWeekDays();

            GetNotesForWeek();

            PreviousWeek = new RelayCommand(_ =>
            {
                for (int i = 0; i < Week.Count; i++)
                {
                    Week[i].Date = Week[i].Date.AddDays(-7);
                }
                Week.ResetBindings();
                GetNotesForWeek();
            });

            NextWeek = new RelayCommand(_ =>
            {
                for (int i = 0; i < Week.Count; i++)
                {
                    Week[i].Date = Week[i].Date.AddDays(7);
                }
                Week.ResetBindings();
                GetNotesForWeek();
            });
        }

        private void SetWeekDays()
        {
            var currentDay = DateTime.Now;

            var week = CalendarHelper.GetWeek(currentDay)
                .Select(x => new WeekDayModel { Date = DateOnly.FromDateTime(x) })
                .ToList();

            Week = new BindingList<WeekDayModel>();

            foreach (var item in week)
            {
                Week.Add(item);
            }
        }

        private void GetNotesForWeek()
        {
            // filter out ToDoNotes that don't have any set date before grouping them into days 
            var groups = ToDoNotes
                .Where(x => x.Date != null)
                .Where(x => Week.First().Date <= x.Date && x.Date <= Week.Last().Date)
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
            Days.ResetBindings();
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
