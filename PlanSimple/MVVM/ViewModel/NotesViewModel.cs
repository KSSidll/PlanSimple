using PlanSimple.Core;
using PlanSimple.Database.Model;

namespace PlanSimple.MVVM.ViewModel;

public class NotesViewModel : BaseViewModel
{
	private static BaseViewModel? _currentView = new NotesDisplayViewModel();
	
	public NotesViewModel()
	{
		NotesDisplayViewCommand = new RelayCommand(_ => { CurrentView = new NotesDisplayViewModel(); });
		NoteEditViewCommand = new RelayCommand(o =>
		{
			if (o is ToDoNote note)
			{
				CurrentView = new NoteEditViewModel(note);
			}
			else
			{
				// Changing views calls parameterless constructor, so displayed note is a static property,
				// because of that we need to call constructor with new ToDoNote() if we want to erase saved data
				CurrentView = new NoteEditViewModel(new ToDoNote());
			}
		});
	}

	public RelayCommand NotesDisplayViewCommand { get; }
	public RelayCommand NoteEditViewCommand { get; }

	public BaseViewModel? CurrentView
	{
		get => _currentView;
		set
		{
			if(Equals(value, _currentView)) return;
			_currentView = value;
			OnPropertyChanged();
		}
	}
}