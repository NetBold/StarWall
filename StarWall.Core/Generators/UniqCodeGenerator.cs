namespace StarWall.Core.Generators;

public static class UniqCodeGenerator
{
    public static string GenerateUniqCode()
    {
        return Guid.NewGuid().ToString().Replace("-", "");
    }
}