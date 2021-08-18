﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    public List<GameObject> NoteList = new List<GameObject>();

    [SerializeField] Transform Center = null;
    [SerializeField] RectTransform[] timingRect = null;
    Vector2[] timingBox = null;

    EffectManager effectManager;
    ScoreManager scoreManager;
    private void Start()
    {
        effectManager = FindObjectOfType<EffectManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
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
                if (timingBox[x].x <= notePosX && notePosX <= timingBox[x].y &&
                    timingBox[x].x <= notePosY && notePosY <= timingBox[x].y)
                {
                    //노트 제거
                    NoteList[i].GetComponent<Note>().HideNote();
                    NoteList.RemoveAt(i);

                    //이펙트
                    if (x < timingBox.Length -1)
                        effectManager.NoteHitEffect();                   
                    effectManager.JudgementEffect(x);

                    //점수 증가
                    scoreManager.IncreaseScore(x);
                    return;
                }
            }
        }
        effectManager.JudgementEffect(3);
    }
}
