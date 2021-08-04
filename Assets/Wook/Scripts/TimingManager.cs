using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    public List<GameObject> NoteList = new List<GameObject>();

    [SerializeField] Transform Center = null;
    [SerializeField] RectTransform[] timingRect = null;
    Vector2[] timingBox = null;

    private void Start()
    {
        //타이밍 박스 설정
        timingBox = new Vector2[timingRect.Length];
        for (int i = 0; i < timingRect.Length; i++)
        {
            timingBox[i].Set(Center.localPosition.x - timingRect[i].rect.width / 2,
                              Center.localPosition.x + timingRect[i].rect.width / 2);
        }
    }

    public void CheckTiming()
    {
        for (int i = 0; i < NoteList.Count; i++)
        {
            float notePosX = NoteList[i].transform.localPosition.x;
            float notePosY = NoteList[i].transform.localPosition.y;

            for (int x = 0; x < timingBox.Length; x++)
            {
                if (timingBox[x].x <= notePosX && notePosX <= timingBox[x].y)
                {
                    NoteList[i].GetComponent<Note>().HideNote();
                    NoteList.RemoveAt(i);
                    Debug.Log("Hit" + x);
                    return;
                }
            }
        }
        Debug.Log("miss");
    }
}
