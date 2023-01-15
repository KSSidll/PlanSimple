using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using PlanSimple.Core;
using PlanSimple.Database.Context;
using PlanSimple.Database.Model;

namespace PlanSimple.MVVM.ViewModel;

public class NotesDisplayViewModel : BaseViewModel
{
	private readonly ToDoNoteContext _toDoNoteContext = new();
	private static int _itemUpdateSkip;
	public NotesDisplayViewModel()
	{
		RefreshCollection();
		UpdateNote = new RelayCommand(o =>
		{
			if (o is not ToDoNote toDoNote) return;
			_toDoNoteContext.ToDoNotes.Update(toDoNote);
			_toDoNoteContext.SaveChanges();
			
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
			_toDoNoteContext.ToDoNotes.Remove(toDoNote);
			_toDoNoteContext.SaveChanges();
			
			ToDoNotes.Remove(toDoNote);
		});
		
		ToggleDisplayCompleted = new RelayCommand(_ =>
		{
			RefreshCollection();
		});
	}

	private void RefreshCollection()
	{
		ToDoNotes = DisplayCompleted ? new ObservableCollection<ToDoNote>(_toDoNoteContext.ToDoNotes) : 
			new ObservableCollection<ToDoNote>(_toDoNoteContext.ToDoNotes.Where(x => x.Completed == false));
		OnPropertyChanged(nameof(ToDoNotes));
	}

	public ObservableCollection<ToDoNote> ToDoNotes { get; private set; } = null!;
	public static bool DisplayCompleted { get; set; }

	public RelayCommand? ToggleDisplayCompleted { get; }
	public RelayCommand? UpdateNote { get; }
	public RelayCommand? DeleteNote { get; }
}