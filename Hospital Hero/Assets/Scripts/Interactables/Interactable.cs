using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Interactable : MonoBehaviour
{
    public float interactionDistance = 2f;
    public string interactPrompt = "Press I to interact";
    public TextMeshProUGUI promptText;
    private bool isInteracted = false;
    
    public Transform playerTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        promptText.text = " ";
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInteracted)
        {
            float distance = Vector3.Distance(playerTransform.position, transform.position);
            

            if (distance <= interactionDistance) 
            {
                promptText.text = GetInteractPrompt();

                if (Input.GetKeyDown(KeyCode.I))
                {
                    Interact();
                }
            }
            else
            {
                promptText.text = " ";
            }
        }
    }
    protected abstract void Interact();

    protected abstract string GetInteractPrompt();
}
