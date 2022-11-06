using PlanSimple.Database.Model;
using System;
using System.Collections.Generic;

namespace PlanSimple.MVVM.Model
{
    public class ToDoListDisplayModel
    {
        public DateTime Date { get; set; }
        public List<ToDoModel> ToDoList { get; set; } = new List<ToDoModel>();
        public string DisplayDate { get => $"{Date.Day} {Date.ToString("MMM")} {Date.ToString("ddd")}"; }
    }
}