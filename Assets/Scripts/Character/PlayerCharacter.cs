using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{

    public override void Start()
    {
        base.Start();

        LiveComponent = new CharacterLiveComponent();
    }

    public override void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 movementVector = new Vector3(horizontal, 0, vertical).normalized;

        MovableComponent.Move(movementVector);
        MovableComponent.Rotation(movementVector);
    }
}
