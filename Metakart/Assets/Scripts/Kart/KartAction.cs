using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;

namespace kart_action
{
    public class KartAction : MonoBehaviour
    {
        public readonly KartStats stats;
        public KartStateManager stateManager;
        public Transform[] raysPosList; //Raycast order: FrontLeft, FrontRight, BackLeft, BackRight
        public Rigidbody krigidbody;
        public BoxCollider kartCollider;
        public GroundInfo groundInfo;
        public Motor motor;
        public Steering steering;
        public LapCounter lapCounter;
        public Item holdingItem = null;
        public CameraChaser cameraChaser;
        public InputManager.KartControlActions controller;
        private readonly float wheelHeight = 0.25f; // distance the kartCollider has to keep from the ground so that wheels don't sink on the floor
        //public float leftJoyStick = 1f;
        //public float accelInput;
        //public float currentAccelForce;
        //public float steerInput;
          //public float steerSpeed = 1.2f;
        //public float rotationSpeed;
        //public float rotationAngle;
        //public float curMaxSpeed;
        //public int driftingInput;
        //public bool lookBack;
        //public bool isBoosting;
        //public bool canDrift = true;
        //public float cpProgress;
        //public int currentCP;
        //public int lapProgress;
        private CheckpointsInfo cpInfo;
        private TextMeshProUGUI lapCounterText;
        private int totalCPs;
        public Vector3 offset;
        public Vector3 gravityV;
        public Vector3 accelV = Vector3.zero;
        public Vector3 velocityV = Vector3.zero;
        public Vector3 rotationAxis;
        public Quaternion wantedRotation;

        public void Start()
        {
            //currentAccelForce = stats.accelForce;
            //rotationSpeed = 2f;
            raysPosList = new Transform[] { transform.Find("FLRayLoc"), transform.Find("FRRayLoc"),
                                        transform.Find("BRRayLoc"), transform.Find("BLRayLoc"),
                                        transform.Find("CentRayLoc") };
            kartCollider = GetComponent<BoxCollider>();
            krigidbody = GetComponent<Rigidbody>();
            cameraChaser = GetComponentInChildren<CameraChaser>();
            groundInfo = new GroundInfo(0.4f, kartCollider.bounds.extents.y, false);
            groundInfo.SetHugFloor(true);
            steering = new Steering(this);
            motor = new Motor(this);
            stateManager = new KartStateManager(this);
            krigidbody.mass = stats.mass;
            //cpProgress = 0f;
            //lapProgress = 0;
            
            cpInfo = GameObject.Find("Checkpoints").GetComponent<CheckpointsInfo>();
            totalCPs = cpInfo.cpPos.Length;
            //currentCP = totalCPs-1;

            lapCounterText = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void Awake()
        {

            controller = new InputManager().KartControl;
            controller.Enable();
        }

        public void Update()
        {
            stateManager.Update();
        }

        public void FixedUpdate()
        {
            KeepWheelsOffset();

            Vector3 nextCPPos = cpInfo.cpPos[AlmightyModOperator.RealModulo(currentCP + 1, totalCPs)];
            float cpDistance = Vector3.Distance(cpInfo.cpPos[currentCP], nextCPPos);
            cpProgress = 1 - Vector3.Distance(krigidbody.position, nextCPPos) / cpDistance;
                        
            groundInfo.CheckFloorNormalAndDist(raysPosList, kartCollider, -transform.up);
                        
            stateManager.FixedUpdate();  // The movement is calculated here
        }

        private void OnCollisionEnter(Collision collision)
        {
            stateManager.OnCollisionEnter(collision);
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.tag.Equals("CP"))
            {
                int cpNumber = int.Parse(collider.name);
                if (AlmightyModOperator.RealModulo(currentCP+1, totalCPs) == cpNumber)
                {
                    currentCP = cpNumber;
                    if (currentCP == 0)
                    {
                        lapProgress++;
                        lapCounterText.text = "Lap " + lapProgress + "/3";
                    }
                } else if (currentCP == cpNumber)
                {
                    currentCP = AlmightyModOperator.RealModulo(currentCP - 1, totalCPs);
                    if (cpNumber == 0) 
                    {
                        lapProgress--;
                        lapCounterText.text = "Lap " + lapProgress + "/3";
                    }
                }
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            stateManager.OnCollisionStay(collision);
        }

        private void OnCollisionExit(Collision collision)
        {
            stateManager.OnCollisionExit(collision);
        }

        private void KeepWheelsOffset()
        {   if (groundInfo.IsOnFloor())
            {
                offset = (0.25f - groundInfo.GetFloorDistance()) * 8f * groundInfo.GetFloorNormal();
                krigidbody.AddForce(offset, ForceMode.VelocityChange);
            }
        }

        public GameObject SpawnItem(GameObject item, Vector3 position, Quaternion rotation)
        {
            return Instantiate(item, position, rotation);
        }

        public Vector3 GetKartRight()
        {
            return transform.right;
        }

        public Vector3 GetVelocity()
        {
            return velocityV;
        }

        public Vector3 GetForward()
        {
            return transform.forward;
        }

        public Vector3 GetUp()
        {
            return transform.up;
        }

        public Item GetHoldingItem()
        {
            return holdingItem;
        }

        public void SetHoldingItem(Item item)
        {
            holdingItem = item;
        }

        public bool IsHoldingItem()
        {
            return holdingItem != null;
        }
    }
}