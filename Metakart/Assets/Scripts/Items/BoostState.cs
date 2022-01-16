using kart_action;
using power_up;
using UnityEngine;

public class BoostState : PowerUpState
{
    private readonly float boostForce = 2000f;
    public BoostState(KartAction k) : base(k) {
        duration = 2f;
    }

    public override void FixedUpdate()
    {
        if (counter >= duration)
            k.stateManager.RemovePowerUp(GetType(), this);

        k.accelV = k.transform.forward * k.stats.maxAccelForce;
        k.kartBody.AddForce(boostForce * k.kartBody.transform.forward);
        k.groundInfo.floorType = "Road";
        base.FixedUpdate();
    }
}
