using UnityEngine;
using kart_action;

namespace kartstates
{
    public class InAir : KartState
    {
        public InAir(KartAction k) : base(k) { }

        public override KartState CheckStateChange()
        {
            if (k.isOnFloor)
                return StatesList.OnFloor(k);
            else
                return null;
        }

        public override void Update()
        {
            k.floorNormalV = k.groundInfo.floorNormal;
            if (k.floorNormalV == Vector3.zero)
                k.floorNormalV = Vector3.up;

            k.wantedRotation = Quaternion.FromToRotation(k.kartBody.transform.up, k.floorNormalV) * k.kartBody.rotation;
        }
        public override void FixedUpdate()
        {
            k.kartBody.AddForce(Vector3.down * k.stats.mass * 20f);

            k.kartBody.rotation = Quaternion.Slerp(k.kartBody.rotation, k.wantedRotation, k.rotationSpeed * Time.fixedDeltaTime);
        }
    }
}

