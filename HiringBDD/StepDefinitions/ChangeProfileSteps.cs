using System;
using TechTalk.SpecFlow;

namespace HiringBDD.StepDefinitions
{
    [Binding]
    public class ChangeProfileSteps
    {
        [When(@"I change my profile")]
        public void WhenIChangeMyProfile()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"My profile should be changed")]
        public void ThenMyProfileShouldBeChanged()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
