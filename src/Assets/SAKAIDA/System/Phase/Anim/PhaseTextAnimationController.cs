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
        if(!this.gameObject.activeSelf)
            this.gameObject.SetActive(true);

        PhaseText.text = DefaltPhaseText + nowPhase.ToString();
        animator.Play("開始");
        AudioManager.instance.PlaySE(StartClip);
    }
}
