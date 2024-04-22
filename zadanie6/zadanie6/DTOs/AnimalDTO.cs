namespace zadanie6.DTOs;

public class AnimalDTO
{
    public record GetAllAnimals(int Id, string Name, string Description, string Category, string Area);
    public record CreateAnimalsRequest(string Name, string Description, string Category, string Areae);
}