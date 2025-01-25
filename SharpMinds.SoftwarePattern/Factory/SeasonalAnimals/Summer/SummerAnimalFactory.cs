namespace SharpMinds.SoftwarePattern.Factory.SeasonalAnimals.Summer;

public class SummerAnimalFactory : ISeasonalAnimalFactory
{
    public IAnimal CreateAnimal(AnimalType animalType) =>
        animalType switch
        {
            AnimalType.Dog => new SummerDog(),
            AnimalType.Cat => new SummerCat(),
            _ => throw new ArgumentOutOfRangeException(nameof(animalType), animalType, null)
        };
}