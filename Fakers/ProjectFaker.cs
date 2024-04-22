using Bogus;
using FinalWork.Models.API;

namespace FinalWork.Fakers;

public class ProjectFaker : Faker<Project>
{
    private static Random random = new();

    private static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
    
    /// <summary>
    /// Генерируем тестовые данные
    /// </summary>
    public ProjectFaker()
    {
        RuleFor(b => b.Title, f => "Project " + f.PickRandom(RandomString(10)));
        RuleFor(b => b.Code, f => f.PickRandom(RandomString(10))); // Project code may not be greater than 10 characters.
        RuleFor(b => b.Description, f => f.Random.Words(5));
    }
}