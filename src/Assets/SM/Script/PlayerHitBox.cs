using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    [SerializeField] PlayerController controller;
    [SerializeField] HitPointViewer hitPointViewer;

    static readonly string ShellTag = "Shell";

    public void Hit()
    {
        controller.TakeDamege();
        hitPointViewer.SetHitPoint(controller.GetHP());
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(ShellTag))
        {
            if (collision.gameObject.TryGetComponent<ShellController>(out var shell))
            {
                if(shell.Level == 0)
                    return;
            }

            Hit();
        }
    }
}
