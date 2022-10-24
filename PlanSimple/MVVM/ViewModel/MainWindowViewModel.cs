using PlanSimple.Core;

namespace PlanSimple.MVVM.ViewModel;

public class MainWindowViewModel : ObservableObject
{
	private object? _currentView;

	public MainWindowViewModel()
	{
		HomeViewModel = new HomeViewModel();
		TextInputViewModel = new TextInputViewModel();

		CurrentView = TextInputViewModel;

		HomeViewCommand = new RelayCommand(o => { CurrentView = HomeViewModel; });

		TextInputViewCommand = new RelayCommand(o => { CurrentView = TextInputViewModel; });
	}

	public RelayCommand HomeViewCommand { get; set; }
	public RelayCommand TextInputViewCommand { get; set; }

	public HomeViewModel HomeViewModel { get; set; }
	public TextInputViewModel TextInputViewModel { get; set; }

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