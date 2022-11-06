using System.Collections.ObjectModel;
using PlanSimple.Core;
using PlanSimple.Database.Context;
using PlanSimple.Database.Model;

namespace PlanSimple.MVVM.ViewModel;

public class NotesViewModel
{
	private static readonly NoteContext NoteContext = new();

	public NotesViewModel()
	{
		AddNote = new RelayCommand(o =>
		{
			Note note = new Note(NoteText);
			NoteContext.Notes.Add(note);
			NoteContext.SaveChanges();

			Notes.Add(note);
		});
	}

	public static ObservableCollection<Note> Notes { get; } = new(NoteContext.Notes);

	public RelayCommand? AddNote { get; }
	public string NoteText { get; set; } = string.Empty;
}