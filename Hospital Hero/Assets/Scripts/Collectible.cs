using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : Interactable
{
    public float promptTime = 2f;
    private UIManager uiManager;
    private void Start()
    {
        uiManager = FindAnyObjectByType<UIManager>();
    }
    
    protected override void Interact()
    {
        uiManager.DisplayPrompt("<b>Fun Fact here</b>", promptTime);
        Destroy(gameObject);
        
        
    }

    protected override string GetInteractPrompt()
    {
        return "Press I to interact";
    }
}
