using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    // Start is called before the first frame update
    //private GameManager m_gameManager;
    void Start()
    {
        //m_gameManager = FindObjectOfType<GameManager>();
    }

    public void btb_HomePress()
    {
        GameManager.Instance.Home();

    }
    public void btb_ContinuePress()
    {
        GameManager.Instance.Continue();
    }
}
