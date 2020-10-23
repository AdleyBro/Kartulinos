using UnityEngine;
using UnityEngine.InputSystem;

public class InputScript : MonoBehaviour
{
    private float steerIn = 0;

    public void OnMovement(InputValue value)
    {
        steerIn = value.Get<float>();
        Debug.Log(steerIn);
    }
}
