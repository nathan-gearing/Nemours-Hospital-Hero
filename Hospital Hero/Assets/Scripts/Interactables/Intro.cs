using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : Interactable
{
    protected override void Interact()
    {
        return;
    }

    protected override string GetInteractPrompt()
    {
        return "Welcome to Hospital Heros\r\nUse WASD or arrows to move\r\nSpace to jump\r\nI to interact\r\nF to throw bars of soap\r\nE to melee\r\nG for special attack (must pickup)";
    }
}
