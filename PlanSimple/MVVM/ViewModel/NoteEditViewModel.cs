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
			if (string.IsNullOrEmpty(Note.Content)) return;
			NoteContext.Notes.Update(Note);
			NoteContext.SaveChanges();
		});
	}

	public RelayCommand? SaveDataToDatabase { get; }
	public Note Note { get; set; } = new();

}