using Microsoft.EntityFrameworkCore;
using PlanSimple.Database.Model;

namespace PlanSimple.Database.Context;

public class NoteContext : DbContext
{
	public DbSet<Note> Notes { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlite("Data source=notes.db");
	}
}