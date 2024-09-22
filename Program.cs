using W4_assignment_template.Interfaces;
using W4_assignment_template.Models;
using W4_assignment_template.Services;

namespace W4_assignment_template;

class Program
{
    static IFileHandler fileHandler;
    static List<Character> characters;
    static EquipmentManager equipmentManager;
    static CharcaterClassManager characterClassManager;    

    static void Main()
    {
        string filePath = "Files/input.csv"; // Default to CSV file
        fileHandler = new CsvFileHandler(); // Default to CSV handler
        characters = fileHandler.ReadCharacters(filePath);
        equipmentManager = new EquipmentManager();
        characterClassManager = new CharcaterClassManager();

        while (true)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Display Characters");
            Console.WriteLine("2. Add Character");
            Console.WriteLine("3. Level Up Character");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    DisplayAllCharacters();
                    break;
                case "2":
                    AddCharacter();
                    break;
                case "3":
                    LevelUpCharacter();
                    break;
                case "4":
                    fileHandler.WriteCharacters(filePath, characters);
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void DisplayAllCharacters()
    {
        fileHandler.DisplayCharacters();
    }

    static void AddCharacter()
    {
        Console.Write("Enter the name for your new character: ");
        string newCharacter = Console.ReadLine();

        Console.WriteLine($"Pick you character's class from the menu below: ");
        characterClassManager.DisplayCharacterClassMenu();
        Console.Write("Enter Your Choice: ");
        int newClassIndex = Convert.ToInt16(Console.ReadLine()) - 1;

        string[] characterClassOptions = characterClassManager.CharacterClassOptions;

        string newClass = characterClassOptions[newClassIndex];
        
        Console.WriteLine("Select 3 tools from the menu below: ");

        string[] equipmentOptions = equipmentManager.EquipmentOptions;
        int countEquipChoicesLeft = 3;
        List<string> choicesList = new List<string>();

        while (countEquipChoicesLeft > 0)
        {
            equipmentManager.DisplayEquipmentMenu();
            
            int choice = Convert.ToInt16(Console.ReadLine());
            int mappedToIndexChoice = choice - 1;
            string choiceName = equipmentOptions[mappedToIndexChoice];

            choicesList.Add(choiceName);

            countEquipChoicesLeft -= 1;
        }

        string[] choicesArray = choicesList.ToArray();
        string choicesString = string.Join(", ", choicesArray);

        Console.WriteLine($"You've chosen the following equipment: {choicesString}");

        // Append the new character to the lines array

        string pipeDelimitedChoicesString = string.Join("|", choicesArray);
        string lineToAppend = $"{newCharacter},{newClass},{1},{10},{pipeDelimitedChoicesString}"; // automatically level 1 and 10 hit points
        Console.WriteLine(lineToAppend);
        
        characters.Add(new Character()
            {
                CharacterName = newCharacter,
                CharacterClass = newClass,
                CharacterLevel = 1,
                CharacterHitPoints = 10,
                CharacterEquipment = choicesArray
            });    
    }

    static void LevelUpCharacter()
    {
        Console.WriteLine("Select the character to level up: ");

        fileHandler.DisplayCharacterNamesMenu();
        
        Console.Write("Enter Your Choice: ");
        string choice = Console.ReadLine();
                
        var foundCharacter = fileHandler.FindCharacter(choice);

        if (foundCharacter != null)
        {
             Console.WriteLine($"You've Chosen to Level Up {foundCharacter.CharacterName}");
            foundCharacter.CharacterLevel++;
        }
        else
        {
            Console.WriteLine("Character not found.");
        }
    }

    public class CharcaterClassManager
{
    public string[] CharacterClassOptions {get;set;} = {"Paladin", "Warrior", "Rogue", "Warlock"};

    public void DisplayCharacterClassMenu()
    {
        for (int i = 0; i < CharacterClassOptions.Length; i++)
        {
            Console.WriteLine($"{i+1}: {CharacterClassOptions[i]}");
        }
    }
}

    public class EquipmentManager
{
    public string[] EquipmentOptions {get;set;} = {"Armor","Book","Cloak","Dagger","Horse","Lockpick","Mace","Health Potion","Robe","Shield","Staff","Sword"};

    
    public void DisplayEquipmentMenu()
    {
        for (int i = 0; i < EquipmentOptions.Length; i++)
            {
                Console.WriteLine($"{i+1}: {EquipmentOptions[i]}");
            }
    }
}
 
}