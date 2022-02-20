using System.Collections;
using MAED.ActionAndStates;
using UnityEngine;

[System.Serializable]
public class PatrolPath : MonoBehaviour
{
    [SerializeField] private DeathPlugableStateController reservedBy = null;
    [SerializeField] private Transform[] pathPoints;
    public Transform[] PathPoints => pathPoints;
    public bool IsReserved => reservedBy != null;
    public DeathPlugableStateController ReservedBy
    {
        get => reservedBy;
        set => reservedBy = value;
    }
    public PatrolPathType PathType = PatrolPathType.Loop;
    public enum PatrolPathType
    {
        Loop,
        UpAndDown
    }
    private IEnumerator Start()
    {
        while (PatrolPathManager.Instance == null)
        {
            yield return null;
        }

        if (!PatrolPathManager.Instance.paths.Contains(this))
            PatrolPathManager.Instance.paths.Add(this);
    }
    private void OnEnable()
    {
        if (PatrolPathManager.Instance != null && !PatrolPathManager.Instance.paths.Contains(this))
            PatrolPathManager.Instance.paths.Add(this);
    }
    private void OnDisable()
    {
        if (PatrolPathManager.Instance != null && PatrolPathManager.Instance.paths.Contains(this))
            PatrolPathManager.Instance.paths.Remove(this);
    }
    public bool TakePatrolPath(DeathPlugableStateController requester)
    {
        if (IsReserved)
        {
            Debug.LogWarning("Patrol Path " + name + " already reserved by  " + reservedBy.name + "... " + requester.name + " cannot take this patrol path.");
            return false;
        }

        reservedBy = requester;
        return true;
    }
    public int GetClosestPatrolWaypointIndex(Vector3 queryPosition)
    {
        float maxDist = Mathf.Infinity;
        int result = 0;

        for (int i = 0; i < pathPoints.Length; i++)
        {
            float dist = (pathPoints[i].transform.position - queryPosition).sqrMagnitude;

            if (dist < maxDist)
            {
                maxDist = dist;
                result = i;
            }
        }

        return result;
    }
    protected void OnDrawGizmos()
    {
        if (pathPoints.Length > 0)
        {
            for (int i = 0; i < pathPoints.Length - 1; i++)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawLine(pathPoints[i].position, pathPoints[i + 1].position);
                Gizmos.DrawCube(pathPoints[i].position, new Vector3(0.5f, 0.5f, 0.5f));
            }
            
            Gizmos.DrawCube(pathPoints[pathPoints.Length - 1].position, new Vector3(0.5f, 0.5f, 0.5f));

            if (PathType == PatrolPathType.Loop)
            {
                Gizmos.DrawLine(pathPoints[0].position, pathPoints[pathPoints.Length - 1].position);
            }
        }
    }
}
