using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class EnemyCharacter : Character
{
    [SerializeField] private Character targetCharacter;
    [SerializeField] private AiState currentState;

    private float cooldownAttack = 0.0f;
    private Vector3 direction;

    private float _range;

    public void Start()
    {
        base.Initialize();

        LiveComponent = new ImmortalLiveComponent();
        LiveComponent.Initialize(this);

        DamageComponent = new CharacterDamageComponent();
        DamageComponent.Initialize(this);

        LogicComponent = new EnemyLogicComponent();
        LogicComponent.Initialize(this);

        _range = DamageComponent.AttackRange;
    }

    public override void Update()
    {
        direction = targetCharacter.transform.position - characterData.transform.position;
        direction.Normalize();

        switch (currentState)
        {
            case AiState.None: break; // AI Disabled

            case AiState.Idle:
                if (Vector3.Distance(targetCharacter.transform.position, characterData.transform.position) < 10)
                {
                    currentState = AiState.MoveToTarget;
                    break;
                }
                //LookAround();
                break;

            case AiState.MoveToTarget:
                MovementComponent.Move(direction);
                MovementComponent.Rotation(direction);

                if (Vector3.Distance(targetCharacter.transform.position, characterData.transform.position) >= 10)
                {
                    currentState = AiState.Idle;
                    break;
                }


                if (Vector3.Distance(targetCharacter.transform.position, characterData.transform.position) < _range)
                {
                    currentState = AiState.Attack;
                    break;
                }


                break;

            case AiState.Attack:
                MovementComponent.Move(direction);
                MovementComponent.Rotation(direction);

                if (cooldownAttack <= 0)
                {
                    DamageComponent.MakeDamage(targetCharacter);
                    cooldownAttack = characterData.TimeBetweenAttacks;
                }

                if (cooldownAttack > 0)
                    cooldownAttack -= Time.deltaTime;

                if (Vector3.Distance(targetCharacter.transform.position, characterData.transform.position) >= _range)
                {
                    currentState = AiState.MoveToTarget;
                    break;
                }

                break;
        }
    }
}
