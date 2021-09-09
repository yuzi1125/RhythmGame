using UnityEngine;
using UnityEditor;

//다중 오브젝트 편집을 가능하게
[CanEditMultipleObjects]
//DialogueManager 스크립트를 커스텀화
[CustomEditor(typeof(DialogueManager))]
public class DialogueEditor : Editor
{
    public string storyNum;
    public string choiceNum;

    //인터펙터에 GUI를 그려주는 함수
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        //명령이 작동할수 있도록 접근
        DialogueManager dialogueManager = (DialogueManager)target;
        Dialogue dialogue = dialogueManager.dialogue;

        storyNum = EditorGUILayout.TextField("스토리 번호", storyNum);

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("스토리 불러오기", GUILayout.Width(120), GUILayout.Height(30)))
            dialogueManager.LoadFromJson(storyNum);
        if (GUILayout.Button("스토리 저장하기", GUILayout.Width(120), GUILayout.Height(30)))
            dialogueManager.SaveToJson(storyNum);
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

        /*
        if (dialogue.nextAction == NEXTACTION.CHOICE)
        {
            dialogue.sprites[0] = (Sprite)EditorGUILayout.ObjectField("첫번째 선택지 이미지", dialogue.sprites[0], typeof(Sprite), true);
        }
        */

        /*
        GUILayout.FlexibleSpace();
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
        */
    }
}
