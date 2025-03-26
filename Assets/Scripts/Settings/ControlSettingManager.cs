using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlSettingManager : MonoBehaviour
{
    [SerializeField] private Image JoystickEdge;
    [SerializeField] private Image JoystickCircle;

    private Color joystickCircleColor;
    private Color joystickEdgeColor;

    private float joystickCircleOpacity;
    private float joystickEdgeOpacity;

    public float JoystickCircleOpacity => joystickCircleOpacity;
    public float JoystickEdgeOpacity => joystickEdgeOpacity;

    public void Initialize()
    {
        joystickCircleColor = JoystickCircle.color;
        joystickCircleOpacity = JoystickCircle.color.a;

        joystickEdgeColor = JoystickEdge.color;
        joystickEdgeOpacity = JoystickEdge.color.a;
    }

    public void SetOpacity(float opacity)
    {
        joystickEdgeOpacity = opacity;
        joystickCircleOpacity = opacity;

        Color newColorCircle = new Color(joystickCircleColor.r, joystickCircleColor.g, joystickCircleColor.b, joystickCircleOpacity);
        Color newColorEdge = new Color(joystickEdgeColor.r, joystickEdgeColor.g, joystickEdgeColor.b, joystickEdgeOpacity);

        JoystickCircle.color = newColorCircle;
        JoystickEdge.color = newColorEdge;
    }
}
