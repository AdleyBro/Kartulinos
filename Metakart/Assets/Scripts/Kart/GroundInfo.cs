using System.Collections.Generic;
using UnityEngine;

// This method takes a list of raycast transforms and gathers information about the floor beneath the kart.
public class GroundInfo
{
    private bool hugFloor;
    private bool isOnFloor;
    private readonly float rayDistance;
    private float smallestDist;
    private float floorDistance; //Is the smallest distance from one of the four raycasts to the floor
    private string floorType;
    private Vector3 floorNormal;
    private RaycastHit[] hitsList;
    private Stack<RaycastHit> raycastHits;

    // float array should ALWAYS have two values. 
    // float[0] = frontal force
    // float[1] = lateral force
    private Dictionary<string, float[]> frictionForces = new Dictionary<string, float[]>();
    
    public GroundInfo(float rayDistance, float colliderExtents, bool hugFloor)
    {
        this.rayDistance = rayDistance;
        hitsList = new RaycastHit[5];
        this.hugFloor = hugFloor;
        raycastHits = new Stack<RaycastHit>();
        floorType = "Road";

        InitializeFrictionForces();
    }

    private void InitializeFrictionForces()
    {
        frictionForces.Add("Road",      new float[2] { 100f, 1000f });
        frictionForces.Add("Offroad",   new float[2] { 400f, 1000f });
        frictionForces.Add("Dirt",      new float[2] { 100f, 400f });
    }

    public void CheckFloorNormalAndDist(Transform[] raysList, Collider kartCollider, Vector3 kartDown)
    {
        floorNormal = Vector3.zero;
        floorDistance = 0;
        Vector3 rayDir = hugFloor ? kartDown : Vector3.down;
        
        for (int i = 3; i >= 0; i--)
        {
            if (Physics.Raycast(raysList[i].position, rayDir, out RaycastHit hit, rayDistance))
            {
                if (Vector3.Angle(hit.normal, kartDown) > 130f)
                {
                    raycastHits.Push(hit);
                    floorDistance += hit.distance;
                }
            }
        }
        
        int count = raycastHits.Count;
        floorDistance /= count;

        if (double.IsNaN(floorDistance) || double.IsInfinity(floorDistance))
            floorDistance = 0f;
        
        isOnFloor = count > 0;
        if (isOnFloor)
        {
            if (count == 1)
                floorNormal = raycastHits.Pop().normal;
            else if (count == 2)
            {
                RaycastHit firstHit = raycastHits.Pop();
                RaycastHit secondHit = raycastHits.Pop();

                Vector3 tempNormal = firstHit.normal + secondHit.normal;
                Vector3 pointVector = firstHit.point - secondHit.point;

                Vector3 tempCross = Vector3.Cross(tempNormal, pointVector);

                floorNormal = Vector3.Cross(pointVector, tempCross).normalized;
            }
            else
            {
                Vector3[] pointVectors = new Vector3[count];
                RaycastHit firstHit = raycastHits.Peek();

                for (int i = 0; i < count-1; i++)
                    pointVectors[i] = raycastHits.Pop().point - raycastHits.Peek().point;

                pointVectors[count - 1] = raycastHits.Pop().point - firstHit.point;

                Vector3 tempNormal = Vector3.zero;
                for (int i = 0; i < count; i++)
                    tempNormal += Vector3.Cross(pointVectors[i], pointVectors[(i+1) % count]);

                floorNormal = tempNormal.normalized;
                //raycastHits.Pop();
            }
        }
    }

    public void CheckFloorType(Vector3 raycastPos, Vector3 direction)
    {
        if (Physics.Raycast(raycastPos, direction, out RaycastHit hit, rayDistance))
            floorType = hit.transform.tag;
    }

    public float[] GetFrictionForces(Vector3 raycastPos, Vector3 direction)
    {
        string floorType = "Road";
        if (Physics.Raycast(raycastPos, direction, out RaycastHit hit, rayDistance))
            floorType = hit.transform.tag;

        float[] forces;
        frictionForces.TryGetValue(floorType, out forces);

        return forces;
    }

    public void CheckGroundShell(Transform rayPos, SphereCollider shellCollider, Vector3 shellDown)
    {
        smallestDist = 4f;
        if (Physics.Raycast(rayPos.position, hugFloor ? shellDown : Vector3.down, out RaycastHit hit, rayDistance))
        {
            Vector3 hitNormal = hit.normal;
            if (hit.distance < smallestDist)
                floorDistance = hit.distance;
            floorNormal = hitNormal.normalized;
        } else
        {
            floorNormal = Vector3.up;
            floorDistance = smallestDist;
        }
    }

    public float GetFloorDistance()
    {
        return floorDistance;
    }

    public Vector3 GetFloorNormal()
    {
        return floorNormal;
    }

    public float FloorAngle(Vector3 kartNormal)
    {
        return Vector3.Angle(kartNormal, floorNormal);
    }

    public void SetHugFloor(bool hugFloor)
    {
        this.hugFloor = hugFloor;
    }

    public bool IsOnFloor()
    {
        return isOnFloor;
    }

    public RaycastHit[] getHitsList()
    {
        return hitsList;
    }
}
