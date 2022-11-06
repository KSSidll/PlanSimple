using System;

namespace PlanSimple.Database.Model
{
    public class ToDoModel
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public Priority Priority { get; set; }
        public bool IsDone { get; set; } = false;
    }
}
