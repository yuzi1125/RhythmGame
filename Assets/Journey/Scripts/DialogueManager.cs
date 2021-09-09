using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;

public enum NEXTACTION { CHOICE, DIALOGUE };

public class DialogueManager : MonoBehaviour
{
    public Animator animator;

    public Text dialogueText;
    [SerializeField] Text nameText;

    public Queue<string> sentences;

    public Dialogue dialogue;
    public ChoiceTrigger choiceTrigger;

    public string currentStory;

    private void Awake()
    {
        sentences = new Queue<string>();
    }

    private void Start()
    {
        currentStory = "default";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !animator.GetBool("IsOpen"))
        {
            LoadFromJson(currentStory);

            StartDialogue(dialogue);
        }
        if (Input.GetKeyDown(KeyCode.Space) && !choiceTrigger.animator.GetBool("IsOpen") && animator.GetBool("IsOpen"))
        {
            DisplayNextSentence();
        }
    }

    //Json에 저장
    public void SaveToJson(string number)
    {
        string json = JsonUtility.ToJson(dialogue, true);
        
        string path = Path.Combine(Application.dataPath, "Journey", "Dialogue", number + ".json");

        File.WriteAllText(path, json);
    }

    //Json에서 불러오기
    public void LoadFromJson(string number)
    {
        string path = Path.Combine(Application.dataPath, "Journey", "Dialogue", number + ".json");

        string json = File.ReadAllText(path);
        dialogue = JsonUtility.FromJson<Dialogue>(json);
    }

    //대화 시작
    public void StartDialogue(Dialogue dialogue)
    {
        StopAllCoroutines();
        dialogueText.text = "";

        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    //다음 문장 출력
    public void DisplayNextSentence()
    {
        //선택지가 없을 때, 대화를 마친다.
        if (sentences.Count == 0 && !IsChoice())
        {
            EndDialogue();
            currentStory += "1";
            return;
        }
        //선택지가 있을 때, 선택지를 띄운다.
        else if(sentences.Count == 0 && IsChoice())
        {
            choiceTrigger.Open();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypingSentence(sentence));
    }

    //대화 끝남
    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false);

        if (currentStory == "default")
            currentStory = null;
    }

    //글자 타이핑 효과
    IEnumerator TypingSentence(string sentence)
    {
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;

            yield return new WaitForSeconds(0.1f);
        }
    }

    //선택지가 있는 대사인지
    bool IsChoice()
    {
        if (dialogue.nextAction == NEXTACTION.CHOICE)
            return true;
        return false;
    }
}