using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    [SerializeField] private Transform[] m_WavePoin;
    [SerializeField] private Color m_color;

    public Transform[] WavePoin => m_WavePoin;    
    private void OnDrawGizmos()
    {
        Gizmos.color = m_color;
        if (m_WavePoin != null && m_WavePoin.Length > 1)
        {
            for (int i = 0;i<m_WavePoin.Length -1 ; i++)
            {
                Transform from = m_WavePoin[i]; 
                Transform to = m_WavePoin[i+1];
                Gizmos.DrawLine(from.position, to.position);
            }
        }
        Gizmos.DrawLine(m_WavePoin[0].position, m_WavePoin[m_WavePoin.Length-1].position);
    }
}
