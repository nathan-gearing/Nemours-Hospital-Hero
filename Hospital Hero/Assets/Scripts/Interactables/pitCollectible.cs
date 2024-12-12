using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class pitCollectible : Interactable
{
    public float promptTime = 15f;
    private UIManager uiManager;
    public TextMeshProUGUI promptText1;
    private PlayerController playerController;
    public GameObject door;
    private void Start()
    {
        uiManager = FindAnyObjectByType<UIManager>();
        playerController = FindAnyObjectByType<PlayerController>();
    }

    protected override void Interact()
    {
        uiManager.DisplayPrompt(promptText1, "Did you know? Washing your hands for just 20 seconds can remove up to 1 million germs!", promptTime);
        Destroy(door);
        playerController.SetGiantSoapStatus(true);
        Destroy(gameObject);
    }

    protected override string GetInteractPrompt()
    {
        return "Press I to collect";
    }
}
