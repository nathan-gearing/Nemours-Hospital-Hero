using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class endCollectible : Interactable
{
    public float promptTime = 15f;
    private UIManager uiManager;
    public TextMeshProUGUI promptText1;
    private PlayerController playerController;
    private void Start()
    {
        uiManager = FindAnyObjectByType<UIManager>();
        playerController = FindAnyObjectByType<PlayerController>();
    }

    protected override void Interact()
    {
        uiManager.DisplayPrompt(promptText1, "Congratulations, You fought off the germs!", promptTime);
        Destroy(gameObject);
    }

    protected override string GetInteractPrompt()
    {
        return "Press I to collect";
    }
}
