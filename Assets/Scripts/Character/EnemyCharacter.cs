using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character
{
    [SerializeField]
    private AiState currentState;

    [SerializeField]
    private Character targetCharacter;

    private float timeBetweenAttackCounter = 0;

    public override void Start() {
        base.Start();

        LiveComponent = new ImmortalLiveComponent();
        DamageComponent = new CharacterDamageComponent();
    }

    public override void Update()
    {
        switch (currentState)
        {
            case AiState.None:
                break;

            case AiState.MoveToTarget:
                Vector3 direction = targetCharacter.transform.position - transform.position;
                direction.Normalize();

                MovableComponent.Move(direction);
                MovableComponent.Rotation(direction);

                if (Vector3.Distance(targetCharacter.transform.position, transform.position) < 3
                    && timeBetweenAttackCounter <= 0) 
                {
                    DamageComponent.MakeDamage(targetCharacter);
                    timeBetweenAttackCounter = characterData.TimeBetweenAttacks;
                }

                if(timeBetweenAttackCounter > 0)
                {
                    timeBetweenAttackCounter -= Time.deltaTime;
                }

                break;
        }
    }
}
