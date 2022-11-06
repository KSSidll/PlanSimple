using System;
using System.ComponentModel.DataAnnotations;

namespace PlanSimple.Database.Model;

public class ToDoNote
{
	[Key] public int NoteId { get; set; }

	public string? Title { get; set; }
	public string? Description { get; set; }
	public DateOnly? Date { get; set; }
	public Priority Priority { get; set; }
	public bool Completed { get; set; }
}