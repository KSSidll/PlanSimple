using System;
using PlanSimple.Core;
using PlanSimple.Database.Context;
using PlanSimple.Database.Model;

namespace PlanSimple.MVVM.ViewModel;

public class NoteEditViewModel
{
	private static readonly NoteContext NoteContext = new();

	public NoteEditViewModel()
	{
		SaveDataToDatabase = new RelayCommand(o =>
		{
			// Don't save if the note is empty
			if (string.IsNullOrEmpty(Note.Content)) return;
			
			// Convert DateTime to DateOnly
			// DatePicker supports DateTime but not DateOnly, but as we are only interested in DateOnly,
			// We need to use another property for it and then convert it during save
			if (Date != null) Note.ExpireDate = new DateOnly(Date.Value.Year, Date.Value.Month, Date.Value.Day);

			NoteContext.Notes.Update(Note);
			NoteContext.SaveChanges();
		});
	}

	public RelayCommand? SaveDataToDatabase { get; }
	public Note Note { get; } = new();
	public DateTime? Date { get; set; }
}