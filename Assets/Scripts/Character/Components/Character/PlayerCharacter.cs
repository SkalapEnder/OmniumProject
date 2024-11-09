using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    public void Start()
    {
        base.Initialize();

        LiveComponent = new CharacterLiveComponent();
        LiveComponent.Initialize(this);

        ControlComponent = new PlayerControlComponent();
        ControlComponent.Initialize(this);
    }

    public override void Update()
    {
        ControlComponent.OnUpdate();
    }
}
