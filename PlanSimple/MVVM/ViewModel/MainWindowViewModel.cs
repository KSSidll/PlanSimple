using PlanSimple.Core;
using PlanSimple.Database.Model;

namespace PlanSimple.MVVM.ViewModel;

public class MainWindowViewModel : BaseViewModel
{
	private static BaseViewModel? _currentView;

	public MainWindowViewModel()
	{
		CurrentView = new CalendarViewModel();

        CalendarViewCommand = new RelayCommand(_ => { CurrentView = new CalendarViewModel(); });
		NotesViewModelCommand = new RelayCommand(_ => { CurrentView = new NotesViewModel(); });
		
		NoteEditViewCommand = new RelayCommand(o =>
		{
			if (o is not ToDoNote) return;
			
			CurrentView = new NotesViewModel();
			if ((CurrentView as NotesViewModel)!.NoteEditViewCommand.CanExecute(o))
				(CurrentView as NotesViewModel)!.NoteEditViewCommand.Execute(o);
			
			NotesNavButtonChecked = true;
			OnPropertyChanged(nameof(NotesNavButtonChecked));
		});
	}

	public RelayCommand CalendarViewCommand { get; }
	public RelayCommand NotesViewModelCommand { get; }
	public RelayCommand NoteEditViewCommand { get; }

	public bool CalendarNavButtonChecked { get; set; } = true;
	public bool NotesNavButtonChecked { get; set; }

	public BaseViewModel? CurrentView
	{
		get => _currentView;
		set
		{
			if (Equals(value, _currentView)) return;
			_currentView = value;
			OnPropertyChanged();
		}
	}
}