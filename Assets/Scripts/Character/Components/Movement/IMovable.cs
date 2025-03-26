using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovable : ICharacterComponent
{
    float Speed { get; set; }

    Vector3 Position { get; }

    void Move(Vector3 direction);

    void Rotation(Vector3 direction);

}
