﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.9.0.0
//      SpecFlow Generator Version:3.9.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Features.Commands
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class APlayerWantsToCreateACrewParty_Feature : object, Xunit.IClassFixture<APlayerWantsToCreateACrewParty_Feature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "CreateACrewParty.feature"
#line hidden
        
        public APlayerWantsToCreateACrewParty_Feature(APlayerWantsToCreateACrewParty_Feature.FixtureData fixtureData, BKA_Tools_CrewFinding_BehaviourTest_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "CrewParties/Features/Commands", "A player wants to create a crew party.", null, ProgrammingLanguage.CSharp, featureTags);
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public void TestInitialize()
        {
        }
        
        public void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        void System.IDisposable.Dispose()
        {
            this.TestTearDown();
        }
        
        [Xunit.SkippableTheoryAttribute(DisplayName="Successful creation of a Crew Party")]
        [Xunit.TraitAttribute("FeatureTitle", "A player wants to create a crew party.")]
        [Xunit.TraitAttribute("Description", "Successful creation of a Crew Party")]
        [Xunit.InlineDataAttribute("Rowan", "Rowan\'s CrewParty", new string[0])]
        public void SuccessfulCreationOfACrewParty(string userName, string crewPartyDefaultName, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("UserName", userName);
            argumentsOfScenario.Add("CrewPartyDefaultName", crewPartyDefaultName);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Successful creation of a Crew Party", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 3
    this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 4
        testRunner.Given(string.Format("a player named {0}", userName), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
                TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                            "CrewSize",
                            "Languages",
                            "System",
                            "PlanetarySystem",
                            "Planet/Moon",
                            "Place",
                            "Description",
                            "Activity"});
                table1.AddRow(new string[] {
                            "6",
                            "ES, EN",
                            "Stanton",
                            "Crusader",
                            "Crusader",
                            "Seraphim Station",
                            "Elite bounty hunters",
                            "Bounty Hunting"});
#line 5
        testRunner.When("the player creates a Crew Party named \'The Stellar Hunters\' with the following de" +
                        "tails:", ((string)(null)), table1, "When ");
#line hidden
#line 8
        testRunner.Then(string.Format("a Crew Party named {0} is successfully created", crewPartyDefaultName), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 9
        testRunner.And(string.Format("{0} is designated as the Captain", userName), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
                TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                            "CrewSize",
                            "Languages",
                            "System",
                            "PlanetarySystem",
                            "Planet/Moon",
                            "Place",
                            "Description",
                            "Activity"});
                table2.AddRow(new string[] {
                            "6",
                            "ES, EN",
                            "Stanton",
                            "Crusader",
                            "Crusader",
                            "Seraphim Station",
                            "Elite bounty hunters",
                            "Bounty Hunting"});
#line 10
        testRunner.And("the Crew Party contains the following details:", ((string)(null)), table2, "And ");
