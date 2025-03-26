using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlComponent : IControlComponent
{
    private Character player;
    private IInputService InputService => player.InputService;
    private IMovable MovementComponent => player.MovementComponent;
    private IAttackComponent AttackComponent => player.AttackComponent;
    private IAnimationComponent AnimationComponent => player.AnimationComponent;

    public void Initialize(Character character)
    {
        this.player = character;
    }

    public void OnUpdate()
    {
        Vector2 direction = InputService.Direction;
        float horizontal = direction.x;
        float vertical = direction.y;

        Vector3 movementVector = new Vector3(horizontal, 0, vertical).normalized;
        MovementComponent.Move(movementVector);
        AnimationComponent.SetValue("MoveX", vertical);
        if (player.Target == null) 
        {
            MovementComponent.Rotation(movementVector);
        }
        else
        {
            Vector3 rotationDirection = player.Target.transform.position - player.transform.position;
            MovementComponent.Rotation(rotationDirection);

            AttackComponent.OnUpdate();
            AttackComponent.MakeAttack();
        }
    }
}
