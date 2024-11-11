using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warning : Interactable
{
    protected override void Interact()
    {
        return;
    }

    protected override string GetInteractPrompt()
    {
        return "Watch out, enemies will move towards you once close enough\r\nDont't forget to use F to clean these germs up!";
    }
}