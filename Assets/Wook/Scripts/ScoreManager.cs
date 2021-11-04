using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Text txtScore = null;

    [SerializeField] int increaseScore = 10;
    int currentScore = 0;

    [SerializeField] float[] weight = null;

    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        currentScore = 0;
        GameManager.Instance.Score = 0;
        txtScore.text = "0";
    }

    public void IncreaseScore(int _JudgementState)
    {
         int _increaseScore = increaseScore;

        //가중치 계산
        GameManager.Instance.Score += (int)(_increaseScore * weight[_JudgementState]);
        txtScore.text = string.Format("{0:#,##0}", GameManager.Instance.Score);

        animator.SetTrigger("ScoreUp");

    }
}
