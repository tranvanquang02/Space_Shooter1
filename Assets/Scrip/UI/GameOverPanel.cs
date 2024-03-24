using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] TextMeshProUGUI m_txtResult;
    [SerializeField] TextMeshProUGUI m_txtResultScore;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    public void btb_HomePress()
    {
        gameManager.Home();
    }
    public void DisPlayScore(int score)
    {
        m_txtResultScore.text = "HIGHSCORE:  " + score.ToString();
    }
    public void DisPlayResult(bool isWin)
    {
        if (isWin)
        {
            m_txtResult.text = "YOU WIN";
        }
        else m_txtResult.text = "YOU LOSE";
    }
}
