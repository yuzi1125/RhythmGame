using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScore : MonoBehaviour
{
    public Text[] ScoreText;
    void Start()
    {
        ShowScore();
    }

    public void ShowScore()
    {

        string str = "Score : " + GameManager.Instance.Score;
        for (int i = 0; i < 2; i++)
        {

            ScoreText[i].text = str;
        }
    }
}
