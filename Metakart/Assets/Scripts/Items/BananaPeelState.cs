using UnityEngine;
using kart_action;
using power_up;

public class BananaPeelState : PowerUpState
{
    private static readonly GameObject bananaObject = (GameObject)Resources.Load("itemBanana", typeof(GameObject));

    public BananaPeelState(KartAction k) : base(k)
    {
    }

    public override void OnEnter()
    {
        float isLookingBack = k.lookBack ? -1 : 1;
        Vector3 dir = k.cameraChaser.transform.forward * isLookingBack;
        float orientation = (k.leftJoyStick <= 0) ? -1 : 1;
        Vector3 pos = dir * (k.kartCollider.bounds.extents.x + 0.7f) * orientation;
        GameObject spawnedBanana = k.SpawnItem(bananaObject, k.transform.position + pos + k.GetUp() * 0.3f, Quaternion.LookRotation(dir * orientation, k.GetUp()));
        if (orientation == 1)
            spawnedBanana.GetComponent<Rigidbody>().AddForce(Quaternion.AngleAxis(60f, k.cameraChaser.transform.right * isLookingBack) * k.cameraChaser.transform.up * (1600f + k.kartBody.velocity.magnitude * 30f));
        k.stateManager.RemovePowerUp(GetType(), this);
    }
}
