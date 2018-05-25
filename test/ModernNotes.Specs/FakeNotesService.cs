using System.Collections.Generic;
using System.Linq;
using ModernNotes.WpfClient.Models;
using ModernNotes.WpfClient.Services;

namespace ModernNotes.Specs
{
	public class FakeNotesService : INotesService
	{
		public readonly List<Note> Notes = new List<Note>();

		public IEnumerable<Note> GetNotes()
		{
			return Notes;
		}

		public Note SaveNewNote(string noteText)
		{
			var nextId = Notes.Any() ? Notes.Max(n => n.Id) + 1 : 1;
			var newNote = new Note {Id = nextId, Text = noteText};
			Notes.Add(newNote);
			return newNote;
		}

		public bool UpdateNote(int noteId, string noteText)
		{
			var note = Notes.FirstOrDefault(n => n.Id == noteId);
			if (note == null) return false;
			note.Text = noteText;
			return true;
		}

		public bool DeleteNote(int noteId)
		{
			var note = Notes.FirstOrDefault(n => n.Id == noteId);
			if (note == null) return false;
			Notes.Remove(note);
			return true;
		}
	}
}