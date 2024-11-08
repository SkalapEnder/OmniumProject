using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private PlayerCharacter player;

    [SerializeField] private TextMeshPro lifeText;

    public TextMeshPro LifeText
    {
        get => lifeText; 
        set 
        {
            lifeText.text = "HP: " + value.ToString();
        }
    }

}
