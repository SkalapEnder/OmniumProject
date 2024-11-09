using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogicComponent : ILogicComponent
{
    private Character character;
    private CharacterData characterData;

    

    public void Initialize(Character character)
    {
        this.character = character;
        characterData = this.character.CharacterData;
    }

    public void checkState(Character targetCharacter, AiState currentState)
    {


    }
}
