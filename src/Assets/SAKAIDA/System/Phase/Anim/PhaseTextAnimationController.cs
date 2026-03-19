using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PhaseTextAnimationController : MonoBehaviour
{

    [SerializeField] Animator animator;
    [SerializeField] TextMeshProUGUI PhaseText;
    [SerializeField] string DefaltPhaseText = "PHASE:";

    [SerializeField] AudioClip StartClip;

    public void PhaseStart(int nowPhase) 
    { 
    
        PhaseText.text = DefaltPhaseText + nowPhase.ToString();
        animator.Play("ŠJŽn");
        AudioManager.instance.PlaySE(StartClip);
    }
}
