using UnityEngine;
using kart_action;

public abstract class KartState
{
    public KartAction k;

    public KartState(KartAction k) { 
        this.k = k;
    }
    public virtual KartState CheckStateChange() { return null; }
    public virtual void OnEnter() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    public virtual void LateUpdate() { }

    // Use this function when you want to apply certain forces to the kart AFTER the PowerUps have made their effect.
    // Example: BoostState overrides the kart's accelV inside FixedUpdate, so that it forces the kart to go full speed,
    //          but the OnFloor state also assigns a value to accelV inside FixedUpdate, and if it immediately adds that force to
    //          the kart, BoostState's won't make any effect. That's why OnFloor applies accelV force to the kart inside this function,
    //          because that way BoostState succesfully overrides OnFloor's effect.
    public virtual void ApplyLateForces() { }
    public virtual void OnCollisionEnter(Collision co) { }
    public virtual void OnCollisionStay(Collision co) { }
    public virtual void OnCollisionExit(Collision co) { }
    public virtual void OnGUI() { }
    public virtual void OnDisable() { }
    public virtual void OnEnable() { }
    public virtual void OnExit() { }
}
