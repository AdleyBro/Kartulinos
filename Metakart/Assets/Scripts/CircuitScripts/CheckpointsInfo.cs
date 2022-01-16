using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointsInfo : MonoBehaviour
{
    public Vector3[] cpPos;
    // Start is called before the first frame update
    void Start()
    {
        GetCheckpointsPositions();
    }

    private void GetCheckpointsPositions()
    {
        int totalCheckpoints = transform.childCount;
        cpPos = new Vector3[totalCheckpoints];
        for (int i = 0; i < totalCheckpoints; i++)
            cpPos[i] = transform.GetChild(i).position;
    }
}
