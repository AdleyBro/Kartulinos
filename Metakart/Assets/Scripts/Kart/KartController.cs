using UnityEngine;
using UnityEngine.InputSystem;
using kart_action;
using System;

public class KartController : MonoBehaviour
{
    private KartAction k;

    public void Start()
    {
        k = GetComponent<KartAction>();
    }

    private void OnSteering(InputValue value)
    {
        k.steerInput = value.Get<float>();
    }

    private void OnAccelerating(InputValue value)
    {
        k.accelInput = value.Get<float>();
    }

    private void OnRegressing(InputValue value)
    {
        k.accelInput = -value.Get<float>();
    }

    private void OnUsingItem(InputValue value)
    {
        if (k.holdingItem != null && value.Get<float>() > 0f)
        {/*
            KartState buff = k.holdingItem.GetKartState(k);
            Type buffType = buff.GetType();

            k.activeBuffs.Add(buffType, buff);
            */
            k.stateManager.AddPowerUp(k.holdingItem.GetPowerUpState(k));
            k.holdingItem = null;
        }
    }

    private void OnLookBack(InputValue value)
    {
        k.lookBack = value.Get<float>() > 0;
    }

    private void OnDrifting(InputValue value)
    {
        if (value.Get<float>() > 0f && k.steerInput != 0f)
            k.driftingInput = k.steerInput > 0f ? 1 : -1;
        else
            k.driftingInput = 0;
    }

    private void OnGetItem()
    {
        k.SetHoldingItem(new BananaPeel());
    }

    private void OnLeftJoyStick(InputValue value)
    {
        k.leftJoyStick = value.Get<float>();
    }

    private void OnTeleport(InputValue value)
    {
        k.kartBody.MovePosition(new Vector3(-46, 20, -87));
    }
}
