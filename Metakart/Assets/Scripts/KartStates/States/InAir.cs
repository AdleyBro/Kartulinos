using UnityEngine;
using kart_action;

namespace kartstates
{
    public class InAir : KartState
    {
        public InAir(KartAction k) : base(k) { }

        public override KartState CheckStateChange()
        {
            if (k.groundInfo.IsOnFloor())
                return StatesList.OnFloor(k);
            else
                return null;
        }

        public override void FixedUpdate()
        {
            k.krigidbody.AddForce(Vector3.down * k.stats.mass * 20f);

            k.steering.AlignKartToNormal(Vector3.up, 8f);
        }
    }
}

