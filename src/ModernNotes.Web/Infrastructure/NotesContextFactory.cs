using Microsoft.EntityFrameworkCore.Design;

namespace ModernNotes.Web.Infrastructure
{
	/// <inheritdoc />
	public class NotesContextFactory : IDesignTimeDbContextFactory<NotesContext>
	{
		/// <inheritdoc />
		public NotesContext CreateDbContext(string[] args)
		{
			return new NotesContext();
		}
	}
}