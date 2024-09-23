using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    public float promptTime = 2f;
    public float loadTime = 2.5f;
    private UIManager manager;
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
        manager.DisplayPrompt("<b>Now to Final Level</b>", promptTime);
        gM.RequestLevelTransition(2, loadTime);
        Destroy(gameObject);
    }

    protected override string GetInteractPrompt()
    {
        return "Press I to open the door";
    }
}
