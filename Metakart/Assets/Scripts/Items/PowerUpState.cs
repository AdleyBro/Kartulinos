using UnityEngine;
using kart_action;

namespace power_up {

    public abstract class PowerUpState
    {
        public KartAction k;
        public PowerUpState previousPWS;
        public PowerUpState nextPWS;
        public float duration;
        public float counter = 0f;

        public PowerUpState(KartAction kart = null, PowerUpState pPWS = null, PowerUpState nPWS = null) {
            k = kart;
            previousPWS = pPWS;
            nextPWS = nPWS;
        }

        // Should be called ONLY inside FixedUpdate()
        public void UpdateCounter() { counter += Time.fixedDeltaTime; }

        public virtual void OnEnter() { }

        public virtual void Update() 
        {
            nextPWS.Update();
        }

        public virtual void FixedUpdate() 
        {
            UpdateCounter();
            nextPWS.FixedUpdate(); 
        }

        public virtual void LateUpdate() { nextPWS.LateUpdate(); }

        public virtual void OnCollisionEnter(Collision co) { nextPWS.OnCollisionEnter(co); }

        public virtual void OnCollisionStay(Collision co) { nextPWS.OnCollisionStay(co); }

        public virtual void OnCollisionExit(Collision co) { nextPWS.OnCollisionExit(co); }

        public virtual void OnGUI() { nextPWS.OnGUI(); }

        public virtual void OnDisable() { nextPWS.OnDisable(); }

        public virtual void OnEnable() { nextPWS.OnEnable(); }

        public virtual void OnExit() {}
    }
}