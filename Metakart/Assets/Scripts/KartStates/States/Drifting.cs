using UnityEngine;
using kart_action;

namespace kartstates
{
    public class Drifting : KartState, OnFloorInterface
    {
        private float driftingSense;
        private Vector3 rotatedForward;
        private bool isEndingDrift;
        private float boostForce;

        // in seconds
        private float counter;                      
        private readonly float endingDriftDuration = 1f;
        private readonly float smallBoostPeriod = 1f;
        private readonly float mediumBoostPeriod = 2f;
        private readonly float highBoostPeriod = 3.5f;
        // ^^^

        private readonly float smallBoostForce = 800f;
        private readonly float mediumBoostForce = 1000f;
        private readonly float highBoostForce = 1400f;

        public Drifting(KartAction k) : base(k) 
        {
            driftingSense = k.driftingInput;
            counter = 0f;
            isEndingDrift = false;
        }

        public override KartState CheckStateChange()
        {
            if (CanKeepDrifting(k))
                return null;
            else
            {
                if (k.isOnFloor)
                    return StatesList.OnFloor(k);
                else
                    return StatesList.InAir(k);
            }
        }

        public override void OnEnter()
        {
        }

        public override void FixedUpdate()
        {
            if (isEndingDrift)
            {
                if (counter >= endingDriftDuration)
                    k.stateManager.ChangeState(StatesList.OnFloor(k));

                rotatedForward = Vector3.Slerp(rotatedForward, k.kartBody.transform.forward, 8f * Time.fixedDeltaTime);
                k.accelV = rotatedForward * k.stats.maxAccelForce;
                k.kartBody.AddForce(boostForce * rotatedForward);


                // TODO: Comprimir toda esta parte en una funcion dentro de OnFloorUtils
                OnFloorUtils.UpdateIsMovingForward(k);

                int sense = k.isMovingForward ? 1 : -1;
                float floatCanSteer = k.kartBody.velocity.magnitude > 1f ? 1f : k.kartBody.velocity.magnitude;

                Quaternion q = Quaternion.AngleAxis(k.steerInput * 90f * sense * floatCanSteer * Time.fixedDeltaTime, k.kartBody.transform.up);

                k.kartBody.rotation = q * k.kartBody.rotation;
            } else if (k.driftingInput == 0f)
            {
                if (counter < highBoostPeriod)
                {
                    if (counter < mediumBoostPeriod)
                    {
                        if (counter < smallBoostPeriod)
                            boostForce = 0f;
                        else
                            boostForce = smallBoostForce;
                    } else
                        boostForce = mediumBoostForce;
                }
                else
                    boostForce = highBoostForce;

                isEndingDrift = true;
                counter = -Time.fixedDeltaTime;
            } else
            {
                ApplyKartSteering(k);
                rotatedForward = Quaternion.AngleAxis(-driftingSense * 45f, k.kartBody.transform.up) * k.kartBody.transform.forward;
            }
            
            counter += Time.fixedDeltaTime;

            ApplyAcceleration(k);

            ApplyFloorFriction(k);

            KeepVelocityAttachedToFloor(k);

            KeepWheelsOffset(k);

            RotateKartToFloorNormal(k);

            
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

        public static bool CanStartDrifting(KartAction k)
        {
            return k.driftingInput != 0f && k.kartBody.velocity.magnitude > 30f * 0.5f
                    && k.isOnFloor && k.accelInput > 0f;
        }

        private bool CanKeepDrifting(KartAction k)
        {
            return isEndingDrift || (k.kartBody.velocity.magnitude > 30f * 0.5f
                    && k.isOnFloor && k.accelInput > 0f);
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
            k.accelV = rotatedForward * k.accelInput * (k.kartBody.velocity.magnitude + 0.1f) * k.currentAccelForce;
            k.accelV = Vector3.ClampMagnitude(k.accelV, k.stats.maxAccelForce);
        }

        public void ApplyFloorFriction(KartAction k)
        {
            k.groundInfo.UpdateFrictionForces();

            Vector3 frontFriction = -Vector3.Project(k.kartBody.velocity, rotatedForward) * k.groundInfo.frontalFricForce;
            k.kartBody.AddForce(frontFriction);

            Vector3 rightVector = Vector3.Cross(rotatedForward, k.kartBody.transform.up);
            Vector3 lateralFriction = -Vector3.Project(k.kartBody.velocity, rightVector) * k.groundInfo.lateralFricForce * 0.8f;
            k.kartBody.AddForce(lateralFriction);

            k.groundInfo.CheckFloorType(k.raysPosList[4].position, -k.kartBody.transform.up);
        }


        public void ApplyKartSteering(KartAction k)
        {
            //OnFloorUtils.UpdateIsMovingForward(k);
            Quaternion q = Quaternion.AngleAxis(driftingSense * (40f + (driftingSense * k.steerInput + 1f) * 30f) * Time.fixedDeltaTime, k.kartBody.transform.up);

            k.kartBody.rotation = q * k.kartBody.rotation;
        }
    }
}
