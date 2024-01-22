using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] FloatVariable health;
    [SerializeField] PhysicsCharacterController characterController;

    [Header("Events")]
    [SerializeField] IntEvent scoreEvent = default;
    [SerializeField] VoidEvent gameStartEvent = default;
    [SerializeField] VoidEvent playerDeadEvent = default;

    private int score = 0;

    public int Score 
    { 
        get { return score; } 
        set { 
            score = value; 
            scoreText.text = score.ToString(); 
            scoreEvent.RaiseEvent(score);
        } 
    }

    private void OnEnable()
    {
        gameStartEvent.Subscribe(OnStartGame);
    }

    void Start()
    {
    }

    public void AddPoints(int points)
    {
        Score += points;
    }

    private void OnStartGame()
    {
        characterController.enabled = true;
    }

    public void OnRespawn(GameObject respawn)
    {
        transform.position = respawn.transform.position;
        transform.rotation = respawn.transform.rotation;

        characterController.Reset();
    }

    public void Damage(float damage)
    {
        health.value -= damage;
        if (health.value <= 0)
        {
            playerDeadEvent.RaiseEvent();
            characterController.enabled = false;
            health.value = 0;
        }
    }
}
