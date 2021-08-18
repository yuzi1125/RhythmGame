using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    [SerializeField] int bpm = 0;
    double currentTime = 0d;

    [SerializeField] Transform[] NoteApeear;
    [SerializeField] GameObject goNote = null;
    bool GameStart = false;

    TimingManager timeingManager;
    EffectManager effectManager;
    void Start()
    {
        timeingManager = GetComponent<TimingManager>();
        effectManager = FindObjectOfType<EffectManager>();
    }

    void Update()
    {

        currentTime += Time.deltaTime;
        if(currentTime>= 60d / bpm)
        {
            
            int dir = Random.Range(0, 4);
            if(!GameStart)
            {
                GameStart = true;
                dir = (int)NoteDir.left;
            }

            GameObject note = ObjectPoolManager.Instance.ReturnObject(ObjectType.Note);
            note.SetActive(true);
            note.transform.position = NoteApeear[dir].position;
            note.GetComponent<Note>().SetDir(dir);
            note.transform.SetParent(this.transform);
            note.transform.localScale = new Vector3(1, 1, 1);
            timeingManager.NoteList.Add(note);
            currentTime -= 60d / bpm; //오차 계산
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Note")
        {
            if(collision.GetComponent<Note>().GetNoteFlag())
                effectManager.JudgementEffect(3);
            collision.gameObject.SetActive(false);
            timeingManager.NoteList.Remove(collision.gameObject);
        }
    }


}
