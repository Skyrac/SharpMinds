using SharpMinds.SoftwarePattern.Factory.SeasonalAnimals;
using SharpMinds.SoftwarePattern.Factory.SeasonalAnimals.Summer;
using SharpMinds.SoftwarePattern.Factory.SeasonalAnimals.Winter;

namespace SharpMinds.Unit.Tests.SoftwarePattern.Factory;

public class SeasonalFactoryTests
{
    [Test]
    [Arguments(Season.Summer, AnimalType.Dog, typeof(SummerDog))]
    [Arguments(Season.Summer, AnimalType.Cat, typeof(SummerCat))]
    [Arguments(Season.Winter, AnimalType.Cat, typeof(WinterCat))]
    [Arguments(Season.Winter, AnimalType.Dog, typeof(WinterDog))]
    public async Task
        CreateAnimal_ShouldReturnRelevantAnimal_WhenTypeEqualsRelatedAnimalTypeAndSeason(
            Season season,
            AnimalType animal, 
            Type serviceType)
    {
        var factory = SeasonalFactoryCreator.Create(season);
        var service = factory.CreateAnimal(animal);
        await Assert.That(service.GetType()).IsEqualTo(serviceType);
    }
}