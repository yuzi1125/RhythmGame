using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;

public class DialogueManager : MonoBehaviour
{
    public DialogueData dialogueData;
    
    public Text text_dialogue;
    [SerializeField] Text text_name;

    TextEffect textEffect;
    public float delay = 1f;
    public float interval = 0.2f;

    Coroutine croutine_typing;

    private void Awake()
    {
        textEffect = GetComponent<TextEffect>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            SaveToJson();

        if (Input.GetKeyDown(KeyCode.L))
            LoadFromJson();

        if (Input.GetKeyDown(KeyCode.Space))
            SkipTypingEffect();
    }

    private void SkipTypingEffect()
    {
        StopCoroutine(croutine_typing);

        text_dialogue.text = dialogueData.dialogue;
    }

    [ContextMenu("Save To Json Data")]
    void SaveToJson()
    {
        string json = JsonUtility.ToJson(dialogueData, true);
        string path = Path.Combine(Application.dataPath, "Resources");
        path = Path.Combine(path, "dialogueData.json"); File.WriteAllText(path, json);
    }

    [ContextMenu("Load From Json Data")]
    void LoadFromJson()
    {
        string path = Path.Combine(Application.dataPath, "Resources");
        path = Path.Combine(path, "dialogueData.json");
        string json = File.ReadAllText(path);
        dialogueData = JsonUtility.FromJson<DialogueData>(json);

        text_name.text = dialogueData.name;
        text_dialogue.text = "";

        if (Application.isPlaying)
        {
            croutine_typing = StartCoroutine(textEffect.OnTypingEffect(dialogueData.dialogue));
            return;
        }

        text_dialogue.text = dialogueData.dialogue;
    }
}

[System.Serializable]
public class DialogueData
{
    //public Image portrait;
    public string name;
    //public string face;
    public string dialogue;
}