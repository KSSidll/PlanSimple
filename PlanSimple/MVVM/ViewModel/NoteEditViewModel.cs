using System;
using PlanSimple.Core;
using PlanSimple.Database.Context;
using PlanSimple.Database.Model;

namespace PlanSimple.MVVM.ViewModel;

public class NoteEditViewModel
{
	private static readonly ToDoNoteContext ToDoNoteContext = new();

	public NoteEditViewModel()
	{
		SaveDataToDatabase = new RelayCommand(o =>
		{
			// Don't save if the note doesn't have any set title
			if (string.IsNullOrEmpty(ToDoNote.Title)) return;
			
			// Convert DateTime to DateOnly
			// DatePicker supports DateTime but not DateOnly, but as we are only interested in DateOnly,
			// We need to use another property for it and then convert it during save
			if (Date != null) ToDoNote.Date = new DateOnly(Date.Value.Year, Date.Value.Month, Date.Value.Day);

			ToDoNoteContext.ToDoNotes.Update(ToDoNote);
			ToDoNoteContext.SaveChanges();
		});
	}

	public RelayCommand? SaveDataToDatabase { get; }
	public ToDoNote ToDoNote { get; } = new();
	public DateTime? Date { get; set; }
}