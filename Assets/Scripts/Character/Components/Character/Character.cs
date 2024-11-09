using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected CharacterData characterData;
    public CharacterData CharacterData => characterData;

    public IMovable MovementComponent { get; protected set; }
    public ILiveComponent LiveComponent { get; protected set; }
    public IDamageComponent DamageComponent { get; protected set; }
    public IControlComponent ControlComponent { get; protected set; }
    public ILogicComponent LogicComponent { get; protected set; }

    public virtual void Initialize()
    {
        MovementComponent = new CharacterMovementComponent();
        MovementComponent.Initialize(this);
    }

    public abstract void Update();
}
