using System;

namespace BKA.Tools.CrewFinding.Tests.Crews.Commands.Leave;

public class PlayerNotInCrewException : Exception
{
    public PlayerNotInCrewException() : base("Player is not in the crew")
    {
    }
}