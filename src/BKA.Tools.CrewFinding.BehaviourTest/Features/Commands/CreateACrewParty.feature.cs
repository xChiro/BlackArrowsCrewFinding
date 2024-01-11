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
namespace BKA.Tools.CrewFinding.BehaviourTest.Features.Commands
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class APlayerWantsToCreateACrewParty_Feature : object, Xunit.IClassFixture<APlayerWantsToCreateACrewParty_Feature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private string[] _featureTags = ((string[])(null));
        
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
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features/Commands", "A player wants to create a crew party.", null, ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void TestInitialize()
        {
        }
        
        public virtual void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
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
        [Xunit.InlineDataAttribute("Rowan", new string[0])]
        public virtual void SuccessfulCreationOfACrewParty(string userName, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("UserName", userName);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Successful creation of a Crew Party", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 3
    this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
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
                            "MaxCrewSize",
                            "Languages",
                            "System",
                            "PlanetarySystem",
                            "Planet/Moon",
                            "Location",
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
        testRunner.Then(string.Format("the Crew Party named {0}\'s Crew is successfully created with the specified detail" +
                            "s", userName), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 9
        testRunner.And(string.Format("{0} is designated as the Captain", userName), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 10
        testRunner.And("the creation date is the current date", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableTheoryAttribute(DisplayName="Creation of a Crew Party with default name")]
        [Xunit.TraitAttribute("FeatureTitle", "A player wants to create a crew party.")]
        [Xunit.TraitAttribute("Description", "Creation of a Crew Party with default name")]
        [Xunit.InlineDataAttribute("Rowan", "\'Rowan\'s Crew\'", new string[0])]
        [Xunit.InlineDataAttribute("James", "\'James\' Crew\'", new string[0])]
        public virtual void CreationOfACrewPartyWithDefaultName(string captainName, string expectedCrewName, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("CaptainName", captainName);
            argumentsOfScenario.Add("ExpectedCrewName", expectedCrewName);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Creation of a Crew Party with default name", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 16
    this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 17
        testRunner.Given(string.Format("a player named {0}", captainName), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 18
        testRunner.When("the player creates a Crew Party with no name", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 19
        testRunner.Then(string.Format("the Crew Party is successfully created with the default name {0}", expectedCrewName), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableTheoryAttribute(DisplayName="Preventing the creation of multiple active Crew Parties")]
        [Xunit.TraitAttribute("FeatureTitle", "A player wants to create a crew party.")]
        [Xunit.TraitAttribute("Description", "Preventing the creation of multiple active Crew Parties")]
        [Xunit.InlineDataAttribute("Rowan", new string[0])]
        public virtual void PreventingTheCreationOfMultipleActiveCrewParties(string userName, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("UserName", userName);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Preventing the creation of multiple active Crew Parties", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 26
    this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 27
        testRunner.Given(string.Format("a player named {0}", userName), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 28
        testRunner.And("the player already has an active Crew Party", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 29
        testRunner.When("the player attempts to create a new Crew Party named \'The Stellar Hunters\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 30
        testRunner.Then("the creation of the new Crew Party is prevented", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 31
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
        public virtual void CreationOfACrewPartyWithDefaultLocationInformation(string userName, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("UserName", userName);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Creation of a Crew Party with default location information", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 37
    this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 38
        testRunner.Given(string.Format("a player named {0}", userName), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
                TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                            "System",
                            "PlanetarySystem",
                            "Planet/Moon",
                            "Location"});
                table2.AddRow(new string[] {
                            "Stanton",
                            "Crusader",
                            "Crusader",
                            "Seraphim Station"});
#line 39
        testRunner.And("the default location is the following", ((string)(null)), table2, "And ");
#line hidden
#line 42
        testRunner.When("the player creates a Crew Party named \'The Stellar Hunters\' with missing location" +
                        " information", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 43
        testRunner.Then(string.Format("the Crew Party named {0}\'s Crew is successfully created with the default location" +
                            " information", userName), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableTheoryAttribute(DisplayName="Preventing creation of a crew party with default activity information")]
        [Xunit.TraitAttribute("FeatureTitle", "A player wants to create a crew party.")]
        [Xunit.TraitAttribute("Description", "Preventing creation of a crew party with default activity information")]
        [Xunit.InlineDataAttribute("Rowan", "Trading", new string[0])]
        public virtual void PreventingCreationOfACrewPartyWithDefaultActivityInformation(string userName, string defaultActivity, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("UserName", userName);
            argumentsOfScenario.Add("Default Activity", defaultActivity);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Preventing creation of a crew party with default activity information", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 49
    this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 50
        testRunner.Given(string.Format("a player named {0}", userName), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 51
        testRunner.And("the default activity is <Trading>", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 52
        testRunner.When("the player attempts to create a Crew Party named \'The Stellar Hunters\' with missi" +
                        "ng activity information", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 53
        testRunner.Then("the creation of the Crew Party is prevented", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 54
        testRunner.And("the player receives a message \'Incomplete information provided. Crew Party not cr" +
                        "eated.\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableTheoryAttribute(DisplayName="Creating a Crew Party with default crew size")]
        [Xunit.TraitAttribute("FeatureTitle", "A player wants to create a crew party.")]
        [Xunit.TraitAttribute("Description", "Creating a Crew Party with default crew size")]
        [Xunit.InlineDataAttribute("Rowan", "4", new string[0])]
        public virtual void CreatingACrewPartyWithDefaultCrewSize(string userName, string defaultMaxCrewSize, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("UserName", userName);
            argumentsOfScenario.Add("DefaultMaxCrewSize", defaultMaxCrewSize);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Creating a Crew Party with default crew size", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 60
    this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
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
        testRunner.And(string.Format("the default MaxCrewSize is {0}", defaultMaxCrewSize), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 63
        testRunner.When("the player attempts to create a Crew Party named \'The Stellar Hunters\' with missi" +
                        "ng MaxCrewSize", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 64
        testRunner.Then(string.Format("the Crew Party named {0}\'s Crew is successfully created", userName), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 65
        testRunner.And(string.Format("the MaxCrewSize is set to {0}", defaultMaxCrewSize), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 66
        testRunner.And(string.Format("{0} is designated as the Captain", userName), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableTheoryAttribute(DisplayName="Creating a Crew Party with default languages")]
        [Xunit.TraitAttribute("FeatureTitle", "A player wants to create a crew party.")]
        [Xunit.TraitAttribute("Description", "Creating a Crew Party with default languages")]
        [Xunit.InlineDataAttribute("Rowan", "EN", new string[0])]
        public virtual void CreatingACrewPartyWithDefaultLanguages(string userName, string defaultLanguage, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("UserName", userName);
            argumentsOfScenario.Add("DefaultLanguage", defaultLanguage);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Creating a Crew Party with default languages", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 72
    this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 73
        testRunner.Given(string.Format("a player named {0}", userName), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 74
        testRunner.And(string.Format("the default Language is {0}", defaultLanguage), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 75
        testRunner.When("the player attempts to create a Crew Party named \'The Stellar Hunters\' with missi" +
                        "ng languages", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 76
        testRunner.Then(string.Format("the Crew Party named {0}\'s Crew is successfully created", userName), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 77
        testRunner.And(string.Format("the default Language is set to {0}", defaultLanguage), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 78
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
