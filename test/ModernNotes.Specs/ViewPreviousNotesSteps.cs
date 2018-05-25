using System;
using ModernNotes.Specs.Helpers;
using ModernNotes.WpfClient.Main;
using ModernNotes.WpfClient.Models;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace ModernNotes.Specs
{
    [Binding]
    public class ViewPreviousNotesSteps
    {
        [Given(@"I have the following notes saved")]
        public void GivenIHaveTheFollowingNotesSaved(Table table)
        {
	        var notes = table.CreateSet<Note>();
			var notesService = new FakeNotesService();
			notesService.Notes.AddRange(notes);
	        ScenarioContext.Current["notesService"] = notesService;
        }
        
        [Then(@"the list of notes should contain the following")]
        public void ThenTheResultShouldBe(Table table)
        {
	        var viewmodel = ScenarioContext.Current.GetOrDefault<MainViewModel>("viewmodel");
			table.CompareToSet(viewmodel.Notes);
        }
    }
}
