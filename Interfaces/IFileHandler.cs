using W4_assignment_template.Models;

namespace W4_assignment_template.Interfaces;

public interface IFileHandler
{
    List<Character> ReadCharacters(string filePath);
    void WriteCharacters(string filePath, List<Character> characters);

    void DisplayCharacterNamesMenu();
    Character FindCharacter(string choice);
    void DisplayCharacters();
    void AddCharacter(string newCharacter, string newClass, string[] choicesArray);
}