using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterFlame : MonoBehaviour
{
    AudioSource audiosource;
    bool musicStart = false;
    private void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if(collision.CompareTag("Note")&& !musicStart)
        //{
        //    audiosource.Play();
        //    musicStart = true;
        //}
    }
}
