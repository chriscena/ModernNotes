namespace ModernNotes.Web.Contracts
{
	public class Note
	{
		public Note(int id, string text)
		{
			Id = id;
			Text = text;
		}

		public int Id { get; private set; }
		public string Text { get; private set; }
	}
}