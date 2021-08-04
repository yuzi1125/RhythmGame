using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextEffect : MonoBehaviour
{
    DialogueManager dialogueManager;

    private string originalText = ""; //기존의 텍스트

    private void Awake()
    {
        dialogueManager = GetComponent<DialogueManager>();
    }

    public IEnumerator OnTypingEffect(string text_)
    {
        originalText = text_;
        Debug.Log(originalText);

        yield return new WaitForSeconds(dialogueManager.delay);
        for (int i = 0; i <= text_.Length; i++)
        {
            dialogueManager.text_dialogue.text = originalText.Substring(0, i);

            yield return new WaitForSeconds(dialogueManager.interval);
        }
    }
}
