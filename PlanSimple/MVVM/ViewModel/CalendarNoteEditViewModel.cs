using PlanSimple.Database.Model;

namespace PlanSimple.MVVM.ViewModel;

public class CalendarNoteEditViewModel : NoteEditViewModel
{
	public CalendarNoteEditViewModel() : base() {}
	public CalendarNoteEditViewModel(ToDoNote note) : base(note) {}
}