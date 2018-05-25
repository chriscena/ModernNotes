namespace ModernNotes.WpfClient.Models
{
	public class UpdateNote
	{
		public UpdateNote(string noteText)
		{
			Text = noteText;
		}

		public string Text { get; set; }
	}
}