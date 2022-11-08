using System.Collections.ObjectModel;
using PlanSimple.Core;
using PlanSimple.Database.Context;
using PlanSimple.Database.Model;

namespace PlanSimple.MVVM.ViewModel;

public class NotesDisplayViewModel
{
	private static readonly ToDoNoteContext ToDoNoteContext = new();

	public NotesDisplayViewModel()
	{
		UpdateNote = new RelayCommand(o =>
		{
			if (o is not ToDoNote toDoNote) return;
			ToDoNoteContext.ToDoNotes.Update(toDoNote);
			ToDoNoteContext.SaveChanges();
		});
		
		DeleteNote = new RelayCommand(o =>
		{
			if (o is not ToDoNote toDoNote) return;
			ToDoNoteContext.ToDoNotes.Remove(toDoNote);
			ToDoNoteContext.SaveChanges();
			
			ToDoNotes.Remove(toDoNote);
		});
	}

	public ObservableCollection<ToDoNote> ToDoNotes { get; } = new(ToDoNoteContext.ToDoNotes);
	
	public RelayCommand? UpdateNote { get; }
	public RelayCommand? DeleteNote { get; }
}