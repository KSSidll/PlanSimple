using PlanSimple.Core;
using PlanSimple.Database.Model;

namespace PlanSimple.MVVM.ViewModel;

public class CalendarViewModel : BaseViewModel
{
	private static BaseViewModel? _currentView = new CalendarDisplayViewModel();
	
	public CalendarViewModel()
	{
		if (_currentView is CalendarNoteEditViewModel) CurrentView = new CalendarDisplayViewModel();
		
		CalendarDisplayViewCommand = new RelayCommand(_ => { CurrentView = new CalendarDisplayViewModel(); });
		NoteEditViewCommand = new RelayCommand(o =>
		{
			if (o is ToDoNote note)
			{
				CurrentView = new CalendarNoteEditViewModel(note);
			}
			else
			{
				// Changing views calls parameterless constructor, so displayed note is a static property,
				// because of that we need to call constructor with new ToDoNote() if we want to erase saved data
				CurrentView = new CalendarNoteEditViewModel(new ToDoNote());
			}
		});
	}

	public RelayCommand CalendarDisplayViewCommand { get; }
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