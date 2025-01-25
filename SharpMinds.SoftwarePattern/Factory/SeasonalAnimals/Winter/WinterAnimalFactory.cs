namespace SharpMinds.SoftwarePattern.Factory.SeasonalAnimals.Winter;

public class WinterAnimalFactory: ISeasonalAnimalFactory
{
    public IAnimal CreateAnimal(AnimalType animalType) =>
        animalType switch
        {
            AnimalType.Dog => new WinterDog(),
            AnimalType.Cat => new WinterCat(),
            _ => throw new ArgumentOutOfRangeException(nameof(animalType), animalType, null)
        };
}