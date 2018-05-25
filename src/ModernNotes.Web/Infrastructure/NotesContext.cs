using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModernNotes.Web.Contracts;

namespace ModernNotes.Web.Infrastructure
{
	/// <summary>
	/// 
	/// </summary>
	public class NotesContext : DbContext
	{
		private readonly string _storeName;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="storeName"></param>
		public NotesContext(string storeName = null)
		{
			_storeName = storeName ?? "notes.db";
		}
		/// <summary>
		/// Notes
		/// </summary>
		public DbSet<Note> Notes { get; set; }

		/// <inheritdoc />
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite($"Data Source={_storeName}");
		}

		/// <inheritdoc />
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder
				.Entity<Note>().HasKey(n => n.Id);
			modelBuilder
				.Entity<Note>()
				.Property(n => n.Id)
				.ValueGeneratedOnAdd();
			base.OnModelCreating(modelBuilder);

		}
	}
}
