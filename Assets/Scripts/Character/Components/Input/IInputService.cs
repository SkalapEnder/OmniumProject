using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputService
{
    Vector2 Direction { get; }

    public void Initialize(Character character);

    public void OnUpdate();
}

