using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Life : MonoBehaviour
{
    [SerializeField] GameObject pickupPrefab = null;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        print(collision.gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Player>(out Player player))
        {
            GameManager.Instance.Lives += 1;
        }

        Instantiate(pickupPrefab, transform.position, Quaternion.identity);
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }
}
