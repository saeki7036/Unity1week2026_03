using UnityEngine;
using UnityEngine.Events;

public class StopShell : MonoBehaviour
{
    [SerializeField]
    Transform[] SpawnPos; 

    [SerializeField] GameObject ShellPrehub;

    [SerializeField] ShellController[] controllers;

    [SerializeField] UnityEvent StartEvent;

    bool ShotCheck = false;

    void Start()
    {
        ShotCheck = false;
    }

    private void Update()
    {
        if (ShotCheck)
            return;

        foreach (var con in controllers)
        {
            if(con.Level != 0)
            {
                StartEvent.Invoke();
                ShotCheck = true;
                break;
            }
        }
    }

    public void StopShells()
    {
        GameObject[] shells = GameObject.FindGameObjectsWithTag("Shell");

        foreach (var shell in shells)
        {
            if(shell.TryGetComponent<ShellController>(out var con))
                con.Stop();
        }
    }

    public void SpawnShell()
    {
        Vector2 PlayerPos = PlayerController.Instance.GetTransform.position;

        float disMax = -999;
        Vector2 max = Vector3.one * -999;

        foreach (var pos in SpawnPos)
        {
            float dis  = Vector2.Distance(PlayerPos, pos.position);

            if(disMax < dis)
            {
                disMax = dis;
                max = pos.position;
            }
        }

        if (disMax == -999)
            return;

        //プレイヤーに飛ばす方向を計算
        Vector2 dirTarget = (PlayerPos - max).normalized;

        //オブジェクト生成
        GameObject bullet = Instantiate(ShellPrehub, max, Quaternion.identity);

        ShellController shell = bullet.GetComponent<ShellController>();

        if (shell.IsNoVerocity())
        {
            shell.Shot(1, dirTarget);
        }
    }
}
