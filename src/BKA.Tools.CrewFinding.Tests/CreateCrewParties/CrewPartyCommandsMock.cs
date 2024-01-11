using System;

namespace BKA.Tools.CrewFinding.Tests.CreateCrewParties;

public class CrewPartyCommandsMock : ICrewPartyCommands
{
    public CrewName? Name { get; private set; }
    public Location? StartingPlace { get; private set; }
    public LanguageCollections? Languages { get; private set; }
    public MaxCrewNumber? MaxCrewNumber { get; private set; }
    public Activity? Activity { get; private set; }
    public Captain? Captain { get; private set; }
    public DateTime? CreationDate { get; private set; }

    public void SaveCrewParty(Captain captain, CrewParty crewParty)
    {
        Name = crewParty.Name;
        StartingPlace = crewParty.ReunionPoint;
        Languages = crewParty.Languages;
        MaxCrewNumber = crewParty.MaxCrewNumber;
        Activity = crewParty.Activity;
        Captain = captain;
        CreationDate = crewParty.CreationDate;
    }
}