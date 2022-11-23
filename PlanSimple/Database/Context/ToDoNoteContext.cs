using Microsoft.EntityFrameworkCore;
using PlanSimple.Database.Model;

namespace PlanSimple.Database.Context;

public class ToDoNoteContext : DbContext
{
	public DbSet<ToDoNote> ToDoNotes { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlite("Data source=to-do-notes.db");
	}
}