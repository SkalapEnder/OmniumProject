using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogicComponent : ILogicComponent
{
    private AiState aiState;
    private Character character;
    private float TARGET_DISTANCE;

    private IMovable MovementComponent =>
        character.MovementComponent;

    private IAttackComponent AttackComponent =>
        character.AttackComponent;

    public void Initialize(Character character)
    {
        this.character = character;
        aiState = AiState.MoveToTarget;
        TARGET_DISTANCE = character.CharacterData.DefaultRange;
    }

    public void OnUpdate()
    {
        if (character.Target == null || !character.Target.gameObject.activeSelf)
            return;

        Vector3 direction = character.Target.transform.position
            - character.CharacterData.CharacterTransform.position;


        switch (aiState)
        {
            case AiState.Idle:

                return;

            case AiState.MoveToTarget:
                direction = direction.normalized;
                MovementComponent.Move(direction);
                MovementComponent.Rotation(direction);
                if (Vector3.Distance(character.Target.transform.position,
                        character.CharacterData.CharacterTransform.position)
                    <= TARGET_DISTANCE)
                {
                    aiState = AiState.Attack;
                }

                return;


            case AiState.Attack:
                MovementComponent.Move(Vector3.zero);

                direction = direction.normalized;
                MovementComponent.Rotation(direction);

                AttackComponent.MakeAttack();

                if (Vector3.Distance(character.Target.transform.position,
                        character.CharacterData.CharacterTransform.position)
                    > TARGET_DISTANCE)
                    aiState = AiState.MoveToTarget;
                return;

        }
    }
}
