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
        public float[] debugDistances;
        public KartStats stats;
        public KartStateManager stateManager;
        public Transform[] raysPosList; //Raycast order: FrontLeft, FrontRight, BackLeft, BackRight
        public Rigidbody kartBody;
        public GroundInfo groundInfo;
        public BoxCollider kartCollider;
        public Item holdingItem = null;
        public CameraChaser cameraChaser;
        public float leftJoyStick = 1f;
        public float accelInput;
        public float currentAccelForce;
        public float steerInput;
        public float steering = 1.2f;
        public float rotationSpeed;
        public float rotationAngle;
        public float curMaxSpeed;
        public int driftingInput;
        public bool isMovingForward;
        public bool lookBack;
        public bool isBoosting;
        public bool canDrift = true;
        public bool isOnFloor;
        public float cpProgress;
        public int currentCP;
        public int lapProgress;
        private CheckpointsInfo cpInfo;
        private TextMeshProUGUI lapCounterText;
        private int totalCPs;
        public Vector3 offset;
        public Vector3 gravityV;
        public Vector3 accelV = Vector3.zero;
        public Vector3 velocityV = Vector3.zero;
        public Vector3 normalV;
        public Vector3 floorNormalV;
        public Vector3 rotationAxis;
        public Quaternion wantedRotation;

        public void Start()
        {
            currentAccelForce = stats.accelForce;
            rotationSpeed = 2f;
            raysPosList = new Transform[] { transform.Find("FLRayLoc"), transform.Find("FRRayLoc"),
                                        transform.Find("BRRayLoc"), transform.Find("BLRayLoc"),
                                        transform.Find("CentRayLoc") };
            kartCollider = GetComponent<BoxCollider>();
            kartBody = GetComponent<Rigidbody>();
            cameraChaser = GetComponentInChildren<CameraChaser>();
            groundInfo = new GroundInfo(0.4f, kartCollider.bounds.extents.y, false);
            groundInfo.SetHugFloor(true);
            normalV = kartBody.transform.up;
            lookBack = false;
            stateManager = new KartStateManager(this);
            kartBody.mass = stats.mass;
            cpProgress = 0f;
            lapProgress = 0;
            
            cpInfo = GameObject.Find("Checkpoints").GetComponent<CheckpointsInfo>();
            totalCPs = cpInfo.cpPos.Length;
            currentCP = totalCPs-1;

            lapCounterText = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void Update()
        {
            stateManager.Update();
        }

        public void FixedUpdate()
        {
            Vector3 nextCPPos = cpInfo.cpPos[AlmightyModOperator.RealModulo(currentCP + 1, totalCPs)];
            float cpDistance = Vector3.Distance(cpInfo.cpPos[currentCP], nextCPPos);
            cpProgress = 1 - Vector3.Distance(kartBody.position, nextCPPos) / cpDistance;
                        
            groundInfo.CheckFloorNormalAndDist(raysPosList, kartCollider, -transform.up);
            
            // TODO: Delete this shiet
            // v
            isOnFloor = groundInfo.IsOnFloor();
            
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

        

        public GameObject SpawnItem(GameObject item, Vector3 position, Quaternion rotation)
        {
            return Instantiate(item, position, rotation);
        }

        public float GetSteerIn()
        {
            return steerInput;
        }

        public Vector3 GetKartRight()
        {
            return transform.right;
        }

        public bool GetIsMovingForward()
        {
            return isMovingForward;
        }

        public bool GetLookBack()
        {
            return lookBack;
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

        public int GetDrifting()
        {
            return driftingInput;
        }

        public bool GetIsBoosting()
        {
            return isBoosting;
        }

        public void SetIsBoosting(bool value)
        {
            isBoosting = value;
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