using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleIntro : Interactable
{
    protected override void Interact()
    {
        return;
    }

    protected override string GetInteractPrompt()
    {
        return "Along the way you will come acorss objects you can collect (some hidden)\r\nEach one will reveal a unique fun fact\r\nCollect them all for a secret powerup";
    }
}
