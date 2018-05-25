using System;
using ModernNotes.Specs.Helpers;
using ModernNotes.WpfClient.Main;
using TechTalk.SpecFlow;

namespace ModernNotes.Specs
{
    [Binding]
    public class DeleteNoteSteps
    {
        [When(@"I press the Delete button")]
        public void WhenIPressTheDeleteButton()
        {
			var viewmodel = ScenarioContext.Current.GetOrDefault<MainViewModel>("viewmodel");
	        viewmodel.DeleteCommand.Execute(null);
		}
    }
}
