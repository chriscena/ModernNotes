using System;
using System.Linq;
using ModernNotes.Specs.Helpers;
using ModernNotes.WpfClient.Main;
using TechTalk.SpecFlow;

namespace ModernNotes.Specs
{
    [Binding]
    public class UpdateNoteSteps
    {
        [When(@"I select a note with the text ""(.*)""")]
        public void WhenISelectANoteWithTheText(string noteText)
        {
			var viewmodel = ScenarioContext.Current.GetOrDefault<MainViewModel>("viewmodel");
	        viewmodel.SelectedNote = viewmodel.Notes.Single(n => n.Text == noteText);
        }
        
        [When(@"I press the Edit button")]
        public void WhenIPressTheEditButton()
        {
			var viewmodel = ScenarioContext.Current.GetOrDefault<MainViewModel>("viewmodel");
			viewmodel.EditCommand.Execute(null);
		}
        
        [When(@"I change the text of the note to ""(.*)""")]
        public void WhenIChangeTheTextOfTheNoteTo(string noteText)
		{
			var viewmodel = ScenarioContext.Current.GetOrDefault<MainViewModel>("viewmodel");
			viewmodel.NoteText = noteText;
		}
    }
}
