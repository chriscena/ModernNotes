namespace ModernNotes.WpfClient.Models
{
	public class NewNote
	{
		public string Text { get; set; }

		public NewNote(string text)
		{
			Text = text;
		}
	}
}