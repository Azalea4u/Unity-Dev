using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] protected float health;
    [SerializeField] protected int points;
    [SerializeField] protected IntEvent scoreEvent;

    [SerializeField] protected GameObject hitPrefab;
    [SerializeField] protected GameObject destoryPrefab;

    public void ApplyDamage(float damage)
    {
        health -= damage;

        if (health < 0)
        {
            health = 0;
        }

        Debug.Log("Enemy Health: " + health);
        if (health <= 0)
        {
            scoreEvent?.RaiseEvent(points);
            if (destoryPrefab != null)
            {
                Instantiate(destoryPrefab, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
        else
        {
            if (hitPrefab != null)
            {
                Instantiate(hitPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}
