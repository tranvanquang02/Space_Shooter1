using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HomePanel : MonoBehaviour

{
    //private GameManager gameManager;
    void Start()
    {
        //gameManager = FindObjectOfType<GameManager>();
    }
    public void btn_PlayPress()
    {
        GameManager.Instance.Play();
    }
    
}
