using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour, IDamagable
{
    [SerializeField] private PathFollower pathFollower;
    [SerializeField] private IntEvent scoreEvent;
    [SerializeField] private Inventory inventory;
    [SerializeField] private IntVariable score;
    [SerializeField] private FloatVariable health;

    [SerializeField] private GameObject hitPrefab;
    [SerializeField] private GameObject destoryPrefab;

    private void Start()
    {
        scoreEvent.Subscribe(AddPoints);
        health.value = 100;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            inventory.Use();
        }
        if (Input.GetButtonUp("Fire1"))
        {
            inventory.StopUse();
        }

        pathFollower.speed = (Input.GetKey(KeyCode.Space)) ? 2 : 1;
    }

    public void AddPoints(int points)
    {
        score.value += points;
        Debug.Log("Score: " + score.value);
    }

    public void ApplyDamage(float damage)
    {
        health.value -= damage;
        Debug.Log("Player Health: " + health.value);
        if (health.value <= 0)
        {
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

    public void ApplyHealth(float health)
    {
		this.health.value += health;
        this.health.value = Mathf.Clamp(this.health.value, 0, 100);
        Debug.Log("Player Health: " + this.health.value);
	}
}

