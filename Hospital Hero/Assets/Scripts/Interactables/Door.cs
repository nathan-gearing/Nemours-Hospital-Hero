using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Door : Interactable
{
    public float promptTime = 2f;
    public float loadTime = 2.5f;
    private UIManager manager;
    public TextMeshProUGUI promptText2;
    private GameManager gM;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindAnyObjectByType<UIManager>();
        if (manager == null)
        {
            Debug.Log("UI not found");
        }
        gM = FindAnyObjectByType<GameManager>();    
    }

    protected override void Interact()
    {
        manager.DisplayPrompt(promptText2, "Goodluck!", promptTime);
        gM.RequestLevelTransition(1, loadTime);
        Destroy(gameObject);
        
    }

    protected override string GetInteractPrompt()
    {
        return "Press I to open the door\nTo start your adventure";
    }
}
