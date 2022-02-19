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


        StartCoroutine(ShockTimer());

        
        

    }

    private IEnumerator ShockTimer()
    {
        float timer = 5f;

        while(timer > 0)
        {
            RaycastHit[] hits = Physics.SphereCastAll(spherecastStart.position, spherecastRadius, Vector3.down, 3, deathPlayerMask);
            foreach (var shocked in hits)
            {
                if (shocked.collider.gameObject.CompareTag("Player"))
                {
                    Debug.Log("Got Hit");
                }
                else if (shocked.collider.gameObject.CompareTag("TheDeath"))
                {
                    //shocked.collider.GetComponent<PlugableStateController>().IsActive = false;
                    StartCoroutine(StunTimer(shocked.collider.GetComponent<PlugableStateController>()));
                }
            }
            timer -= Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }


    private IEnumerator StunTimer(PlugableStateController enemy)
    {
        if (enemy.IsActive)
        {
            enemy.IsActive = false;
            yield return new WaitForSeconds(1f);
            enemy.IsActive = true;
            Debug.Log(enemy.IsActive);
        }
        yield return null;
    }
}
