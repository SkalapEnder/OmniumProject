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

        if (character.TargetCharacter == null) 
        {
            MovementComponent.Rotation(movementVector);
        }
        else
        {
            Vector3 rotationDirection = character.TargetCharacter.transform.position - character.transform.position;
            MovementComponent.Rotation(rotationDirection);


        }

        MovementComponent.Move(movementVector);
        MovementComponent.Rotation(movementVector);
    }
}
