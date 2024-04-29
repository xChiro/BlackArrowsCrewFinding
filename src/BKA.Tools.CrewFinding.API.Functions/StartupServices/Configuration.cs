using System;

namespace BKA.Tools.CrewFinding.API.Functions.StartupServices;

public static class Configuration
{
    public static string GetEnvironmentVariable(string name)
    {
        return Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process) ?? string.Empty;
    }
}