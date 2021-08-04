using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    [SerializeField] int bpm = 0;
    double currentTime = 0d;

    [SerializeField] Transform[] NoteApeear;
    [SerializeField] GameObject goNote = null;

    TimingManager timeingManager;
    void Start()
    {
        timeingManager = GetComponent<TimingManager>();
    }

    void Update()
    {

        currentTime += Time.deltaTime;
        if(currentTime>= 60d / bpm)
        {
            int dir = Random.Range(0, 3);
            GameObject note = Instantiate(goNote, NoteApeear[dir].position, Quaternion.identity);//Random.Range(0, 3)
            note.GetComponent<Note>().SetDir(dir);
            note.transform.SetParent(this.transform);
            timeingManager.NoteList.Add(note);
            currentTime -= 60d / bpm; //오차 계산
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Note")
        {
            Destroy(collision.gameObject);
            timeingManager.NoteList.Remove(collision.gameObject);
        }
    }


}
