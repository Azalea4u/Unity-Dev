using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SpaceGameManager : Singleton<SpaceGameManager>
{
    [SerializeField] GameObject titleUI;
    [SerializeField] GameObject gameoverUI;
    [SerializeField] GameObject winUI;
    [SerializeField] TMP_Text timerUI;
    [SerializeField] Slider healthUI;

    [SerializeField] FloatVariable health;

    [Header("Events")]
    [SerializeField] IntEvent scoreEvent;
    [SerializeField] VoidEvent gameStartEvent;

    [Header("Objects")]
    [SerializeField] GameObject[] pickups;

    public enum State
    {
        TITLE,
        START_GAME,
        PLAY_GAME,
        RESTART_GAME,
        GAME_OVER,
        GAME_WON
    }

    public State state = State.TITLE;
    public float timer = 0;

    public float Timer
    {
        get { return timer; }
        set
        {
            timer = value; 
            timerUI.text = string.Format("{0:F1}", timer); 
        }
    }

    private void OnEnable()
    {
        //scoreEvent.Subscribe(OnAddPoints);
    }

    private void OnDisable()
    {
        //scoreEvent.Unsubscribe(OnAddPoints);
    }

    void Start()
    {

    }

    void Update()
    {
        Player player = FindObjectOfType<Player>();
        var startingPosition = new Vector3(0, 21, -26);

        switch (state)
        {
            case State.TITLE:
                titleUI.SetActive(true);
                gameoverUI.SetActive(false);
                winUI.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                state = State.RESTART_GAME;

                break;

            case State.START_GAME:
                titleUI.SetActive(false);
                gameoverUI.SetActive(false);
                winUI.SetActive(false);

                health.value = 100;

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                gameStartEvent.RaiseEvent();

                break;

            case State.PLAY_GAME:
                Timer = Timer + Time.deltaTime;
                break;

            case State.RESTART_GAME:

                foreach (GameObject pickup in pickups)
                {
                    pickup.SetActive(true);
                }
                player.Score = 0;
                break;

            case State.GAME_OVER:
                gameoverUI.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                break;

            case State.GAME_WON:
                winUI.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                break;

            default:
                break;
        }

        Console.WriteLine(state);

        healthUI.value = health.value / 100.0f;
    }

    public void OnStartGame()
    {
        state = State.START_GAME;
    }

    public void OnPlayerDead()
    {
        state = State.GAME_OVER;
    }

    public void OnTitleScreen()
    {
        state = State.TITLE;
    }

    public void OnAddPoints(int points)
    {
        print(points);
    }
}
