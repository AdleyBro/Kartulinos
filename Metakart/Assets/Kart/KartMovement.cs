using UnityEngine;

public class KartMovement : MonoBehaviour
{
    private Transform[] raysPosList; //Raycast order: Front,Back,Left,Right
    private CharacterController kartBody;
    private BoxCollider kartCollider;
    private GroundInfo groundInfo;
    private float maxSpeed;
    private float maxSteer;
    private float accel_val;
    float accel_in;
    float steer_in;
    float gravity;
    Vector3 accel_v;
    Vector3 velocity_v;

    public void Start()
    {
        raysPosList = new Transform[] { transform.Find("FrontRayLoc"), transform.Find("BackRayLoc"), 
                                        transform.Find("LeftRayLoc"), transform.Find("RightRayLoc") };
        maxSpeed = 0.5f;
        maxSteer = 5f;
        gravity = 20f;
        kartBody = GetComponent<CharacterController>();
        kartCollider = GetComponentInChildren<BoxCollider>();
        groundInfo = new GroundInfo(0.5f);
        velocity_v = Vector3.zero;
        accel_v = Vector3.zero;
        accel_val = 1f;
    }

    public void FixedUpdate()
    {
        Vector3 gravity_v = Vector3.zero;
        Vector3 friction_v = Vector3.zero;
        Vector3 direction_v = kartBody.transform.forward;
        
        PlayerInput();
        PrintRays(); // Debug purpose only
        groundInfo.CheckGround(raysPosList, -kartBody.transform.up);

        if (groundInfo.floorDistance < 0.4f) // If kart on floor
        {
            accel_v = direction_v * accel_in * accel_val * Time.deltaTime;
            if (accel_in == 0f)
                friction_v = -velocity_v * 2f * Time.deltaTime;
        }
        else
        {
            gravity_v = Vector3.down * gravity * Time.deltaTime;
            accel_v = Vector3.zero;
        }

        kartBody.transform.up = Vector3.Lerp(kartBody.transform.up, groundInfo.floorNormal, 10 * Time.deltaTime); // Kart aligns to floor's
        kartBody.transform.Rotate(kartBody.transform.up, maxSteer * steer_in);

        velocity_v += accel_v + friction_v;
        velocity_v = Vector3.ClampMagnitude(velocity_v, maxSpeed);

        velocity_v += gravity_v;

        kartBody.Move(velocity_v);
    }

    private void PlayerInput()
    {
        if (Input.GetKey(KeyCode.UpArrow))
            accel_in = 1f;
        else if (Input.GetKey(KeyCode.DownArrow))
            accel_in = -1f;
        else
            accel_in = 0;
        if (Input.GetKey(KeyCode.RightArrow))
            steer_in = 1f;
        else if (Input.GetKey(KeyCode.LeftArrow))
            steer_in = -1f;
        else
            steer_in = 0;
    }

    private void Collision()
    {
        
    }

    private void PrintRays()
    {
        foreach (Transform rayTrans in raysPosList)
            Debug.DrawRay(rayTrans.position, -kartBody.transform.up * 1f);
    }
}