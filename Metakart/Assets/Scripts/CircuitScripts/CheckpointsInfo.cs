using UnityEngine;

public class CheckpointsInfo : MonoBehaviour
{
    public Vector3[] cpPos;

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
