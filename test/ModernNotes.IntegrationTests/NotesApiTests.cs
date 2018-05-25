using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModernNotes.Web;
using ModernNotes.Web.Contracts;
using Newtonsoft.Json;

namespace ModernNotes.IntegrationTests
{
	[TestClass]
	public class NotesApiTests
	{
		private readonly TestServer _server;
		private readonly HttpClient _client;

		public NotesApiTests()
		{
			_server = new TestServer(new WebHostBuilder()
				.UseStartup<TestStartup>());
			_client = _server.CreateClient();
		}

		[TestMethod]
		public async Task PostNewNoteReturnsNoteWithNewId()
		{
			InMemoryNotesRepository.Notes.Clear();
			var newNote = new NewNote {Text = "test"};
			var bodyContent = new StringContent(JsonConvert.SerializeObject(newNote), Encoding.Default, "application/json");

			var response = await _client.PostAsync("/api/notes", bodyContent);

			response.EnsureSuccessStatusCode();
			var responseString = await response.Content.ReadAsStringAsync();
			var resultNote = JsonConvert.DeserializeObject<Note>(responseString);
			Assert.AreNotEqual(0, resultNote.Id);
			Assert.AreEqual("test", resultNote.Text);
		}

		[TestMethod]
		public async Task PostEmptyNoteReturnsBadRequest()
		{
			InMemoryNotesRepository.Notes.Clear();
			var newNote = new NewNote { Text = "" };
			var bodyContent = new StringContent(JsonConvert.SerializeObject(newNote), Encoding.Default, "application/json");

			var response = await _client.PostAsync("/api/notes", bodyContent);

			Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
		}

		[TestMethod]
		public async Task GetNotesWith0NotesReturns0Notes()
		{
			InMemoryNotesRepository.Notes.Clear();

			var response = await _client.GetAsync("/api/notes");

			response.EnsureSuccessStatusCode();
			var responseString = await response.Content.ReadAsStringAsync();
			var resultNote = JsonConvert.DeserializeObject<IEnumerable<Note>>(responseString);
			Assert.AreEqual(0, resultNote.Count());
		}

		[TestMethod]
		public async Task GetNotesWith3NotesReturns3Notes()
		{
			InMemoryNotesRepository.Notes.Clear();
			InMemoryNotesRepository.Notes.AddRange(new[]
			{
				new Note(1, "Test1"),
				new Note(2, "Test2"),
				new Note(3, "Test3"),
			});

			var response = await _client.GetAsync("/api/notes");

			response.EnsureSuccessStatusCode();
			var responseString = await response.Content.ReadAsStringAsync();
			var resultNote = JsonConvert.DeserializeObject<IEnumerable<Note>>(responseString);
			Assert.AreEqual(3, resultNote.Count());
		}

		[TestMethod]
		public async Task PutChangedNote3NotesReturnsSuccessAndChangesNote()
		{
			InMemoryNotesRepository.Notes.Clear();
			InMemoryNotesRepository.Notes.AddRange(new[]
			{
				new Note(1, "Test1"),
				new Note(2, "Test2"),
				new Note(3, "Test3"),
			});
			var newNote = new UpdateNote{ Text = "Changed2"};
			var bodyContent = new StringContent(JsonConvert.SerializeObject(newNote), Encoding.Default, "application/json");

			var response = await _client.PutAsync("/api/notes/2", bodyContent);

			response.EnsureSuccessStatusCode();
			var result = InMemoryNotesRepository.Notes.Single(n => n.Id == 2);
			Assert.AreEqual("Changed2", result.Text);
		}

		[TestMethod]
		public async Task PutNonExistentNoteReturns404NotFound()
		{
			InMemoryNotesRepository.Notes.Clear();
			InMemoryNotesRepository.Notes.AddRange(new[]
			{
				new Note(1, "Test1"),
				new Note(2, "Test2"),
				new Note(3, "Test3"),
			});
			var newNote = new Note(4, "Changed4");
			var bodyContent = new StringContent(JsonConvert.SerializeObject(newNote), Encoding.Default, "application/json");

			var response = await _client.PutAsync("/api/notes/4", bodyContent);

			Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
		}

		[TestMethod]
		public async Task DeleteNoteReturnsSuccessAndRemovesNote()
		{
			InMemoryNotesRepository.Notes.Clear();
			InMemoryNotesRepository.Notes.AddRange(new[]
			{
				new Note(1, "Test1"),
				new Note(2, "Test2"),
				new Note(3, "Test3"),
			});

			var response = await _client.DeleteAsync("/api/notes/2");

			response.EnsureSuccessStatusCode();
			var result = InMemoryNotesRepository.Notes.SingleOrDefault(n => n.Id == 2);
			Assert.IsNull(result);
		}

		[TestMethod]
		public async Task DeleteNonExistentNoteReturns404NotFound()
		{
			InMemoryNotesRepository.Notes.Clear();
			InMemoryNotesRepository.Notes.AddRange(new[]
			{
				new Note(1, "Test1"),
				new Note(2, "Test2"),
				new Note(3, "Test3"),
			});

			var response = await _client.DeleteAsync("/api/notes/4");

			Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
		}
	}
}
