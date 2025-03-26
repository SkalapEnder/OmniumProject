using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnimationComponent : ICharacterComponent
{
    void SetAnimation(string animationName);

    void SetTrigger(string triggerName);

    void SetBool(string boolName, bool status);

    void SetValue(string valueName, float value);

}
