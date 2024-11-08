using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : Interactable
{
    public float promptTime = 2f;
    public float loadTime = 2.5f;
    private UIManager uiManager;
    private GameManager gM;
    
    private void Start()
    {
        uiManager = FindAnyObjectByType<UIManager>();
        gM = FindAnyObjectByType<GameManager>();
        if(gM == null)
        {
            Debug.Log("GM not found");
        }
    }
    
    protected override void Interact()
    {
        uiManager.DisplayPrompt("<b>Next Level You Go</b>", promptTime);
        gM.RequestLevelTransition(1, loadTime);
        Destroy(gameObject);
        
        
    }
 
    
    protected override string GetInteractPrompt()
    {
        return "Press I to interact";
    }
}
