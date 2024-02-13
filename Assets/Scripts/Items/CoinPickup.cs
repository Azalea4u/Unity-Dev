using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
	[SerializeField] int score;
	[SerializeField] GameObject pickupPrefab;
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.TryGetComponent(out PlayerShip player))
		{
			player.AddPoints(score);
			if (pickupPrefab != null) Instantiate(pickupPrefab, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
