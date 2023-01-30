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
        public static DateTime DisplayedWeek = DateTime.Now;
        public BindingList<WeekDayModel> Week { get; set; } = new();
        public string? CurrentMonth { get => CalendarHelper.GetMonthName(Week.FirstOrDefault()?.Date); }
        public BindingList<ToDoListDisplayModel> Days { get; set; } = new();
        public RelayCommand? PreviousWeek { get; }
        public RelayCommand? NextWeek { get; }
        public RelayCommand? ResetWeekToToday { get; }
        public RelayCommand? UpdateNote { get; }
        public bool IsInfoVisible { get => String.IsNullOrWhiteSpace(InfoMessage) == false; }

        public string? InfoMessage
        {
            get => _infoMessage;
            set
            {
                _infoMessage = value;
                OnPropertyChanged(nameof(InfoMessage));
                OnPropertyChanged(nameof(IsInfoVisible));
            }
        }

        private readonly ToDoNoteContext _toDoNoteContext = new();
        private DbSet<ToDoNote> ToDoNotes => _toDoNoteContext.ToDoNotes;
        private string? _infoMessage = null;
        
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
            
            ResetWeekToToday = new RelayCommand(_ =>
            {
                var weekDiff = DateTime.Now.Subtract(DisplayedWeek).Days;
                DisplayedWeek = DateTime.Now;
                OffSetWeek(weekDiff);
            });
            
            UpdateNote = new RelayCommand(o =>
            {
                if (o is not ToDoNote toDoNote) return;
                _toDoNoteContext.ToDoNotes.Update(toDoNote);
                _toDoNoteContext.SaveChanges();
            });

        }

        private void OffSetWeek(int days)
        {
            for (int i = 0; i < Week.Count; i++)
            {
                Week[i].Date = Week[i].Date.AddDays(days);
            }

            DisplayedWeek = DisplayedWeek.AddDays(days);
            Week.ResetBindings();
            GetNotesForWeek();
            OnPropertyChanged(nameof(CurrentMonth));
        }

        private void SetWeekDays()
        {
            var currentDay = DisplayedWeek;

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

            InfoMessage = (groups.Count() == 0) ? "You have no notes for this week!" : null;

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
                
                _toDoNoteContext.SaveChanges();
            }         

        }
    }
}