#line hidden
#line 13
        testRunner.And("the creation date is the current date", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableTheoryAttribute(DisplayName="Preventing the creation of multiple active Crew Parties")]
        [Xunit.TraitAttribute("FeatureTitle", "A player wants to create a crew party.")]
        [Xunit.TraitAttribute("Description", "Preventing the creation of multiple active Crew Parties")]
        [Xunit.InlineDataAttribute("Rowan", new string[0])]
        public void PreventingTheCreationOfMultipleActiveCrewParties(string userName, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("UserName", userName);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Preventing the creation of multiple active Crew Parties", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 19
    this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 20
        testRunner.Given(string.Format("a player named {0}", userName), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 21
        testRunner.And("the player already has an active Crew Party", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 22
        testRunner.When("the player attempts to create a new Crew Party named \'The Stellar Hunters\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 23
        testRunner.Then("the creation of the new Crew Party is prevented", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 24
        testRunner.And("the player receives a message \'You cannot create a new Crew Party as you already " +
                        "have an active one.\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableTheoryAttribute(DisplayName="Creation of a Crew Party with default location information")]
        [Xunit.TraitAttribute("FeatureTitle", "A player wants to create a crew party.")]
        [Xunit.TraitAttribute("Description", "Creation of a Crew Party with default location information")]
        [Xunit.InlineDataAttribute("Rowan", new string[0])]
        public void CreationOfACrewPartyWithDefaultLocationInformation(string userName, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("UserName", userName);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Creation of a Crew Party with default location information", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 30
    this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 31
        testRunner.Given(string.Format("a player named {0}", userName), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 32
        testRunner.When("the player creates a Crew Party named \'The Stellar Hunters\' with missing location" +
                        " information", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 33
        testRunner.Then(string.Format("the Crew Party named {0}\'s Crew is successfully created with the default location" +
                            " information", userName), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableTheoryAttribute(DisplayName="Create a Crew Party with missing activity information, use default activity")]
        [Xunit.TraitAttribute("FeatureTitle", "A player wants to create a crew party.")]
        [Xunit.TraitAttribute("Description", "Create a Crew Party with missing activity information, use default activity")]
        [Xunit.InlineDataAttribute("Rowan", new string[0])]
        public void CreateACrewPartyWithMissingActivityInformationUseDefaultActivity(string userName, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("UserName", userName);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Create a Crew Party with missing activity information, use default activity", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 39
    this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 40
        testRunner.Given(string.Format("a player named {0}", userName), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 41
        testRunner.When("the player attempts to create a Crew Party named \'The Stellar Hunters\' with missi" +
                        "ng activity information", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 42
        testRunner.Then("the creation of the Crew Party is created with the default activity", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableTheoryAttribute(DisplayName="Creating a Crew Party with default crew size")]
        [Xunit.TraitAttribute("FeatureTitle", "A player wants to create a crew party.")]
        [Xunit.TraitAttribute("Description", "Creating a Crew Party with default crew size")]
        [Xunit.InlineDataAttribute("Rowan", "4", new string[0])]
        public void CreatingACrewPartyWithDefaultCrewSize(string userName, string defaultMaxCrewSize, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("UserName", userName);
            argumentsOfScenario.Add("DefaultMaxCrewSize", defaultMaxCrewSize);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Creating a Crew Party with default crew size", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 48
    this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 49
        testRunner.Given(string.Format("a player named {0}", userName), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 50
        testRunner.And(string.Format("the default MaxCrewSize is {0}", defaultMaxCrewSize), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 51
        testRunner.When("the player attempts to create a Crew Party named \'The Stellar Hunters\' with missi" +
                        "ng MaxCrewSize", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 52
        testRunner.Then(string.Format("the Crew Party named {0}\'s Crew is successfully created", userName), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 53
        testRunner.And(string.Format("the MaxCrewSize is set to {0}", defaultMaxCrewSize), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 54
        testRunner.And(string.Format("{0} is designated as the Captain", userName), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableTheoryAttribute(DisplayName="Creating a Crew Party with default languages")]
        [Xunit.TraitAttribute("FeatureTitle", "A player wants to create a crew party.")]
        [Xunit.TraitAttribute("Description", "Creating a Crew Party with default languages")]
        [Xunit.InlineDataAttribute("Rowan", "EN", new string[0])]
        public void CreatingACrewPartyWithDefaultLanguages(string userName, string defaultLanguage, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("UserName", userName);
            argumentsOfScenario.Add("DefaultLanguage", defaultLanguage);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Creating a Crew Party with default languages", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 60
    this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 61
        testRunner.Given(string.Format("a player named {0}", userName), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 62
        testRunner.When("the player attempts to create a Crew Party named \'The Stellar Hunters\' with missi" +
                        "ng languages", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 63
        testRunner.Then(string.Format("the Crew Party named {0}\'s Crew is successfully created", userName), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 64
        testRunner.And("the default Language are set to the Crew Party", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 65
        testRunner.And(string.Format("{0} is designated as the Captain", userName), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                APlayerWantsToCreateACrewParty_Feature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                APlayerWantsToCreateACrewParty_Feature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
