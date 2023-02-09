using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    float time = 0.5f;
    float currentTime = 0;
    int ownerId;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= time)
        {
            Destroy(gameObject);
        }
    }

    public void SetOwnerId(int id)
    {
        ownerId = id;
    }

    private void OnTriggerEnter(Collider other)
    {
        var otherRig = other.GetComponent<Rigidbody>();
        if (otherRig == null)
        {
            return;
        }

        var player = other.GetComponent<CustomPlayerControl>();
        if (player != null)
        {
            if (player.Id == ownerId)
            {
                return;
            }
        }

        Vector3 direction = (other.transform.position - transform.position).normalized;
        otherRig.AddForce(direction * 1000);
    }
}
