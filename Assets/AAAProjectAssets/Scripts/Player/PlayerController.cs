using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

[RequireComponent(typeof(RichAI))]
public class PlayerController : MonoBehaviour
{
    private RichAI ai;

    private void Awake()
    {
        ai = GetComponent<RichAI>();
    }

    public void SetPlayerDestination(Vector3 worldPos)
    {
        ai.destination = worldPos;
    }
}
