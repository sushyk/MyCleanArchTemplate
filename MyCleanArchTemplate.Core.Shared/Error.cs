namespace MyCleanArchTemplate.Core.Shared;

public sealed record Error(string Code, string? Descrition = null)
{
    public static readonly Error None = new(string.Empty);
}