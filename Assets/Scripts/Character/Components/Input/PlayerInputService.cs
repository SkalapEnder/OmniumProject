using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputService : IInputService
{
    private Character character;
    private PlayerInput playerInput;
    private Vector2 direction;

    public Vector2 Direction => direction;

    public void Initialize(Character character)
    {
        this.character = character;
        playerInput = character.CharacterInput;
    }

    public void OnUpdate()
    {
        direction = playerInput.actions["Move"].ReadValue<Vector2>();
    }
}
