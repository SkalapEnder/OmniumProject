using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private PlayerCharacter player;
    [SerializeField] private CanvasGroup hud;
    [SerializeField] private TextMeshProUGUI hudHP;

    private bool isHidden;

    public bool IsHidden
    {
        get => isHidden;
        set => isHidden = value;
    }

    private void Update()
    {
        UpdateHP(player.LiveComponent.Health);
    }

    private void UpdateHP(float healthNum)
    {
        hudHP.text = "HP: " + healthNum.ToString();
    }

    public void ToggleHud()
    {
        hud.alpha = isHidden ? 1 : 0;
    }
}
