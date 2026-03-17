using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int AddLevel = 1;
    public Vector2 Direction = Vector2.zero;
    public PlayerController player;
    [SerializeField] List<AudioClip> clips = new List<AudioClip>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        AudioClip clip = clips[0];
        Debug.Log("ê⁄êG");
        ShellController shell = other.GetComponent<ShellController>();
        if (!shell) return;

        if (AddLevel > 2) 
        {
            clip = clips[1];
            player.StartSlow(0.15f, 0.3f);
            Camera_Controller.instance.Shake(0.2f, 0.2f);
        }
        shell.Shot(AddLevel,Direction);

        AudioManager.instance.PlaySE(clip);
        Debug.Log("çUåÇ");
    }

    
}
