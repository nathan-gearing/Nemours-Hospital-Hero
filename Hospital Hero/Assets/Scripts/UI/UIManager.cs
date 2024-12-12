using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI promptText;
    public TextMeshProUGUI introText;
    public TextMeshProUGUI doorPrompt;
    public TextMeshProUGUI[] promptTexts;
    private Dictionary<TextMeshProUGUI, Coroutine> activeCoroutines = new Dictionary<TextMeshProUGUI, Coroutine>();

    private void Start()
    {
        
    }

    private IEnumerator ShowForDuration(TextMeshProUGUI textElement, string message, float duration)
    {
        Debug.Log($"displaying message: {message} on {textElement.name}");
        textElement.text = message;
        yield return new WaitForSeconds(duration);
        textElement.text = "";
    }

    public void DisplayPrompt(TextMeshProUGUI textElement, string message, float duration)
    {
        if (activeCoroutines.TryGetValue(textElement, out Coroutine exisitingCoroutine))
        {
            StopCoroutine(exisitingCoroutine);
        }
        Coroutine newCoroutine = StartCoroutine(ShowForDuration(textElement, message, duration));
        activeCoroutines[textElement] = newCoroutine;
    }

    /*private IEnumerator showForDuration(string message, float duration)
    {
        Debug.Log("started routine");
        promptText.text = message;
        
        yield return new WaitForSeconds(duration);
        promptText.text = "";
        
    }

    public void DisplayPrompt(string message, float duration)
    {
        StartCoroutine(showForDuration(message, duration));
    }*/

    /*public void DisplayDoorPrompt(string message, float duration)
    {
        StartCoroutine(showForDurationDoor(message, duration));
    }

    private IEnumerator showForDurationDoor(string message, float duration)
    {
        Debug.Log("started door routine");
        doorPrompt.text = message;

        yield return new WaitForSeconds(duration);
        doorPrompt.text = "";

    }*/
}
