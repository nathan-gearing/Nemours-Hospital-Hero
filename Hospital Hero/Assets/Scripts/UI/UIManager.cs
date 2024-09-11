using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI promptText;

    private IEnumerator showForDuration(string message, float duration)
    {
        Debug.Log("started routine");
        promptText.text = message;
        yield return new WaitForSeconds(duration);
        promptText.text = "";
    }

    public void DisplayPrompt(string message, float duration)
    {
        StartCoroutine(showForDuration(message, duration));
    }
}
