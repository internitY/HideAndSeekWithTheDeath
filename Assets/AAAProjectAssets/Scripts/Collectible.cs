using System.Collections;
using System.Collections.Generic;
using DarkTonic.MasterAudio;
using UnityEngine;

public class Collectible : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectableManager.Instance.Collect();
            gameObject.SetActive(false);
            MasterAudio.PlaySound3DAtVector3AndForget("Collect", transform.position);
        }
    }
}
