using GalaSoft.MvvmLight.Views;
using ModernNotes.Specs.Helpers;
using ModernNotes.WpfClient.Main;
using Moq;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ModernNotes.Specs
{
	[Binding]
	public class WriteNewNoteSteps
	{
		[Given(@"I have started the client app")]
		[When(@"I have started the client app")]
		public void GivenIStartedTheClientApp()
		{
			var notesService = ScenarioContext.Current.GetOrDefault<FakeNotesService>("notesService");
			var viewModel = new MainViewModel(
				notesService ?? new FakeNotesService(),
				new Mock<IDialogService>().Object);
			viewModel.LoadedCommand.Execute(null);
			ScenarioContext.Current["viewmodel"] = viewModel;
		}

		[Given(@"I have pressed the New button")]
		public void GivenIHavePressedTheNewButton()
		{
			var viewModel = ScenarioContext.Current.GetOrDefault<MainViewModel>("viewmodel");
			viewModel.NewCommand.Execute(null);
		}

		[When(@"I write a new note")]
		public void WhenIWriteANewNote()
		{
			var viewModel = ScenarioContext.Current.GetOrDefault<MainViewModel>("viewmodel");
			viewModel.NoteText = "Test";
		}

		[When(@"I press the Save button")]
		public void WhenIPressTheSaveButton()
		{
			var viewModel = ScenarioContext.Current.GetOrDefault<MainViewModel>("viewmodel");
			viewModel.SaveCommand.Execute(null);
		}

		[Then(@"the list of notes should contain (.*) note")]
		public void ThenTheListOfNotesShouldContainOneNote(int num)
		{
			var viewModel = ScenarioContext.Current.GetOrDefault<MainViewModel>("viewmodel");
			Assert.AreEqual(num, viewModel.Notes.Count);
		}
	}
}
