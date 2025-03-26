using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Character : MonoBehaviour
{
    [SerializeField] private CharacterType characterType;
    [SerializeField] protected CharacterData characterData;
    [SerializeField] private float shakeIntensity = 1f;
    protected float invicibleCooldown;

    public virtual Character Target { get; }
    public CharacterType CharacterType => characterType;
    public CharacterData CharacterData => characterData;
    public PlayerInput CharacterInput { get; set; }
    public AudioSource AudioSource { get; set; }
    public IMovable MovementComponent { get; protected set; }
    public ILiveComponent LiveComponent { get; protected set; }
    public IDamageComponent DamageComponent { get; protected set; }
    public IAttackComponent AttackComponent { get; protected set; }
    public IControlComponent ControlComponent { get; protected set; }
    public ILogicComponent LogicComponent { get; protected set; }
    public IAnimationComponent AnimationComponent { get; protected set; }
    public IInputService InputService { get; protected set; }

    public virtual void Initialize()
    {
        if (characterData.IsImmortal)
        {
            LiveComponent = new ImmortalLiveComponent();
        }
        else
        {
            LiveComponent = new CharacterLiveComponent();
        }
        LiveComponent.Initialize(this);

        MovementComponent = new CharacterMovementComponent();
        MovementComponent.Initialize(this);

        AnimationComponent = new CharacterAnimationComponent();
        AnimationComponent.Initialize(this);
    }

    public abstract void Update();

    public abstract void CameraShake();
}
