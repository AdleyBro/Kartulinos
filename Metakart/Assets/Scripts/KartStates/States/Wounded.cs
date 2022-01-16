using UnityEngine;
using kart_action;

namespace kartstates
{
    public class Wounded : KartState
    {
        private float floorAngle;
        private float counter = 0f;
        private float stunDuration;

        public Wounded(KartAction k, float stunDur) : base(k) 
        {
            stunDuration = stunDur;
        }

        public override KartState CheckStateChange()
        {
            if (counter < stunDuration)
                return null;
            else
                return StatesList.OnFloor(k);
        }

        public override void FixedUpdate()
        {
            counter += Time.fixedDeltaTime;

            Vector3 friction = -k.kartBody.velocity * k.stats.mass * 5f;
            k.kartBody.AddForce(friction);

            k.offset = (0.25f - k.groundInfo.floorDistance) * k.groundInfo.floorNormal * 10f;
            k.kartBody.AddForce(k.offset, ForceMode.VelocityChange);

            floorAngle = k.groundInfo.FloorAngle(k.transform.up);
            if (floorAngle < 45f && k.groundInfo.floorNormal != Vector3.zero)
                k.floorNormalV = k.groundInfo.floorNormal;

            k.wantedRotation = Quaternion.FromToRotation(k.kartBody.transform.up, k.floorNormalV) * k.kartBody.rotation;
            k.kartBody.rotation = Quaternion.Slerp(k.kartBody.rotation, k.wantedRotation.normalized, 25f * Time.fixedDeltaTime);
            k.kartBody.rotation = Quaternion.AngleAxis(360f * Time.fixedDeltaTime, k.kartBody.transform.up) * k.kartBody.rotation;
        }

        public override void ApplyLateForces()
        {
            //k.kartBody.AddForce(k.accelV);
        }



        public override void OnCollisionEnter(Collision co)
        {/*
            ContactPoint cop = co.contacts[0];
            if (Vector3.Angle(cop.normal, k.transform.up) < 30f)
                return;

            Vector3 dir = Vector3.Reflect(k.transform.forward, cop.normal);
            dir = Vector3.ProjectOnPlane(dir, k.floorNormalV).normalized;
            k.kartBody.AddForce(dir * 100000f);
            Debug.DrawRay(k.transform.position, dir, Color.red);
            //Debug.DrawRay(k.transform.position, k.kartBody.velocity, Color.cyan);
            */
        }
    }
}
