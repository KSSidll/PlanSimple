using PlanSimple.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanSimple.HelperClasses
{
    public static class CalendarHelper
    {
        public static readonly Dictionary<string, string> WeekDaysShortNames = new Dictionary<string, string>
        {
            {"Monday", "Mon"},
            {"Tuesday", "Tues"},
            {"Wednesday", "Wed"},
            {"Thursday", "Thurs"},
            {"Friday", "Fri"},
            {"Saturday", "Sat"},
            {"Sunday", "Sun"},
        };
        public static List<DateTime> GetWeek(DateTime day)
        {
            List<DateTime> week = new List<DateTime>();

            if (day.DayOfWeek == DayOfWeek.Sunday)
            {
                for (int i = 6; i >= 0; i--)
                {
                    week.Add(day.AddDays(-i));
                }
            }
            else
            {
                var x = (int)day.DayOfWeek - (int)DayOfWeek.Monday;

                // before
                for (int i = x; i > 0; i--)
                {
                    week.Add(day.AddDays(-i));
                }
                // day + after
                for (int i = 0; i <= (int)DayOfWeek.Saturday - (int)day.DayOfWeek + 1; i++)
                {
                    week.Add(day.AddDays(i));
                }
            }

            return week;
        }

        public static string? GetMonthName(DateOnly? date)
        {
            var x = Enum.GetName(typeof(Month), date?.Month);
            return x;
        }
    }
}
