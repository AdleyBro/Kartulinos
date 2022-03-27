using UnityEngine;
using kart_action;
using power_up;
using System.Collections.Generic;
using System;

public class KartStateManager
{
    private KartState state;
    private PowerUpState powerUpState;
    private Dictionary<Type, PowerUpState> activePowerUps;

    public KartStateManager(KartAction k)
    {
        EnterState(StatesList.OnFloor(k));
        powerUpState = new BasePowerUpState();
        activePowerUps = new Dictionary<Type, PowerUpState>();
    }

    public KartState GetState()
    {
        return state;
    }

    public bool IsInState(Type _state)
    {
        return state.GetType().Equals(_state);
    }

    public bool IsPowerUpActive(Type powerUpType)
    {
        return activePowerUps.ContainsKey(powerUpType);
    }

    public void AddPowerUp(PowerUpState newPowerUp)
    {
        Type powerUpType = newPowerUp.GetType();
        PowerUpState outPowerUp;
        if (activePowerUps.TryGetValue(powerUpType, out outPowerUp))
            RemovePowerUp(powerUpType, outPowerUp);

        activePowerUps.Add(powerUpType, newPowerUp);

        powerUpState.previousPWS = newPowerUp;
        newPowerUp.nextPWS = powerUpState;
        powerUpState = newPowerUp;
        powerUpState.OnEnter();
    }

    public void RemovePowerUp(Type type, PowerUpState powerUp)
    {
        powerUp.OnExit();
        if (powerUp.previousPWS == null)
        {
            powerUpState = powerUp.nextPWS;
            powerUpState.previousPWS = null;
        }
        else
        {
            powerUp.previousPWS.nextPWS = powerUp.nextPWS;
            powerUpState = powerUp.previousPWS;
        }

        activePowerUps.Remove(type);
    }

    public void Update()
    {
        KartState nextState = state.CheckStateChange();
        if (nextState != null)
            ChangeState(nextState);

        state.Update();
        powerUpState.Update();
    }
    
    public void FixedUpdate()
    {
        state.FixedUpdate();
        powerUpState.FixedUpdate();
        state.ApplyLateForces();
    }

    public void LateUpdate()
    {
        state.LateUpdate();
        powerUpState.LateUpdate();
    }

    public void OnCollisionEnter(Collision co)
    {
        state.OnCollisionEnter(co);
        powerUpState.OnCollisionEnter(co);
    }

    public void OnCollisionStay(Collision co)
    {
        state.OnCollisionStay(co);
        powerUpState.OnCollisionStay(co);
    }
    public void OnCollisionExit(Collision co)
    {
        state.OnCollisionExit(co);
        powerUpState.OnCollisionExit(co);
    }

    public void EnterState(KartState _state)
    {
        state = _state;
        state.OnEnter();
    }

    public void ChangeState(KartState newState)
    {
        state.OnExit();
        state = newState;
        state.OnEnter();
    }
}
