using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlComponent : IControlComponent
{
    private Character character;
    private IMovable MovementComponent => character.MovementComponent;

    public void Initialize(Character character)
    {
        this.character = character;
    }

    public void OnUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 movementVector = new Vector3(horizontal, 0, vertical).normalized;
        MovementComponent.Move(movementVector);
        MovementComponent.Rotation(movementVector);
    }
}
