using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Interactable : MonoBehaviour
{
    public float interactionDistance = 2f;
    public string interactPrompt = "Press I to interact";
    public GameObject promptCanvas;
    public TextMeshProUGUI promptText;
    //public GameObject promptInstance;
    private bool isInteracted = false;
    
    public Transform playerTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        //promptText.text = " ";
        if (promptCanvas != null)
        {
            //promptInstance = Instantiate(promptCanvas, transform);
            promptText = promptInstance.GetComponent<TextMeshProUGUI>();
            if (promptText != null )
            {
                promptInstance.SetActive(false);
            }
            else
            {
                Debug.LogError("prompt text not found");
            }
           
        }
        else
        {
            Debug.LogError("Canvas not found");
        }

        if (playerTransform == null)
        {
            Debug.LogError("no player found");
        }

    }

    /*void Update()
    {
        if (promptInstance == null || promptText == null) return;
        float distance = Vector3.Distance(playerTransform.position, transform.position);
        Debug.Log("Distance to player: " + distance);
    }*/
    // Update is called once per frame
    void Update()
    {
        if (!isInteracted)
        {
            float distance = Vector3.Distance(playerTransform.position, transform.position);
            

            if (distance <= interactionDistance) 
            {
                promptInstance.SetActive(true);
                promptText.text = GetInteractPrompt();

                if (Input.GetKeyDown(KeyCode.I))
                {
                    Interact();
                }
            }
            else
            {
                promptInstance.SetActive(false);
            }
        }
    }
    protected abstract void Interact();

    protected abstract string GetInteractPrompt();
}
