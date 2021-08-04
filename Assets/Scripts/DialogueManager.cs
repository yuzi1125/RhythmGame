using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public DialogueData dialogueData;

    [SerializeField] Text text_name;
    [SerializeField] Text text_dialogue;

    void Start()
    {
        Debug.Log(Application.dataPath);

        LoadFromJson();
    }

    [ContextMenu("Save To Json Data")]
    void SaveToJson()
    {
        string json = JsonUtility.ToJson(dialogueData, true);
        string path = Path.Combine(Application.dataPath, "dialogueData.json");
        File.WriteAllText(path, json);
    }

    [ContextMenu("Load From Json Data")]
    void LoadFromJson()
    {
        string path = Path.Combine(Application.dataPath, "dialogueData.json");
        string json = File.ReadAllText(path);
        dialogueData = JsonUtility.FromJson<DialogueData>(json);

        text_name.text = dialogueData.name;
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