using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENDGAME : MonoBehaviour
{
    [SerializeField] private CanvasGroup endGameGroup;
    public void ActiveDeathScene()
    {
        endGameGroup.alpha = 1;
    }
}
