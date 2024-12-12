using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Collectible : Interactable
{
    public float promptTime = 15f;
    public float loadTime = 2.5f;
    private UIManager uiManager;
    private GameManager gM;
    public TextMeshProUGUI promptText1;
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
        uiManager.DisplayPrompt(promptText1, "Handwashing with soap and water is more effective than using hand sanitizer\r\nSoap is the real hero!", promptTime);
       
        Destroy(gameObject);
        
        
    }
 
    
    protected override string GetInteractPrompt()
    {
        return "Press I to collect";
    }
}
