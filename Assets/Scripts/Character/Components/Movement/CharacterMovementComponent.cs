using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterMovementComponent : IMovable
{
    private Character character;
    private CharacterData characterData;

    public Vector3 Position =>
        characterData.CharacterTransform.position;

    private float speed;

    public float Speed
    {
        get => speed;
        set
        {
            if (value > 0)
                speed = value;
        }
    }

    public void Move(Vector3 direction)
    {
        if (direction == Vector3.zero)
        {
            character.AnimationComponent.SetValue("Movement", 0);
            return;
        }


        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        Vector3 move = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
        characterData.CharacterController.Move(move * Speed * Time.deltaTime);

        character.AnimationComponent.SetValue("Movement", direction.magnitude);
    }

    public void Rotation(Vector3 direction)
    {
        if (direction == Vector3.zero)
            return;

        float smooth = 0.03f;
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(characterData.CharacterTransform.eulerAngles.y, targetAngle, ref smooth, smooth);
        characterData.CharacterTransform.rotation = Quaternion.Euler(0, angle, 0);
    }

    public void Initialize(Character character)
    {
        this.character = character;
        characterData = character.CharacterData;
        Speed = characterData.DefaultSpeed;
    }
}
