using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField]
    private JoystickController joystickController;
    public string Input => (joystickController == JoystickController.One) ? "Player1 " : "Player2 ";

    public Animator animator;
}
