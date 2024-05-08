using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Commands.Creators;
using BKA.Tools.CrewFinding.Players.Exceptions;
using BKA.Tools.CrewFinding.Tests.Crews.Commands.Creators.Utilities;
using BKA.Tools.CrewFinding.Tests.Crews.Mocks;

namespace BKA.Tools.CrewFinding.Tests.Crews.Commands.Creators;

public class CrewCreatorTest
{
    [Theory]
    [InlineData("Rowan", "Crew of Rowan")]
    [InlineData("James", "Crew of James")]
    public async Task Create_Crew_Forms_Correct_Crew_Name(string captainName, string expectedCrewName)
    {
        // Arrange
        var captainId = Guid.NewGuid().ToString();
        var crewPartyCommandsMock = new CrewCommandRepositoryMock();
        var playerQueriesMock = new PlayerQueryRepositoryValidationMock(captainId, captainName);
        var sut = CrewCreatorBuilder.Build(crewPartyCommandsMock, playerQueriesMock,
            playerId: captainId);

        // Act
        await ExecuteCrewCreation(sut, captainId);

        // Assert
        crewPartyCommandsMock.Name!.Value.Should().Be(expectedCrewName);
    }

    [Fact]
    public async Task Try_To_Create_A_Crew_But_Captain_Not_Was_Found()
    {
        // Arrange
        var captainId = Guid.NewGuid().ToString();
        var crewPartyCommandsMock = new CrewCommandRepositoryMock();
        var playerQueriesMock = new PlayerQueryRepositoryValidationMock(captainId, "Rowan");
        var sut = CrewCreatorBuilder.Build(crewPartyCommandsMock, playerQueriesMock);

        // Act
        var act = async () => await ExecuteCrewCreation(sut, Guid.NewGuid().ToString());

        // Assert
        await act.Should().ThrowAsync<PlayerNotFoundException>();
    }

    [Fact]
    public async Task Create_Crew_Assigns_Captain_Successfully()
    {
        // Arrange
        const string captainName = "Rowan";
        const int maxCrewAllowed = 3;

        var captainId = Guid.NewGuid().ToString();
        var createCrewPartyResultMock = new CrewCommandRepositoryMock();

        var sut = CrewCreatorBuilder.Build(createCrewPartyResultMock,
            captainName: captainName);

        // Act
        await ExecuteCrewCreation(sut, captainId, maxCrewAllowed);

        // Assert
        createCrewPartyResultMock.Captain.Should().NotBeNull();
        createCrewPartyResultMock.Captain!.CitizenName.Should().Be(captainName);
        createCrewPartyResultMock.Captain!.Id.Should().NotBeNullOrEmpty();
        createCrewPartyResultMock.Members.Should().BeEmpty();
        createCrewPartyResultMock.MaxMembersAllowed.Should().Be(maxCrewAllowed);
        createCrewPartyResultMock.Active = true;
    }

    [Fact]
    public async Task Create_Crew_With_Current_Date()
    {
        // Arrange
        var captainId = Guid.NewGuid().ToString();
        var createCrewPartyResultMock = new CrewCommandRepositoryMock();
        var sut = CrewCreatorBuilder.Build(createCrewPartyResultMock);

        // Act
        await ExecuteCrewCreation(sut, captainId);

        // Assert
        createCrewPartyResultMock.CreationDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public async Task Create_Crew_With_Description_Assigns_Description()
    {
        // Arrange
        var captainId = Guid.NewGuid().ToString();
        const string description = "This is a description";
        var createCrewPartyResultMock = new CrewCommandRepositoryMock();
        var crewPartyCreatorResponseMock = new CrewCreatorResponseMock();
        var sut = CrewCreatorBuilder.Build(createCrewPartyResultMock);

        // Act
        await ExecuteCrewCreation(sut, captainId, 4, description, crewPartyCreatorResponseMock);

        // Assert
        createCrewPartyResultMock.Activity!.Description.Should().Be(description);
        crewPartyCreatorResponseMock.Id.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Create_Crew_With_Max_Crew_Size_Allowed_Then_Crew_Is_Created_With_Max_Crew_Allowed_By_System()
    {
        // Arrange
        var captainId = Guid.NewGuid().ToString();
        const int maxCrewAllowed = 5;
        var createCrewPartyResultMock = new CrewCommandRepositoryMock();
        var sut = CrewCreatorBuilder.Build(createCrewPartyResultMock,
            maxPlayersAllowed: maxCrewAllowed);

        // Act
        await ExecuteCrewCreation(sut, captainId, 10);

        // Assert
        createCrewPartyResultMock.MaxMembersAllowed.Should().Be(maxCrewAllowed);
    }

    private static async Task ExecuteCrewCreation(ICrewCreator sut, string captainId, int totalCrew = 4)
    {
        await ExecuteCrewCreation(sut, captainId, totalCrew, string.Empty, new CrewCreatorResponseMock());
    }

    private static async Task ExecuteCrewCreation(ICrewCreator sut, string captainId, int totalCrew,
        string description, ICrewCreatorResponse crewCreatorResponse)
    {
        await CrewCreatorExecutioner.Execute(sut, crewCreatorResponse,
            Array.Empty<string>(), Location.DefaultLocation(), "Mining", totalCrew, description);
    }
}