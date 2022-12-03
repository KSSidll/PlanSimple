using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using PlanSimple.Core;
using PlanSimple.Database.Context;
using PlanSimple.Database.Model;

namespace PlanSimple.MVVM.ViewModel;

public class NotesDisplayViewModel : BaseViewModel
{
	private static readonly ToDoNoteContext ToDoNoteContext = new();
	private static int _itemUpdateSkip;
	public NotesDisplayViewModel()
	{
		RefreshCollection();
		UpdateNote = new RelayCommand(o =>
		{
			if (o is not ToDoNote toDoNote) return;
			ToDoNoteContext.ToDoNotes.Update(toDoNote);
			ToDoNoteContext.SaveChanges();
			
			// Refresh displayed items when 1 seconds passes after last update
			++_itemUpdateSkip;
			new Task(async () =>
			{
				await Task.Delay(1000);
				--_itemUpdateSkip;
				
				if (_itemUpdateSkip == 0)
					RefreshCollection();
			}).Start();
			
		});
		
		DeleteNote = new RelayCommand(o =>
		{
			if (o is not ToDoNote toDoNote) return;
			ToDoNoteContext.ToDoNotes.Remove(toDoNote);
			ToDoNoteContext.SaveChanges();
			
			ToDoNotes.Remove(toDoNote);
		});
		
		ToggleDisplayCompleted = new RelayCommand(_ =>
		{
			RefreshCollection();
		});
	}

	private void RefreshCollection()
	{
		ToDoNotes = DisplayCompleted ? new ObservableCollection<ToDoNote>(ToDoNoteContext.ToDoNotes) : 
			new ObservableCollection<ToDoNote>(ToDoNoteContext.ToDoNotes.Where(x => x.Completed == false));
		OnPropertyChanged(nameof(ToDoNotes));
	}

	public ObservableCollection<ToDoNote> ToDoNotes { get; private set; } = null!;
	public static bool DisplayCompleted { get; set; }

	public RelayCommand? ToggleDisplayCompleted { get; }
	public RelayCommand? UpdateNote { get; }
	public RelayCommand? DeleteNote { get; }
}