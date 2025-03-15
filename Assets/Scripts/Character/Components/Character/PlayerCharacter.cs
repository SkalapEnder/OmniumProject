using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerCharacter : Character
{
    public override Character TargetCharacter 
    { 
        get
        {
            Character target = null;
            float minDistance = float.MaxValue;
            List<Character> list = GameManager.Instance.CharacterFactory.ActiveCharacters;

            for(int i = 0; i < list.Count; i++) 
            {
                if (list[i].CharacterType == CharacterType.Player)
                    continue;

                float distanceBetween = Vector3.Distance(list[i].transform.position, transform.position);
                if (distanceBetween < minDistance)
                {
                    target = list[i];
                    minDistance = distanceBetween;
                }
                 
            }

            if (minDistance > 20) return null;
            else return target;
        } 
    }

    public override void Initialize()
    {
        base.Initialize();

        LiveComponent = new ImmortalLiveComponent();
        LiveComponent.Initialize(this);

        DamageComponent = new CharacterDamageComponent();
        DamageComponent.Initialize(this);

        //ControlComponent = new PlayerControlComponent();
        //ControlComponent.Initialize(this);
    }

    public override void Update()
    {
        // Disable Movement if Player is Dead
        if (LiveComponent.IsAlive) 
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            Vector3 movementVector = new Vector3(horizontal, 0, vertical).normalized;

            if (TargetCharacter == null)
            {
                MovementComponent.Rotation(movementVector);
            }
            else
            {
                Vector3 rotationDirection = TargetCharacter.transform.position - transform.position;
                MovementComponent.Rotation(rotationDirection);

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.Log("Attack!");
                    DamageComponent.MakeDamage(TargetCharacter);
                }
            }

            MovementComponent.Move(movementVector);
        }
        //ControlComponent.OnUpdate();
    }
}
