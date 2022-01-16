using UnityEngine;
using kart_action;

namespace kartstates
{
    public class OnFloor : KartState, OnFloorInterface
    {
        
        public OnFloor(KartAction k) : base(k) { }

        public override KartState CheckStateChange()
        {
            if (k.isOnFloor)
            {
                if (Drifting.CanStartDrifting(k))
                    return StatesList.Drifting(k);
                else
                    return null;
            }
            else
                return StatesList.InAir(k);
        }

        public override void OnEnter()
        {
            //Debug.Log("EN SUELO");
        }

        public override void FixedUpdate()
        {
            ApplyAcceleration(k);

            ApplyFloorFriction(k);

            KeepVelocityAttachedToFloor(k);

            KeepWheelsOffset(k);

            RotateKartToFloorNormal(k);

            ApplyKartSteering(k);
        }

        public override void ApplyLateForces()
        {
            k.kartBody.AddForce(k.accelV);
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

        public void RotateKartToFloorNormal(KartAction k)
        {
            OnFloorUtils.RotateKartToFloorNormal(k);
        }

        public void KeepWheelsOffset(KartAction k)
        {
            OnFloorUtils.KeepWheelsOffset(k);
        }

        public void KeepVelocityAttachedToFloor(KartAction k)
        {
            OnFloorUtils.KeepVelocityAttachedToFloor(k);
        }

        public void ApplyAcceleration(KartAction k)
        {
            OnFloorUtils.ApplyAcceleration(k);
        }

        public void ApplyFloorFriction(KartAction k)
        {
            k.groundInfo.UpdateFrictionForces();
            OnFloorUtils.ApplyFloorFriction(k);
            k.groundInfo.CheckFloorType(k.raysPosList[4].position, -k.kartBody.transform.up);
        }

        public void UpdateIsMovingForward(KartAction k)
        {
            OnFloorUtils.UpdateIsMovingForward(k);
        }

        public void ApplyKartSteering(KartAction k)
        {
            OnFloorUtils.UpdateIsMovingForward(k);

            int sense = k.isMovingForward ? 1 : -1;
            float floatCanSteer = k.kartBody.velocity.magnitude > 1f ? 1f : k.kartBody.velocity.magnitude;

            Quaternion q = Quaternion.AngleAxis(k.steerInput * 90f * sense * floatCanSteer * Time.fixedDeltaTime, k.kartBody.transform.up);

            k.kartBody.rotation = q * k.kartBody.rotation;
        }
    }
}
