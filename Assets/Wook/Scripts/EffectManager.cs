﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] Animator noteHitAnimator = null;
    [SerializeField] Animator judgementAnimator = null;
    [SerializeField] UnityEngine.UI.Image judgementImage = null;
    [SerializeField] Sprite[] judgementSprite = null;
    string hit = "Hit";



    public void NoteHitEffect()
    {
        noteHitAnimator.SetTrigger(hit);
    }

    public void JudgementEffect(int imageNum)
    {
        judgementImage.sprite = judgementSprite[imageNum];
        judgementAnimator.SetTrigger(hit);
    }
}
