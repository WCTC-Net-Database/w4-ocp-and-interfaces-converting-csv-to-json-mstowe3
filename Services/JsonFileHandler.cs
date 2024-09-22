using W4_assignment_template.Interfaces;
using W4_assignment_template.Models;
using Newtonsoft.Json;

namespace W4_assignment_template.Services;

public class JsonFileHandler : IFileHandler
{
    public string CharacterLines {get;set;}
    public List<Character> CharactersList {get;set;} = new List<Character>();
    public List<string> CharacterNamesList {get;set;} // For menu of characters

    public List<Character> ReadCharacters(string filePath)
    {
        // TODO: Implement JSON reading logic
        // throw new NotImplementedException();
        CharacterLines = File.ReadAllText(filePath);
        var characters = JsonConvert.DeserializeObject<List<Character>>(CharacterLines); 
        foreach (var character in characters)
        {
            CharactersList.Add(new Character()
            {
                CharacterName = character.CharacterName,
                CharacterClass = character.CharacterClass,
                CharacterLevel = character.CharacterLevel,
                CharacterHitPoints = character.CharacterHitPoints,
                CharacterEquipment = character.CharacterEquipment
            });
        }
        return CharactersList;
    }

    public void WriteCharacters(string filePath, List<Character> characters)
    {
        // TODO: Implement JSON writing logic
        // throw new NotImplementedException();
        string json = JsonConvert.SerializeObject(characters);
        File.WriteAllText("WriteLines.txt", json);
    }

    public void DisplayCharacterNamesMenu()
    {
        CharacterNamesList = new List<string>();

        foreach (var character in CharactersList)

        {
            CharacterNamesList.Add(character.CharacterName);
        }

        for (int i = 0; i < CharacterNamesList.Count; i++)
        {
            Console.WriteLine($"{i+1}: {CharacterNamesList[i]}");
        }
    }

    public Character FindCharacter(string choice)
    {
        int indexToFind = Convert.ToInt16(choice) - 1;
        string NameToFind = CharacterNamesList[indexToFind];
        var foundCharacter = CharactersList.Where(c => c.CharacterName == NameToFind).FirstOrDefault();
        return foundCharacter;
    }

    public void DisplayCharacters()
    {
        foreach (var character in CharactersList)
            {
                Console.WriteLine($"Name: {character.CharacterName}; Class: {character.CharacterClass}; Level: {character.CharacterLevel}; Hit Points: {character.CharacterHitPoints};  Equipment: {string.Join(", ", character.CharacterEquipment)}");
            }
    }

    public void AddCharacter(string newCharacter, string newClass, string[] choicesArray)
    {
        CharactersList.Add(new Character()
            {
                CharacterName = newCharacter,
                CharacterClass = newClass,
                CharacterLevel = 1,
                CharacterHitPoints = 10,
                CharacterEquipment = choicesArray
            });

    }
}