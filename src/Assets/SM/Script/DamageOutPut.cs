using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOutPut : MonoBehaviour
{
    [SerializeField] GameObject DamageUIPrehub;

    [SerializeField] static GameObject HpBarParent;

    static GameObject damageUIPrehub;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        damageUIPrehub = DamageUIPrehub;
        HpBarParent = this.gameObject;
    }

    public static void ShowDamage(int damageValue, Transform spawnTransform)
    {
        GameObject damage = Instantiate(damageUIPrehub, spawnTransform.position,Quaternion.identity); 

        var log = damage.GetComponent<DamageLog>();

        log.TextUpdate(damageValue, spawnTransform);

        damage.transform.SetParent(HpBarParent.transform, false);
    }
}
