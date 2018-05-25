using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using ModernNotes.WpfClient.Models;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;

namespace ModernNotes.WpfClient.Services
{
	public interface INotesService
	{
		IEnumerable<Note> GetNotes();
		Note SaveNewNote(string noteText);
		bool UpdateNote(int noteId, string noteText);
		bool DeleteNote(int noteId);
	}

	public class NotesService : INotesService
	{
		private readonly RestClient _restClient;

		public NotesService()
		{
			_restClient = new RestClient("http://localhost:60194/");
		}

		public IEnumerable<Note> GetNotes()
		{
			var request = new RestRequest("api/notes", Method.GET);
			var response = _restClient.Execute(request);

			if (!response.IsSuccessful) return null;

			var result = JsonConvert.DeserializeObject<IEnumerable<Note>>(response.Content);
			return result;
		}

		public Note SaveNewNote(string noteText)
		{
			var request = new RestRequest("api/notes", Method.POST);
			request.AddJsonBody(new NewNote(noteText));
			var response = _restClient.Execute(request);

			if (!response.IsSuccessful) return null;

			var result = JsonConvert.DeserializeObject<Note>(response.Content);
			return result;
		}

		public bool UpdateNote(int noteId, string noteText)
		{
			var request = new RestRequest($"api/notes/{noteId}", Method.PUT);
			request.AddJsonBody(new UpdateNote(noteText));
			var response = _restClient.Execute(request);
			return response.IsSuccessful;
		}

		public bool DeleteNote(int noteId)
		{
			var request = new RestRequest($"api/notes/{noteId}", Method.DELETE);
			var response = _restClient.Execute(request);
			return response.IsSuccessful;
		}
	}

	public class DesignNotesService : INotesService
	{
		public IEnumerable<Note> GetNotes()
		{
			return new[]
			{
				new Note {Id = 1, Text = "Test1"},
				new Note {Id = 2, Text = "Test2"},
				new Note {Id = 3, Text = "Test3"}
			};
		}

		public Note SaveNewNote(string noteText)
		{
			return new Note();
		}

		public bool UpdateNote(int noteId, string noteText)
		{
			return true;
		}

		public bool DeleteNote(int noteId)
		{
			return true;
		}
	}
}
