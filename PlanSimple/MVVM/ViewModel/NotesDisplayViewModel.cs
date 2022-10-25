using System.Collections.ObjectModel;
using PlanSimple.Database.Context;
using PlanSimple.Database.Model;

namespace PlanSimple.MVVM.ViewModel;

public class NotesDisplayViewModel
{
	private static readonly NoteContext NoteContext = new();

	public NotesDisplayViewModel()
	{
		
	}

	public ObservableCollection<Note> Notes { get; } = new(NoteContext.Notes);

	public string NoteText { get; set; } = string.Empty;
}