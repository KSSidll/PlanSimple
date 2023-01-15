using System;
using PlanSimple.Core;
using PlanSimple.Database.Context;
using PlanSimple.Database.Model;

namespace PlanSimple.MVVM.ViewModel;

public class NoteEditViewModel : BaseViewModel
{
	private protected static readonly ToDoNoteContext ToDoNoteContext = new();
	
	// Changing views calls parameterless constructor, so if this isn't static, changing views will reinitialise it,
	// because of that we need to call constructor with new ToDoNote() if we want to erase saved data
	private protected static ToDoNote toDoNote = new();
	private protected static DateTime? date;
	
	public NoteEditViewModel()
	{
		SaveDataToDatabase = new RelayCommand(_ =>
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

	public NoteEditViewModel(ToDoNote note) : this()
	{
		ToDoNote = note;
		
		// Set displayed date to note date if it has one
		if (ToDoNote.Date is null)
		{
			// If note doesn't contain a date, set currently displayed date to null, to erase saved data
			Date = null;
			return;
		}
		
		Date = new DateTime(ToDoNote.Date.Value.Year, ToDoNote.Date.Value.Month, ToDoNote.Date.Value.Day);
	}

	public RelayCommand? SaveDataToDatabase { get; }

	public ToDoNote ToDoNote
	{
		get => toDoNote;
		init
		{
			if (Equals(value, toDoNote)) return;
			toDoNote = value;
			OnPropertyChanged();
		}
	}

	public DateTime? Date
	{
		get => date;
		set
		{
			if (Nullable.Equals(value, date)) return;
			date = value;
			OnPropertyChanged();
		}
	}
}