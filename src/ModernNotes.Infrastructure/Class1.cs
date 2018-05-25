using System;
using Microsoft.EntityFrameworkCore;

namespace ModernNotes.Infrastructure
{
	public class NotesContext : DbContext
	{
		public DbSet<Note> Notes { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite("Data Source=notes.db");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder
				.Entity<Note>().HasKey(n => n.Id);
			modelBuilder
				.Entity<Note>()
				.Property(n => n.Id).ValueGeneratedOnAdd();
			base.OnModelCreating(modelBuilder);

		}
	}

	public class Note
	{
		public int Id { get; set; }
		public string Text { get; set; }
	}

	public class NotesRepository : INotesRepository
	{

	}
}
