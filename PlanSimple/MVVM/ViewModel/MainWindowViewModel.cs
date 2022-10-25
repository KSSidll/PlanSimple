using PlanSimple.Core;

namespace PlanSimple.MVVM.ViewModel;

public class MainWindowViewModel : ObservableObject
{
	private object? _currentView;

	public MainWindowViewModel()
	{
		HomeViewModel = new HomeViewModel();
		NotesViewModel = new NotesViewModel();

		CurrentView = HomeViewModel;

		HomeViewCommand = new RelayCommand(o => { CurrentView = HomeViewModel; });
		NotesViewModelCommand = new RelayCommand(o => { CurrentView = NotesViewModel; });
	}

	public RelayCommand HomeViewCommand { get; }
	public RelayCommand NotesViewModelCommand { get; }

	public HomeViewModel HomeViewModel { get; }
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