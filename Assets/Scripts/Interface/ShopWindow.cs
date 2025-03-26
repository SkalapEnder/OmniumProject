using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopWindow : Window
{
    [SerializeField] private UnityEngine.UIElements.ScrollView ShopList;
    [SerializeField] private Button ExitButton;

    public override void Initialize()
    {
        ExitButton.onClick.AddListener(OnExitButtonHandler);
    }

    private void OnExitButtonHandler()
    {
        Hide(false);
    }

    protected override void OpenStart()
    {
        base.OpenStart();
        ExitButton.interactable = true;
    }

    protected override void CloseStart()
    {
        ExitButton.interactable = false;
    }
}
