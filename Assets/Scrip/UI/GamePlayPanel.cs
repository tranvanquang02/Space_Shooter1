    using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePlayPanel : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI m_txtScore;
    //private GameManager m_gameManager;
    void Start()
    {
        //m_gameManager = FindObjectOfType<GameManager>();
    }

    public void btn_Pausegame()
    {
        GameManager.Instance.Pause();
    }
    public void DisplayScore(int score)
    {
        m_txtScore.text = "SCORE : " + score;
    }
}
