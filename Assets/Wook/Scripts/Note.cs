using System.Collections;
using System.Collections.Generic;
using UnityEngine;
enum NoteDir //노트가 이동할 방향 Right는 오른쪽으로 이동
{
    right = 0,
    left,
    Up,
    Down,
}
public class Note : MonoBehaviour
{
    [SerializeField] float Speed = 400;
    [SerializeField] NoteDir noteDir = NoteDir.right;
    [SerializeField] Vector3 Dir = Vector3.zero;
    [SerializeField] Sprite[] Notesprite;
    UnityEngine.UI.Image noteImage;
    private void Start()
    {
        
    }

    private void OnEnable()
    {
        if(noteImage == null)
            noteImage = GetComponent<UnityEngine.UI.Image>();
        else
            noteImage.enabled = true;
    }
    private void Update()
    {
        Move();
    }

    public int GetDir()
    {
        return (int)noteDir;
    }

    public void SetDir(int dir)
    {
        noteDir = (NoteDir)dir;
        switch (noteDir)
        {
            case NoteDir.right:
                noteImage.sprite = Notesprite[(int)NoteDir.right];
                Dir = Vector3.left;
                break;
            case NoteDir.left:
                noteImage.sprite = Notesprite[(int)NoteDir.left];
                Dir = Vector3.right;
                break;
            case NoteDir.Up:
                noteImage.sprite = Notesprite[(int)NoteDir.Up];
                Dir = Vector3.down;
                break;
            case NoteDir.Down:
                noteImage.sprite = Notesprite[(int)NoteDir.Down];
                Dir = Vector3.up;
                break;
        }
    }

    void Move()
    {
        
        transform.localPosition += Dir * Speed * Time.deltaTime;
    }

    public void HideNote()
    {
        noteImage.enabled = false;
    }

    public bool GetNoteFlag()
    {
        return noteImage.enabled;
    }

}
