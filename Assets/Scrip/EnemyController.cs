using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float m_speed;
    [SerializeField] Transform[] m_wavePoin;

    [SerializeField] Transform m_firePoin;
    [SerializeField] projectileManager m_projectile;
    [SerializeField] private float m_timeCoolDown;
    [SerializeField] private int m_HP;

    //private GameManager m_gameManager;
    private int m_CurrentHp;

    //erializeField] private float m_MaxTimeCoolDown;

    private float m_timeCoolUp;
    int m_currentWavePoinIndex = 0;
    private bool active;
    //private SpawManager spawManager;

    void Start()
    {
        //spawManager = FindObjectOfType<SpawManager>();
        //m_gameManager = FindObjectOfType<GameManager>();
    }
     
    // Update is called once per frame
    void Update()
    {
        if (!active) { return; }
        int m_nextWavePoin = m_currentWavePoinIndex + 1;
        if(m_nextWavePoin > m_wavePoin.Length - 1)
        {
            m_nextWavePoin = 0;
        }
        transform.position = Vector3.MoveTowards(transform.position, m_wavePoin[m_nextWavePoin].position, m_speed * Time.deltaTime);
        if(transform.position == m_wavePoin[m_nextWavePoin].position)
        {
            m_currentWavePoinIndex = m_nextWavePoin;
        }
        //xoay Enemy theo huong di chuyen

        Vector3 direction = m_wavePoin[m_nextWavePoin].position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);

        if (m_timeCoolUp < 0)
        {
            Fire();
            m_timeCoolUp = m_timeCoolDown;
        }
        m_timeCoolUp -= Time.deltaTime;
    }

    private void Fire()
    {
        projectileManager projectile = SpawManager.Instance.EnemyProjectSpam(m_firePoin.position);

        projectile.fireDetroy();
    }

    public void init(Transform[] wavepoin)
    {
        m_wavePoin = wavepoin;
        active = true;
        transform.position = wavepoin[0].position;
        m_timeCoolUp = m_timeCoolDown;
        m_CurrentHp = m_HP;
    }
    public void Hit(int damage)
    {
        m_CurrentHp -= damage;

        if(m_CurrentHp <= 0) {
            SpawManager.Instance.Relaese(this);
            GameManager.Instance.AddScore(100);
        }
    }
}
