using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFactory : MonoBehaviour
{
    [SerializeField] private Character playerCharacterPrefab;
    [SerializeField] private Character enemyCharacterPrefab;

    private Dictionary<CharacterType, Queue<Character>> disabledCharacters =
        new Dictionary<CharacterType, Queue<Character>>();

    public Dictionary<CharacterType, Queue<Character>> DisabledCharacters => disabledCharacters;

    private List<Character> activeCharacters = new List<Character>();

    public List<Character> ActiveCharacters => activeCharacters;

    public Character PlayerCharacter
    {
        get; private set;
    }
    

    public Character GetCharacter(CharacterType type)
    {
        Character character = null;
        if (disabledCharacters.ContainsKey(type))
        {
            if (disabledCharacters[type].Count > 0)
            {
                character = disabledCharacters[type].Dequeue();
            }
        }
        else
        {
            disabledCharacters.Add(type, new Queue<Character>());
        }

        if(character == null)
        {
            character = InstantiateCharacter(type);
        }

        activeCharacters.Add(character);
        return character;
    }

    public void DeleteAllCharacters()
    {
        foreach (Character character in activeCharacters)
        {
            Destroy(character.gameObject);
        }
        activeCharacters.Clear();

        foreach (var queue in disabledCharacters.Values)
        {
            while (queue.Count > 0)
            {
                Destroy(queue.Dequeue().gameObject);
            }
        }
        disabledCharacters.Clear();
    }

    public void ReturnCharacter(Character character)
    {
        Queue<Character> characters = disabledCharacters[character.CharacterType];
        characters.Enqueue(character); 

        activeCharacters.Remove(character);
    }

    private Character InstantiateCharacter(CharacterType type)
    {
        Character character = null;
        switch (type){
            case CharacterType.Player:
                character = GameObject.Instantiate(playerCharacterPrefab, null);
                PlayerCharacter = character;
                break;
            case CharacterType.EnemyDefault:
                character = GameObject.Instantiate(enemyCharacterPrefab, null);
                break;
            default:
                Debug.LogError("Unknown character type: " + type);
                break;
        }

        return character;
    }
}
