using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] GameObject titleUI;
    [SerializeField] GameObject gameoverUI;
    [SerializeField] GameObject gameoverUI_noLives;
    [SerializeField] GameObject winUI;
    [SerializeField] TMP_Text livesUI;
    [SerializeField] TMP_Text timerUI;
    [SerializeField] Slider healthUI;

    [SerializeField] FloatVariable health;

    [SerializeField] GameObject respawn;

    [Header("Events")]
    //[SerializeField] IntEvent scoreEvent;
    [SerializeField] VoidEvent gameStartEvent;
    [SerializeField] GameObjectEvent respawnEvent;

    [Header("Objects")]
    [SerializeField] GameObject[] pickups;


    public enum State
    {
        TITLE,
        START_GAME,
        PLAY_GAME,
        RESTART_GAME,
        GAME_OVER,
        NO_LIVES,
        WON
    }

    public State state = State.TITLE;
    public int lives = 0;
    public float timer = 0;

    public int Lives
    { 
        get { return lives; }
        set { 
            lives = value; 
            livesUI.text =  "LIVES: " + lives.ToString(); 
        }
    }

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

        switch (state)
        {
            case State.TITLE:
                titleUI.SetActive(true);
                gameoverUI.SetActive(false);
                gameoverUI_noLives.SetActive(false);
                winUI.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                state = State.RESTART_GAME;

                break;

            case State.START_GAME:
                titleUI.SetActive(false);
                gameoverUI.SetActive(false);
                gameoverUI_noLives.SetActive(false);
                winUI.SetActive(false);

                Timer = 60;
                health.value = 100;

                if (Lives > 1)
                {
                    Lives--;
                    state = State.PLAY_GAME;
                }
                else if (Lives <= 1)
                {
                    state = State.NO_LIVES;
                }

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                gameStartEvent.RaiseEvent();
                respawnEvent.RaiseEvent(respawn);

                break;

            case State.PLAY_GAME:
                Timer = Timer - Time.deltaTime;

                if (Timer <= 0)
                {
                    state = State.GAME_OVER;
                }
                if (player.Score == 30)
                {
                    state = State.WON;
                }
                break;

            case State.RESTART_GAME:
                Lives = 4;

                foreach (GameObject pickup in pickups)
                {
                    pickup.SetActive(true);
                }
                player.Score = 0;

                break;

            case State.GAME_OVER:
                gameoverUI.SetActive(true);
                gameoverUI_noLives.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                break;

            case State.NO_LIVES:
                gameoverUI.SetActive(false);
                gameoverUI_noLives.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                break;

            case State.WON:
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
