using System;
using System.Collections.Generic;
using System.Linq;
using ModernNotes.Web.Contracts;

namespace ModernNotes.Web
{
	/// <summary>
	/// 
	/// </summary>
	public interface INotesRepository
	{
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		IEnumerable<Note> GetNotes();

		/// <summary>
		/// 
		/// </summary>
		/// <param name="newNote"></param>
		/// <returns></returns>
		Note AddNewNote(NewNote newNote);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		void DeleteNote(int id);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="updatedNote"></param>
		void UpdateNote(Note updatedNote);
	}

	/// <summary>
	/// 
	/// </summary>
	public class InMemoryNotesRepository : INotesRepository
	{
		/// <summary>
		/// 
		/// </summary>
		public static List<Note> Notes { get; } = new List<Note>();

		/// <inheritdoc />
		public IEnumerable<Note> GetNotes()
		{
			return Notes;
		}

		/// <inheritdoc />
		public Note AddNewNote(NewNote newNote)
		{
			var id = !Notes.Any() ? 1 : Notes.Max(n => n.Id) + 1;
			var note = new Note(id, newNote.Text);
			Notes.Add(note);
			return note;
		}

		/// <inheritdoc />
		public void DeleteNote(int id)
		{
			var noteToRemove = Notes.FirstOrDefault(n => n.Id == id);
			if (noteToRemove == null)
				throw new ResourceNotFoundException();
			Notes.Remove(noteToRemove);
		}

		/// <inheritdoc />
		public void UpdateNote(Note updatedNote)
		{
			var noteToRemove = Notes.FirstOrDefault(n => n.Id == updatedNote.Id);
			if (noteToRemove == null)
				throw new ResourceNotFoundException();
			Notes.Remove(noteToRemove);
			Notes.Add(updatedNote);
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class ResourceNotFoundException : Exception
	{
	}
}