using System;
using System.Collections.Generic;
using PlanSimple.Database.Model;

namespace PlanSimple.MVVM.Model
{
    public class ToDoListDisplayModel
    {
        public DateOnly Date { get; set; }
        public List<ToDoNote> ToDoNotes { get; set; } = new();
        public string DisplayDate { get => $"{Date.Day} {Date.ToString("MMM")} {Date.ToString("ddd")}"; }
    }
}