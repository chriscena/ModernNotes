﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ModernNotes.Web.Contracts;

namespace ModernNotes.Web.Controllers
{
	/// <summary>
	/// Notes Resources
	/// </summary>
	[Produces("application/json")]
	[Route("api/Notes")]
	public class NotesController : Controller
	{
		private readonly INotesRepository _repository;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="repository"></param>
		public NotesController(INotesRepository repository)
		{
			_repository = repository;
		}

		/// <summary>
		/// Gets all available notes.
		/// </summary>
		/// <returns>A list of Note instances.</returns>
		[HttpGet]
		public IEnumerable<Note> Get()
		{
			return _repository.GetNotes();
		}

		/// <summary>
		/// Saves a new note.
		/// </summary>
		/// <param name="newNote">The note to save as a NewNote.</param>
		/// <returns>The saved Note</returns>
		[HttpPost]
		[ProducesResponseType(typeof(Note), 200)]
		[ProducesResponseType(400)]
		public IActionResult Post([FromBody]NewNote newNote)
		{
			if (string.IsNullOrWhiteSpace(newNote.Text)) return BadRequest();
			return Ok(_repository.AddNewNote(newNote));
		}

		/// <summary>
		/// Updates a note with the given id.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="noteToUpdate"></param>
		/// <returns></returns>
		[HttpPut("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		public IActionResult Put(int id, [FromBody]UpdateNote noteToUpdate)
		{
			var note = new Note(id, noteToUpdate.Text);
			try
			{
				_repository.UpdateNote(note);
				return Ok();
			}
			catch (ResourceNotFoundException)
			{
				return NotFound();
			}
		}

		/// <summary>
		/// Deletes a note with the given id.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpDelete("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		public IActionResult Delete(int id)
		{
			try
			{
				_repository.DeleteNote(id);
				return Ok();
			}
			catch (ResourceNotFoundException)
			{
				return NotFound();
			}
		}
	}
}
