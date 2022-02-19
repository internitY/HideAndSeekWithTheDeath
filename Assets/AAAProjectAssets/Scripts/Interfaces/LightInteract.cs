using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MAED.ActionAndStates;

public class LightInteract : MonoBehaviour, IInteractable
{
    [SerializeField]
    private Light pointLight;
    [SerializeField]
    private ParticleSystem particleSystemShock;
    [SerializeField]
    private Transform spherecastStart;
    [SerializeField]
    private float spherecastRadius = 3f;
    [SerializeField]
    private LayerMask deathPlayerMask;


    public void Use()
    {
        pointLight.color = Color.blue;
        particleSystemShock.Play();

        Debug.LogError("Do the LightAction Explosion");

        RaycastHit[] hits = Physics.SphereCastAll(spherecastStart.position, spherecastRadius, Vector3.down, 3, deathPlayerMask);

        foreach (var shocked in hits)
        {
            if (shocked.collider.gameObject.CompareTag("Player"))
            {
                Debug.Log("Got Hit");
            }
            else if (shocked.collider.gameObject.CompareTag("TheDeath"))
            {
                shocked.collider.GetComponent<PlugableStateController>().IsActive = false;
                //StartCoroutine(StunTimer(shocked.collider.GetComponent<PlugableStateController>()));
            }
        }
        gameObject.SetActive(false);

        //TODO
        //DeactivateInteract
        //ShockReaper If in Range
    }

    private IEnumerator StunTimer(PlugableStateController enemy)
    {
        enemy.IsActive = false;
        yield return new WaitForSeconds(5f);
        enemy.IsActive = true;
    }
}
