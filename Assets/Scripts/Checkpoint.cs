using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] GameObject respawn;
    [SerializeField] GameObject checkpointArea;
    [SerializeField] GameObject checkpointPrefab;
    [SerializeField] AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Player>(out Player player))
        {
            player.OnCheckpoint(checkpointArea, respawn);
            print("Checkpoint reached");
        }

        Instantiate(checkpointPrefab, transform.position, Quaternion.identity);
        // play audio
        audioSource.Play();

        gameObject.SetActive(false);
    }
}
