using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogicComponent : ILogicComponent
{
    private Character character;
    private CharacterData characterData;

    private float cooldownAttack = 0.0f;
    private Vector3 direction;
    private float _range;

    public void Initialize(Character character)
    {
        this.character = character;
        characterData = character.CharacterData;
        _range = character.DamageComponent.AttackRange;
    }

    public void checkState(Character targetCharacter, ref AiState currentState)
    {
        // Condition to check target's death
        if (!targetCharacter.LiveComponent.IsAlive)
        {
            currentState = AiState.None; return;
        }

        float distance = Vector3.Distance(targetCharacter.transform.position, character.transform.position);
        direction = (targetCharacter.transform.position - character.transform.position).normalized;

        switch (currentState)
        {
            case AiState.None:
                break;

            case AiState.Idle:
                if (distance < 10)
                {
                    currentState = AiState.MoveToTarget;
                }
                break;

            case AiState.MoveToTarget:
                character.MovementComponent.Move(direction);
                character.MovementComponent.Rotation(direction);

                if (distance >= 10)
                {
                    currentState = AiState.Idle;
                }
                else if (distance < _range)
                {
                    currentState = AiState.Attack;
                }
                break;

            case AiState.Attack:
                character.MovementComponent.Rotation(direction);

                if (cooldownAttack <= 0)
                {
                    character.DamageComponent.MakeDamage(targetCharacter);
                    cooldownAttack = characterData.TimeBetweenAttacks;
                }

                cooldownAttack = Mathf.Max(0, cooldownAttack - Time.deltaTime);

                if (distance >= _range)
                {
                    currentState = AiState.MoveToTarget;
                }
                break;
        }
    }
}
