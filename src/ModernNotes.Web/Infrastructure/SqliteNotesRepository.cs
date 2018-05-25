using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ModernNotes.Web.Contracts;

namespace ModernNotes.Web.Infrastructure
{
	/// <summary>
	/// Sqlite implementation of INotesRepository
	/// </summary>
	public class SqliteNotesRepository : INotesRepository
	{
		/// <inheritdoc/>
		public IEnumerable<Note> GetNotes()
		{
			using (var context = new NotesContext())
			{
				return context.Notes.AsNoTracking().ToList();
			}
		}

		/// <inheritdoc/>
		public Note AddNewNote(NewNote newNote)
		{
			using (var context = new NotesContext())
			{
				var note = new Note(0, newNote.Text);
				context.Notes.Add(note);
				context.SaveChanges();
				return note;
			}
		}

		/// <inheritdoc/>
		public void DeleteNote(int id)
		{
			using (var context = new NotesContext())
			{
				var note = context.Notes.SingleOrDefault(n => n.Id == id);
				if (note == null) throw new ResourceNotFoundException();
				context.Notes.Remove(note);
				context.SaveChanges();
			}
		}

		/// <inheritdoc/>
		public void UpdateNote(Note updatedNote)
		{
			using (var context = new NotesContext())
			{
				var note = context.Notes.SingleOrDefault(n => n.Id == updatedNote.Id);
				if (note == null) throw new ResourceNotFoundException();
				note.Text = updatedNote.Text;
				context.SaveChanges();
			}
		}
	}
}