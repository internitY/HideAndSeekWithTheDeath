using MAED.ActionAndStates;
using UnityEngine;

public class AudioListenerFollower : MonoBehaviour
{
    [SerializeField] private PlayerPlugableStateController player;

    private void Awake()
    {
        if (player == null)
            player = FindObjectOfType<PlayerPlugableStateController>();
    }

    private void Update()
    {
        transform.position = player.Eye.position;
    }
}
