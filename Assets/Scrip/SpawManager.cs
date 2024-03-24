using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class EnemyPool
{
    public EnemyController preFab;
    public List<EnemyController> inActiveObject;
    public List<EnemyController> ActiveObject;
    public EnemyController spam(Vector3 position, Transform parent)
    {
        if (inActiveObject.Count == 0)
        {
            EnemyController newobj = GameObject.Instantiate(preFab, parent);
            newobj.transform.position = position;
            ActiveObject.Add(newobj);
            return newobj;
        }
        else
        {
            EnemyController oldObj = inActiveObject[0];
            oldObj.gameObject.SetActive(true);
            oldObj.transform.SetParent(parent);
            oldObj.transform.position = position;
            ActiveObject.Add(oldObj);
            return oldObj;
        }
    }
    public void Release(EnemyController obj)
    {
        if (ActiveObject.Contains(obj))
        {
            ActiveObject.Remove(obj);
            inActiveObject.Add(obj);
            obj.gameObject.SetActive(false);
        }
        
    }
    public void Clear()
    {
        while (ActiveObject.Count > 0)
        {
            EnemyController obj = ActiveObject[0];
            obj.gameObject.SetActive(false);
            ActiveObject.RemoveAt(0);
            inActiveObject.Add(obj);
        }
    }
}
[System.Serializable]
public class ProjectilePool
{
    public projectileManager preFab;
    public List<projectileManager> inActiveObject;
    public List<projectileManager> ActiveObject;
    public projectileManager spam(Vector3 position, Transform parent)
    {
        if (inActiveObject.Count == 0)
        {
            projectileManager newobj = GameObject.Instantiate(preFab, parent);
            newobj.transform.position = position;
            ActiveObject.Add(newobj);
            return newobj;
        }
        else
        {
            projectileManager oldObj = inActiveObject[0];
            oldObj.gameObject.SetActive(true);
            oldObj.transform.SetParent(parent);
            oldObj.transform.position = position;
            ActiveObject.Add(oldObj);
            return oldObj;
        }
    }
    public void Release(projectileManager obj)
    {
        if (ActiveObject.Contains(obj))
        {
            ActiveObject.Remove(obj);
            inActiveObject.Add(obj);
            obj.gameObject.SetActive(false);
        }

    }
    public void Clear()
    {
        while (ActiveObject.Count > 0)
        {
            projectileManager obj = ActiveObject[0];

            obj.gameObject.SetActive(false);
            ActiveObject.RemoveAt(0);
            inActiveObject.Add(obj);
        }
    }
}
public class SpawManager : MonoBehaviour
{
    private static SpawManager m_Instance;

    public static SpawManager Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = FindObjectOfType<SpawManager>();
            }
            return m_Instance;
        }
    }


    [SerializeField] private bool m_active;
    //[SerializeField] private EnemyController m_enemyPrefab;
    [SerializeField] private EnemyPool m_enemyPool;
    [SerializeField] private ProjectilePool m_EnemyProjectilePool;
    [SerializeField] private ProjectilePool m_PlayerProjectilePool;

    [SerializeField] private int m_mintotalEnemy;
    [SerializeField] private int m_maxtotalEnemy;
    //thoi gian spaw 2 enemy lien nhau
    [SerializeField] private float m_spawEnemyInterval;
    [SerializeField] private EnemyPath[] m_enemyPath;
    [SerializeField] private int totalGroup;

    private bool m_isSpaming;


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
        //StartCoroutine(IETestCoroutine());
        
    }
    public void StarButton()
    {
        StartCoroutine(IEspawngroup(totalGroup));
    }
   
    private IEnumerator IEspawngroup(int GroupNumber)
    {
        m_isSpaming = true;
        for (int i = 0; i < GroupNumber; i++)
        {
            int totalEnemy = Random.Range(m_mintotalEnemy, m_maxtotalEnemy);
            EnemyPath path = m_enemyPath[Random.Range(0, m_enemyPath.Length)];
            yield return StartCoroutine(IEspawenemy(totalEnemy, path));
            if (i < GroupNumber - 1)
                yield return new WaitForSeconds(3f);
        }
        m_isSpaming = false;
    }
    private IEnumerator IEspawenemy(int totalEnemy, EnemyPath path)
        {
            for (int i = 0; i < totalEnemy; i++)
            {
                yield return new WaitUntil(() => m_active);
                yield return new WaitForSeconds(m_spawEnemyInterval);
            //EnemyController enemy = Instantiate(m_enemyPrefab, transform);
            EnemyController enemy = m_enemyPool.spam(path.WavePoin[0].position, transform);
                 enemy.init(path.WavePoin);
            }
        }
    public void Relaese(EnemyController obj)
    {
        m_enemyPool.Release(obj);
    }
    public projectileManager EnemyProjectSpam(Vector3 position)
    {
        projectileManager obj = m_EnemyProjectilePool.spam(position,transform);
        obj.setFromPlayer(false);
        return obj;
    }
    public projectileManager PlayerProjectSpam(Vector3 position)
    {
        projectileManager obj = m_PlayerProjectilePool.spam(position, transform);
        obj.setFromPlayer(true);
        return obj;
    }
    public void ReleasePlayerProjectile(projectileManager obj)
    {
        m_PlayerProjectilePool.Release(obj);

    }
    public void ReleaseEnemyProjectile(projectileManager obj)
    {
        m_EnemyProjectilePool.Release(obj);

    }
    public bool IsClear()
    {
        if (m_isSpaming || m_enemyPool.ActiveObject.Count > 0)
            return false;
        return true;
    }
    public void Clear()
    {
        m_enemyPool.Clear();
        m_EnemyProjectilePool.Clear();
        m_PlayerProjectilePool.Clear();

        StopAllCoroutines();
    }
}
