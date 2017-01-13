using System;
using TechTalk.SpecFlow;

namespace HiringBDD.StepDefinitions
{
    [Binding]
    public class LoginSteps
    {
        [Given(@"I enter valid ""(.*)"" or ""(.*)""")]
        public void GivenIEnterValidOr(string p0, string p1)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"I enter invalid ""(.*)"" or ""(.*)""")]
        public void GivenIEnterInvalidOr(string p0, string p1)
        {
            ScenarioContext.Current.Pending();
        }
    }
}
