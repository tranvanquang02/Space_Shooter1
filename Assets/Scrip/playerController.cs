using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class playerController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private float m_speed;
    [SerializeField] Transform m_firePoin;

    [SerializeField] projectileManager m_projectile;
    [SerializeField] private float m_timeCoolDown;
    [SerializeField] private int m_HP;


    private float m_timeCoolUp;
    private int m_currentHP ;
    //private SpawManager m_spawManager;
    //private GameManager m_gamemanager;
    void Start()
    {
        m_currentHP = m_HP;
        //m_spawManager = FindObjectOfType<SpawManager>();
        //m_gamemanager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 m_direction = new Vector3(horizontal, vertical, 0);

        transform.Translate(m_direction * m_speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.Space))
        {
            if (m_timeCoolUp < 0)
            {
                Fire();
                m_timeCoolUp = m_timeCoolDown;
            }
        }
        m_timeCoolUp -= Time.deltaTime;
    }
    private void Fire()
    {
        projectileManager projectile = SpawManager.Instance.PlayerProjectSpam(m_firePoin.position);

        projectile.fireDetroy();

    } 
    public void Hit(int damage)
    {
        m_currentHP -= damage;
        if(m_currentHP <= 0) {
            Destroy(gameObject);
            GameManager.Instance.GameOver(false);          
        }
    }

}
