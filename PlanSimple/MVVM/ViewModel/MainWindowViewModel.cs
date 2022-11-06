using PlanSimple.Core;

namespace PlanSimple.MVVM.ViewModel;

public class MainWindowViewModel : ObservableObject
{
	private object? _currentView;

	public MainWindowViewModel()
	{
		HomeViewModel = new HomeViewModel();
		NotesViewModel = new NotesViewModel();
        CalendarViewModel = new CalendarViewModel();

		CurrentView = HomeViewModel;

		HomeViewCommand = new RelayCommand(o => { CurrentView = HomeViewModel; });
		NotesViewCommand = new RelayCommand(o => { CurrentView = NotesViewModel; });
        CalendarViewCommand = new RelayCommand(o => { CurrentView = CalendarViewModel; });
	}

	public RelayCommand HomeViewCommand { get; }
	public RelayCommand NotesViewCommand { get; }
	public RelayCommand CalendarViewCommand { get; }

	public HomeViewModel HomeViewModel { get; }
	public NotesViewModel NotesViewModel { get; }
	public CalendarViewModel CalendarViewModel { get; }

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