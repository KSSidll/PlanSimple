﻿using System.Collections.ObjectModel;
using PlanSimple.Core;
using PlanSimple.Database.Context;
using PlanSimple.Database.Model;

namespace PlanSimple.MVVM.ViewModel;

public class NotesDisplayViewModel
{
	private static readonly NoteContext NoteContext = new();

	public NotesDisplayViewModel()
	{
		UpdateNote = new RelayCommand(o =>
		{
			if (o is not Note note) return;
			NoteContext.Notes.Update(note);
			NoteContext.SaveChanges();
		});
		
		DeleteNote = new RelayCommand(o =>
		{
			if (o is not Note note) return;
			NoteContext.Notes.Remove(note);
			NoteContext.SaveChanges();
			
			Notes.Remove(note);
		});
	}

	public ObservableCollection<Note> Notes { get; } = new(NoteContext.Notes);
	
	public RelayCommand? UpdateNote { get; }
	public RelayCommand? DeleteNote { get; }
}