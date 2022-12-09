using PlanSimple.HelperClasses;
using System;

namespace PlanSimple.Database.Model
{
    public class WeekDayModel
    {
        public string? DayName { get => CalendarHelper.WeekDaysShortNames[Date.DayOfWeek.ToString()]; }
        public int DayNumber { get => Date.Day; }
        public DateOnly Date { get; set; }
    }
}
