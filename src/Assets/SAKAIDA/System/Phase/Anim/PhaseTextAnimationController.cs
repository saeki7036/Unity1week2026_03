using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PhaseTextAnimationController : MonoBehaviour
{

    [SerializeField] Animator animator;
    [SerializeField] TextMeshProUGUI PhaseText;
    [SerializeField] string DefaltPhaseText = "PHASE:";
    [SerializeField] Vector2 SpawnPos;
    [SerializeField] GameObject SpawnEffect;

    [SerializeField] GameObject GoaldenShell;
    [SerializeField] GameObject nowGoaldenShell;

    [SerializeField] AudioClip StartClip;

    public void PhaseStart(int nowPhase) 
    { 
        if(!this.gameObject.activeSelf)
            this.gameObject.SetActive(true);

        if (!nowGoaldenShell && nowPhase != 1) 
        {
            //Debug.Log("召喚");
            GameObject CL_GoaldenShell =Instantiate(GoaldenShell,SpawnPos,Quaternion.identity);

            GameObject CL_Effect = Instantiate(SpawnEffect, SpawnPos, Quaternion.identity);
            Destroy(CL_Effect, 1);
            nowGoaldenShell = CL_GoaldenShell;
        }

        PhaseText.text = DefaltPhaseText + nowPhase.ToString();
        animator.Play("開始");
        AudioManager.instance.PlaySE(StartClip);
    }
}
