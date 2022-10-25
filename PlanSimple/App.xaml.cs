using System.Windows;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PlanSimple.Database.Context;

namespace PlanSimple
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			DatabaseFacade noteDatabaseFacade = new DatabaseFacade(new NoteContext());
			noteDatabaseFacade.EnsureCreated();
		}
	}
}