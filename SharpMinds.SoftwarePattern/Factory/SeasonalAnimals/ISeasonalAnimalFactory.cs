namespace SharpMinds.SoftwarePattern.Factory.SeasonalAnimals;

public interface ISeasonalAnimalFactory
{
    IAnimal CreateAnimal(AnimalType animalType);
}

public enum AnimalType
{
    Dog,
    Cat
}