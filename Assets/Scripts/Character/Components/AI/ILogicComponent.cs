using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILogicComponent : ICharacterComponent
{
    void checkState(Character targetCharacter, ref AiState currentState);
}
