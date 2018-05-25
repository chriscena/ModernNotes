using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using ModernNotes.WpfClient.Models;
using ModernNotes.WpfClient.Services;

namespace ModernNotes.WpfClient.Main
{
	/// <summary>
	/// This class contains properties that the main View can data bind to.
	/// </summary>
	public class MainViewModel : ViewModelBase
	{
		private readonly INotesService _notesService;
		private readonly IDialogService _dialogService;
		private bool _isEditing;
		private string _noteText;
		private Note _selectedNote;
		private bool _canEdit;
		private bool _canDelete;

		/// <summary>
		/// Initializes a new instance of the MainViewModel class.
		/// </summary>
		public MainViewModel(INotesService notesService, IDialogService dialogService)
		{
			_notesService = notesService;
			_dialogService = dialogService;

			LoadedCommand = new RelayCommand(GetNotes);
			RefreshCommand = new RelayCommand(GetNotes);
			NewCommand = new RelayCommand(StartNewNote);
			EditCommand = new RelayCommand(EditNote);
			DeleteCommand = new RelayCommand(DeleteNote);
			SaveCommand = new RelayCommand(SaveNote);
			CancelEditCommand = new RelayCommand(CancelEdit);

		}

		private void CancelEdit()
		{
			IsEditing = false;
		}

		private void EditNote()
		{
			IsEditing = true;
		}

		public RelayCommand LoadedCommand { get; }

		public RelayCommand NewCommand { get; }

		public RelayCommand RefreshCommand { get; }
		public RelayCommand SaveCommand { get; }

		public RelayCommand EditCommand { get; }
		public RelayCommand DeleteCommand { get; }
		public RelayCommand CancelEditCommand { get; }

		public ObservableCollection<Note> Notes { get; set; } = new ObservableCollection<Note>();

		public bool IsEditing
		{
			get => _isEditing;
			set
			{
				Set(() => IsEditing, ref _isEditing, value);
				RaisePropertyChanged(nameof(CanCreateNew));
				RaisePropertyChanged(nameof(CanEdit));
				RaisePropertyChanged(nameof(CanDelete));
			}
		}

		public bool CanEdit
		{
			get => _canEdit && !IsEditing;
			set
			{
				Set(() => CanEdit, ref _canEdit, value);
			}
		}

		public bool CanDelete
		{
			get => _canDelete && !IsEditing;
			set => Set(() => CanDelete, ref _canDelete, value);
		}

		public bool CanCreateNew => !IsEditing;

		public string NoteText
		{
			get => _noteText;
			set => Set(() => NoteText, ref _noteText, value);
		}

		public Note SelectedNote
		{
			get => _selectedNote;
			set
			{
				Set(() => SelectedNote, ref _selectedNote, value);
				if (_selectedNote == null)
				{
					NoteText = "";
					CanEdit = false;
					CanDelete = false;
					return;
				}
				NoteText = _selectedNote.Text;
				CanEdit = true;
				CanDelete = true;
			}
		}
		
		private void StartNewNote()
		{
			SelectedNote = null;
			IsEditing = true;
		}

		private void SaveNote()
		{
			if (SelectedNote == null)
			{
				var note = _notesService.SaveNewNote(NoteText);
				if (note == null)
				{
					_dialogService.ShowError("Could not save note.", "Error", null, null);
					return;
				}
			}
			else
			{
				if (!_notesService.UpdateNote(SelectedNote.Id, NoteText))
				{
					_dialogService.ShowError("Could not update note.", "Error", null, null);
					return;
				}
				SelectedNote = null;
			}

			NoteText = "";
			IsEditing = false;
			GetNotes();
		}

		private void DeleteNote()
		{
			if (!_dialogService.ShowMessage("Delete message?", "Delete", "Ok", "Cancel", null).Result) return;
			if (!_notesService.DeleteNote(SelectedNote.Id))
			{
				_dialogService.ShowError("Could not delete note.", "Error", null, null);
				return;
			}
			GetNotes();
		}

		private void GetNotes()
		{
			var notes = _notesService.GetNotes();
			if (notes == null)
			{
				_dialogService.ShowError("Could not fetch notes. Can't connect to the service.", "Error", null, null);
				return;
			}

			Notes.Clear();
			foreach (var note in notes)
			{
				Notes.Add(note);
			}
		}
	}
}