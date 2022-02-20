using System;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPathManager : MonoBehaviour
{
    public static PatrolPathManager Instance;
    public List<PatrolPath> paths = new List<PatrolPath>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Found another PatrolPathManager. You only should use one.");
        }
    }

    public PatrolPath GetClosestPatrolPath(Vector3 queryPosition, bool mustBeUnreserved = true)
    {
        float maxSqrDist = Mathf.Infinity;
        List<PatrolPath> availables = paths;
        if (mustBeUnreserved)
        {
            for (int i = 0; i < availables.Count; i++)
            {
                if (availables[i].IsReserved)
                    availables.Remove(availables[i]);
            }
        }

        if (availables.Count <= 0)
        {
            Debug.LogWarning("No unreserved patrol paths available");
            return null;
        }

        PatrolPath result = null;
        for (int i = 0; i < availables.Count; i++)
        {
            float dist = (availables[i].transform.position - queryPosition).sqrMagnitude;

            if (dist < maxSqrDist)
            {
                maxSqrDist = dist;
                result = availables[i];
            }
        }

        return result;
    }
}
