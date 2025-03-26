using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IControlComponent : ICharacterComponent
{
    void OnUpdate();
}
