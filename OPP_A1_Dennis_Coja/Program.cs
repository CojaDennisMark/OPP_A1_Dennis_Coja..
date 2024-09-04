using System;
using System.Collections.Generic;

public enum Kind { Dog, Cat, Lizard, Bird }
public enum Gender { Male, Female }

public abstract class Pet
{
    public Gender Gender { get; set; }
    public string Name { get; set; }
    public string Owner { get; set; }

    protected Pet(Gender gender, string name, string owner)
    {
        Gender = gender;
        Name = name;
        Owner = owner;
    }

    public override string ToString()
    {
        return $"{GetType().Name}: Name = {Name}, Gender = {Gender}, Owner = {Owner}";
    }
}

public class Dog : Pet
{
    public string Breed { get; set; }

    public Dog(Gender gender, string name, string breed, string owner)
        : base(gender, name, owner)
    {
        Breed = breed;
    }

    public override string ToString()
    {
        return $"{GetType().Name}: Name = {Name}, Gender = {Gender}, Owner = {Owner}, Breed = {Breed}";
    }
}

public class Cat : Pet
{
    public bool IsLonghaired { get; set; }

    public Cat(Gender gender, string name, bool isLonghaired, string owner)
        : base(gender, name, owner)
    {
        IsLonghaired = isLonghaired;
    }

    public override string ToString()
    {
        return base.ToString() + $", Longhaired = {IsLonghaired}";
    }
}

public class Lizard : Pet
{
    public bool CanFly { get; set; }

    public Lizard(Gender gender, string name, bool canFly, string owner)
        : base(gender, name, owner)
    {
        CanFly = canFly;
    }

    public override string ToString()
    {
        return base.ToString() + $", Can Fly = {CanFly}";
    }
}

public class Bird : Pet
{
    public bool CanFly { get; set; }

    public Bird(Gender gender, string name, bool canFly, string owner)
        : base(gender, name, owner)
    {
        CanFly = canFly;
    }

    public override string ToString()
    {
        return base.ToString() + $", Can Fly = {CanFly}";
    }
}

public class Program
{
    private static List<Pet> pets = new List<Pet>();

    public static void Main()
    {
        while (true)
        {
            Console.WriteLine("1. Add Pet");
            Console.WriteLine("2. List All Pets");
            Console.WriteLine("3. Exit");
            Console.Write("Choose an option: ");

            var choice = Console.ReadLine().Trim();

            switch (choice)
            {
                case "1":
                    AddPet();
                    break;
                case "2":
                    ListAllPets();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Invalid choice, try again.");
                    break;
            }
        }
    }

    private static void AddPet()
    {
        try
        {
            Kind kind = GetPetKind();
            Gender gender = GetGender();
            string name = GetName();
            string owner = GetOwner();

            Pet pet = CreatePet(kind, gender, name, owner);

            if (pet != null)
            {
                Console.WriteLine($"You are about to add the following pet:\n{pet}");
                if (ConfirmAddition())
                {
                    pets.Add(pet);
                    Console.WriteLine("Pet added successfully.");
                }
                else
                {
                    Console.WriteLine("Pet addition cancelled.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    private static void ListAllPets()
    {
        if (pets.Count == 0)
        {
            Console.WriteLine("No pets in the inventory.");
            return;
        }

        foreach (var pet in pets)
        {
            Console.WriteLine(pet);
        }
    }

    private static Kind GetPetKind()
    {
        Console.Write("Enter the kind of pet (Dog, Cat, Lizard, Bird): ");
        if (!Enum.TryParse(Console.ReadLine()?.Trim(), true, out Kind kind))
        {
            throw new ArgumentException("Invalid pet kind. Please enter Dog, Cat, Lizard, or Bird.");
        }
        return kind;
    }

    private static Gender GetGender()
    {
        Console.Write("Enter the gender (Male, Female): ");
        if (!Enum.TryParse(Console.ReadLine()?.Trim(), true, out Gender gender))
        {
            throw new ArgumentException("Invalid gender. Please enter Male or Female.");
        }
        return gender;
    }

    private static string GetName()
    {
        Console.Write("Enter the name of the pet: ");
        var name = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Pet name cannot be empty.");
        }
        return name;
    }

    private static string GetOwner()
    {
        Console.Write("Enter the owner of the pet: ");
        var owner = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(owner))
        {
            throw new ArgumentException("Owner cannot be empty.");
        }
        return owner;
    }

    private static Pet CreatePet(Kind kind, Gender gender, string name, string owner)
    {
        switch (kind)
        {
            case Kind.Dog:
                Console.Write("Enter the breed of the dog: ");
                var breed = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(breed))
                {
                    throw new ArgumentException("Breed cannot be empty.");
                }
                return new Dog(gender, name, breed, owner);

            case Kind.Cat:
                Console.Write("Is the cat longhaired (yes/no)? ");
                if (!TryParseYesNo(Console.ReadLine()?.Trim(), out bool isLonghaired))
                {
                    throw new ArgumentException("Invalid longhaired status. Please enter yes or no.");
                }
                return new Cat(gender, name, isLonghaired, owner);

            case Kind.Lizard:
                Console.Write("Can the lizard fly (yes/no)? ");
                if (!TryParseYesNo(Console.ReadLine()?.Trim(), out bool canFly))
                {
                    throw new ArgumentException("Invalid fly status. Please enter yes or no.");
                }
                return new Lizard(gender, name, canFly, owner);

            case Kind.Bird:
                Console.Write("Can the bird fly (yes/no)? ");
                if (!TryParseYesNo(Console.ReadLine()?.Trim(), out bool canFlyBird))
                {
                    throw new ArgumentException("Invalid fly status. Please enter yes or no.");
                }
                return new Bird(gender, name, canFlyBird, owner);

            default:
                throw new ArgumentException("Unexpected pet kind.");
        }
    }

    private static bool TryParseYesNo(string input, out bool result)
    {
        input = input?.Trim().ToLower();
        if (input == "yes")
        {
            result = true;
            return true;
        }
        if (input == "no")
        {
            result = false;
            return true;
        }
        result = false;
        return false;
    }

    private static bool ConfirmAddition()
    {
        Console.Write("Do you want to proceed? (y/n): ");
        var confirmation = Console.ReadLine()?.Trim().ToLower();
        return confirmation == "y";
    }
}
