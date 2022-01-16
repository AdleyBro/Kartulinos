using UnityEngine;
using kart_action;
using power_up;

public class GreenShellState : PowerUpState
{
    private static readonly GameObject shell = (GameObject) Resources.Load("GreenShell", typeof(GameObject));

    public GreenShellState(KartAction k) : base(k)
    {
    }

    public override void OnEnter()
    {
        Vector3 dir = Vector3.ProjectOnPlane(k.cameraChaser.transform.forward, k.GetUp()) * (k.lookBack ? -1 : 1);
        float orientation = (k.leftJoyStick >= 0) ? 1 : -1;
        Vector3 pos = dir * (k.kartCollider.bounds.extents.x + 0.7f) * orientation;
        Debug.DrawRay(k.transform.position, dir, Color.cyan, 9999f);
        k.SpawnItem(shell, k.transform.position + pos + k.GetUp() * 0.3f, Quaternion.LookRotation(dir * orientation, k.GetUp()));
        k.stateManager.RemovePowerUp(GetType(), this);
    }
}
