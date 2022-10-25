using PlanSimple.Core;

namespace PlanSimple.MVVM.ViewModel;

public class NotesViewModel : ObservableObject
{
	private object? _currentView;
	
	public NotesViewModel()
	{
		NotesDisplayViewModel = new NotesDisplayViewModel();
		NoteEditViewModel = new NoteEditViewModel();

		CurrentView = NotesDisplayViewModel;

		NotesDisplayViewCommand = new RelayCommand(o => { CurrentView = NotesDisplayViewModel; });
		NoteEditViewCommand = new RelayCommand(o => { CurrentView = NoteEditViewModel; });
	}

	public RelayCommand NotesDisplayViewCommand { get; }
	public RelayCommand NoteEditViewCommand { get; }

	public NotesDisplayViewModel NotesDisplayViewModel { get; }
	public NoteEditViewModel NoteEditViewModel { get; }

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