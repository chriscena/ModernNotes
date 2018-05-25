namespace ModernNotes.Web.Contracts
{
	/// <summary>
	/// Saved Note
	/// </summary>
	public class Note
	{
		private Note()
		{

		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <param name="text"></param>
		public Note(int id, string text)
		{
			Id = id;
			Text = text;
		}

		/// <summary>
		/// The Id of the Note
		/// </summary>
		public int Id { get; set; }
		/// <summary>
		/// The text of the Note
		/// </summary>
		public string Text { get; set; }
	}
}