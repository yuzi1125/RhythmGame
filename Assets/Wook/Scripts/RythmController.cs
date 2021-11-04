using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RythmController : MonoBehaviour
{
    TimingManager timingManager;

    private void Start()
    {
        timingManager = FindObjectOfType<TimingManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            timingManager.CheckTiming((int)dir.Right);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            timingManager.CheckTiming((int)dir.Left);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            timingManager.CheckTiming((int)dir.Up);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            timingManager.CheckTiming((int)dir.Down);
        }


        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    timingManager.CheckTiming();
        //}
    }
}
