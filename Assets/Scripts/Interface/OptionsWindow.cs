using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsWindow : Window 
{
    [Header("Buttons")]
    [SerializeField] private Button ExitButton;
    [SerializeField] private Button ClearProgressButton;

    [Header("Sliders")]
    [SerializeField] private Slider MasterSoundSlider;
    [SerializeField] private Slider GameSoundSlider;
    [SerializeField] private Slider AmbientSoundSlider;
    [SerializeField] private Slider JoystickSlider;

    public override void Initialize() 
    {
        ExitButton.onClick.AddListener(OnExitButtonHandler);
        ClearProgressButton.onClick.AddListener(OnClearProgressButtonPressed);
        JoystickSlider.onValueChanged.AddListener(OnJoystickSliderChanged);
        MasterSoundSlider.onValueChanged.AddListener(OnMasterSoundSliderChanged);
        GameSoundSlider.onValueChanged.AddListener(OnGameSoundSliderChanged);
        AmbientSoundSlider.onValueChanged.AddListener(OnAmbientSoundSliderChanged);
    }

    private void OnExitButtonHandler()
    {
        Hide(false);
    }

    private void OnJoystickSliderChanged(float value)
    {
        GameManager.Instance.ControlSettingManager.SetOpacity(value);
    }

    private void OnMasterSoundSliderChanged(float value)
    {
        GameManager.Instance.AudioService.SetVolume(AudioSystemType.Master, value);
    }

    private void OnGameSoundSliderChanged(float value)
    {
        GameManager.Instance.AudioService.SetVolume(AudioSystemType.Sounds, value);
    }

    private void OnAmbientSoundSliderChanged(float value)
    {
        GameManager.Instance.AudioService.SetVolume(AudioSystemType.Ambient, value);
    }

    private void OnClearProgressButtonPressed()
    {
        GameManager.Instance.ClearProgress();
    }

    protected override void OpenStart() 
    {
        base.OpenStart();
        ClearProgressButton.interactable = GameManager.Instance.IsGameActive ? false : true;
        JoystickSlider.value = GameManager.Instance.ControlSettingManager.JoystickCircleOpacity;

        MasterSoundSlider.value = GameManager.Instance.AudioService.GetVolume(AudioSystemType.Master);
        GameSoundSlider.value = GameManager.Instance.AudioService.GetVolume(AudioSystemType.Sounds);
        AmbientSoundSlider.value = GameManager.Instance.AudioService.GetVolume(AudioSystemType.Ambient);
    }

    protected override void OpenEnd()
    {
        ExitButton.interactable = true;
    }

    protected override void CloseStart()
    {
        ExitButton.interactable = false;
    }
}
