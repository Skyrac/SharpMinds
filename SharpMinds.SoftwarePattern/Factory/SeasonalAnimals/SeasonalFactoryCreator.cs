using SharpMinds.SoftwarePattern.Factory.SeasonalAnimals.Summer;
using SharpMinds.SoftwarePattern.Factory.SeasonalAnimals.Winter;

namespace SharpMinds.SoftwarePattern.Factory.SeasonalAnimals;

public static class SeasonalFactoryCreator
{
    public static ISeasonalAnimalFactory Create(Season season) =>
        season switch
        {
            Season.Spring => throw new NotImplementedException(),
            Season.Summer => new SummerAnimalFactory(),
            Season.Autumn => throw new NotImplementedException(),
            Season.Winter => new WinterAnimalFactory(),
            _ => throw new ArgumentOutOfRangeException(nameof(season), season, null)
        };
}

public enum Season
{
    Spring,
    Summer,
    Autumn,
    Winter
}