using PlanSimple.Core;

namespace PlanSimple.MVVM.ViewModel;

public class MainWindowViewModel : ObservableObject
{
	private object? _currentView;

	public MainWindowViewModel()
	{
        CalendarViewModel = new CalendarViewModel();
		NotesViewModel = new NotesViewModel();

		CurrentView = CalendarViewModel;

        CalendarViewCommand = new RelayCommand(o => { CurrentView = CalendarViewModel; });
		NotesViewModelCommand = new RelayCommand(o => { CurrentView = NotesViewModel; });
	}

	public RelayCommand CalendarViewCommand { get; }
	public RelayCommand NotesViewModelCommand { get; }

	public CalendarViewModel CalendarViewModel { get; }
	public NotesViewModel NotesViewModel { get; }

	public object? CurrentView
	{
		get => _currentView;
		set
		{
			_currentView = value;
			OnPropertyChanged();
		}
	}
}