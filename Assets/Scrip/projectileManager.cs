using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float m_speed;
    [SerializeField] private Vector3 m_direction;
    [SerializeField] private int m_damage;
    
    //private SpawManager m_spawManager;
    private float lifetime;
    private bool m_FromPlayer;


    void Start()
    {
        //m_spawManager = FindObjectOfType<SpawManager>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(m_speed * m_direction * Time.deltaTime);


        if(lifetime<0) this.Release();
        lifetime -= Time.deltaTime;
    }
    public void setFromPlayer(bool fromPlayer)
    {
        m_FromPlayer = fromPlayer;
        //return m_FromPlayer;
    }
    public void fireDetroy()
    {
        lifetime = 100f;
    }
    private void Release()
    {
        if (m_FromPlayer)
        {
            SpawManager.Instance.ReleasePlayerProjectile(this);
        }
        else
        {
            SpawManager.Instance.ReleaseEnemyProjectile(this);   
        }
    }
    //public void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log(collision.gameObject.name);
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Trigger" +  collision.gameObject.name);
        if (gameObject.CompareTag("Player"))
            {
            if (collision.gameObject.CompareTag("Enemy") && !(collision.gameObject.GetComponent<projectileManager>() != null))
            {
                this.Release();

                EnemyController enemy;
                collision.gameObject.TryGetComponent(out enemy);
                enemy.Hit(m_damage);
            }

        }
        if (gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.CompareTag("Player") && !(collision.gameObject.GetComponent<projectileManager>() != null))
            {
                //this.Release();

                Destroy(gameObject);

                playerController player;
                collision.gameObject.TryGetComponent(out player);
                player.Hit(m_damage);
            }
        }
           
    }
}
