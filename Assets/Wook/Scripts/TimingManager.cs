using System.Collections;
using System.Collections.Generic;
using UnityEngine;
enum dir
{
    Right = 0,
    Left,
    Up,
    Down,
}

public class TimingManager : MonoBehaviour
{
    public List<GameObject> NoteList = new List<GameObject>();

    [SerializeField] Transform Center = null;
    [SerializeField] RectTransform[] timingRect = null;
    Vector2[] timingBox = null;

    [SerializeField] UnityEngine.UI.Image PerfectImage;
    [SerializeField] Sprite failsprite;

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

    public void CheckTiming(int dir)
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
                    bool Correct = true;
                    int tmp = x;
                    if (NoteList[i].GetComponent<Note>().GetDir() != dir)
                    {
                        Correct = false;
                        x = 3;
                     }
                    //perfect이미지 변경
                    if(Correct)
                        PerfectImage.sprite = NoteList[i].GetComponent<UnityEngine.UI.Image>().sprite;
                    else
                        PerfectImage.sprite = failsprite;
                    //노트 제거
                    NoteList[i].GetComponent<Note>().HideNote();
                    NoteList.RemoveAt(i);

                    //이펙트
                    if (x < timingBox.Length -1)
                        effectManager.NoteHitEffect();                   
                    effectManager.JudgementEffect(x);

                    //점수 증가
                    if(Correct)
                        scoreManager.IncreaseScore(x);

                    //로그 출력
                    string Log;
                    switch (x)
                    {
                        case 0:
                            Log = "Perfect";
                            break;
                        case 1:
                            Log = "Good";
                            break;
                        case 2:
                            Log = "Bad";
                            break;
                        case 3:
                            Log = "miss";
                            break;
                        default:
                            Log = "";
                            break;
                    }
                    if (x == 0)
                        Log = "Perfect";
                    LogController.Instance.SetLog(Log);
                    if (!Correct)
                        x = tmp;
                    return;
                }
                
            }
            
        }
        effectManager.JudgementEffect(3);
        PerfectImage.sprite = failsprite;
    }
}
