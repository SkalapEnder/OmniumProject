using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterMovementComponent : IMovable
{
    private CharacterData characterData;
    private float speed;

    public float Speed { 
        get => speed;
        set
        {
            if(value < 0) 
                return;
            speed = value;
        }
    }

    public void Initialize(CharacterData characterData)
    {
        this.characterData = characterData;
        speed = characterData.DefaultSpeed;
    }

    public void Move(Vector3 direction)
    {
        if (direction == Vector3.zero)
            return;

        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        Vector3 move = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

        characterData.CharacterController.Move(move * Speed * Time.deltaTime);
    }

    public void Rotation(Vector3 direction)
    {
        if (direction == Vector3.zero)
            return;

        float smooth = 0.1f;
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(characterData.CharacterTransform.eulerAngles.y, targetAngle, ref smooth, smooth);
    }
}
