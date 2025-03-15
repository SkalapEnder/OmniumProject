using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class EnemyCharacter : Character
{
    [SerializeField] private AiState currentState;

    public override Character TargetCharacter => 
        GameManager.Instance.CharacterFactory.Player;

    public override void Initialize()
    {
        base.Initialize();

        LiveComponent = new CharacterLiveComponent();
        LiveComponent.Initialize(this);

        DamageComponent = new CharacterDamageComponent();
        DamageComponent.Initialize(this);

        LogicComponent = new EnemyLogicComponent();
        LogicComponent.Initialize(this);
    }

    public override void Update()
    {
        // Use pointer to change currentState there, not in LogicComponent
        LogicComponent.checkState(TargetCharacter, ref currentState);
    }
}
