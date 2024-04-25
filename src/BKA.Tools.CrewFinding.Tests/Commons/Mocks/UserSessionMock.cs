using System;
using BKA.Tools.CrewFinding.Commons.Ports;

namespace BKA.Tools.CrewFinding.Tests.Commons.Mocks;

public class UserSessionMock(string? userId = null) : IUserSession
{
    private readonly string _userId = userId ?? Guid.NewGuid().ToString();

    public string GetUserId()
    {
        return _userId;
    }
}