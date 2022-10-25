using System.ComponentModel.DataAnnotations;

namespace PlanSimple.Database.Model;

public class Note
{
	public Note(string? content = null)
	{
		Content = content;
	}

	[Key] public int NoteId { get; set; }

	public string? Content { get; set; }
}