using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ChoiceTrigger : MonoBehaviour
{
    [SerializeField] Image A;
    [SerializeField] Image B;
    [SerializeField] Image C;

    public Animator animator;
    public DialogueManager dialogueManager;

    public void Open()
    {
        animator.SetBool("IsOpen", true);

        A.sprite = dialogueManager.dialogue.sprites[0];
        B.sprite = dialogueManager.dialogue.sprites[1];
        C.sprite = dialogueManager.dialogue.sprites[2];
    }

    public void CloseAll()
    {
        animator.SetBool("IsOpen", false);
        dialogueManager.EndDialogue();
    }

    public void ChoiceA()
    {
        CloseAll();
     
        dialogueManager.currentStory += "a";
    }

    public void ChoiceB()
    {
        CloseAll();
        
        dialogueManager.currentStory += "b";
    }

    public void ChoiceC()
    {
        CloseAll();

        dialogueManager.currentStory += "c";
    }
}
