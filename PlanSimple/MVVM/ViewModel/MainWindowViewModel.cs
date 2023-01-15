using PlanSimple.Core;

namespace PlanSimple.MVVM.ViewModel;

public class MainWindowViewModel : BaseViewModel
{
	private static BaseViewModel? _currentView;

	public MainWindowViewModel()
	{
		CurrentView = new CalendarViewModel();

        CalendarViewCommand = new RelayCommand(_ => { CurrentView = new CalendarViewModel(); });
		NotesViewModelCommand = new RelayCommand(_ => { CurrentView = new NotesViewModel(); });
		OptionsViewCommand = new RelayCommand(_ => { CurrentView = new OptionsViewModel(); });
	}

	public RelayCommand CalendarViewCommand { get; }
	public RelayCommand NotesViewModelCommand { get; }
	public RelayCommand OptionsViewCommand { get; }

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