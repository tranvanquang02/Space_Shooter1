using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum GameState{
    Home,
    GamePlay,
    Pause,
    GameOver
}
public class GameManager : MonoBehaviour
{
    private static GameManager m_Instance;
    public static GameManager Instance
    {
        get
        {
            if(m_Instance == null)
            {
                m_Instance = FindObjectOfType<GameManager>();
            }
            return m_Instance;
        }

    }

    private GameState m_state;

    [SerializeField] private HomePanel m_HomePanel;
    [SerializeField] private GamePlayPanel m_GampePlayPanel;
    [SerializeField] private Pause m_PausePanel;
    [SerializeField] private GameOverPanel m_GameOverPanel;

    //private SpawManager m_SpawManager;

    private bool m_win;
    private int m_Score;

    private void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this;
        }
        else if (m_Instance != this) Destroy(gameObject);
    }
    void Start()
    {

        //m_SpawManager = FindObjectOfType<SpawManager>();
        m_state = GameState.Home;


        m_HomePanel.gameObject.SetActive(false);
        m_GampePlayPanel.gameObject.SetActive(false);
        m_PausePanel.gameObject.SetActive(false);
        m_GameOverPanel.gameObject.SetActive(false);

        setState(GameState.Home);
    }

    // Update is called once per frame
    private void setState(GameState state)
    {
        m_state = state;
        m_HomePanel.gameObject.SetActive(m_state == GameState.Home);
        m_GampePlayPanel.gameObject.SetActive(m_state == GameState.GamePlay);
        m_PausePanel.gameObject.SetActive(m_state == GameState.Pause);
        m_GameOverPanel.gameObject.SetActive(m_state == GameState.GameOver);

        if(m_state == GameState.Pause)
            Time.timeScale = 0f;
        else Time.timeScale = 1.0f;
    }
    public void Play()
    {
        setState(GameState.GamePlay);
        m_Score = 0;
        m_GampePlayPanel.DisplayScore(m_Score);

        SpawManager.Instance.StarButton();
    }

    internal void Pause()
    {
        setState(GameState.Pause);
    }

    internal void Home()
    {
        setState(GameState.Home);

        //m_SpawManager.Clear();
    }

    internal void Continue()
    {
        setState(GameState.GamePlay);
    }
    public void GameOver(bool result)
    {
        m_win = result;
        setState(GameState.GameOver);

        m_GameOverPanel.DisPlayResult(m_win);
        m_GameOverPanel.DisPlayScore(m_Score);
    }
    public void AddScore(int value)
    {
        m_Score += value;
        m_GampePlayPanel.DisplayScore(m_Score);

        if (SpawManager.Instance.IsClear()) GameOver(true);
    }
}
