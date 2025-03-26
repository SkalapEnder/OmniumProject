using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class EnemyCharacter : Character
{
    [SerializeField] private AiState currentState;

    public override Character Target => 
        GameManager.Instance.CharacterFactory.PlayerCharacter;

    public override void CameraShake()
    {
       
    }

    public override void Initialize()
    {
        base.Initialize();

        AttackComponent = new EnemyHandedAttackComponent();
        AttackComponent.Initialize(this);

        //DamageComponent = new CharacterDamageComponent();
        //DamageComponent.Initialize(this);

        LogicComponent = new EnemyLogicComponent();
        LogicComponent.Initialize(this);
    }

    public override void Update()
    {
        if (!LiveComponent.IsAlive
            || !GameManager.Instance.IsGameActive)
            return;

        LogicComponent.OnUpdate();
    }
}
