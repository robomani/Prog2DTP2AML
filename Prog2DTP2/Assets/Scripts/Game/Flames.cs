using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flames : MonoBehaviour
{
    public float m_FireTime = 2f;

    private float m_Timer = 0;

    public int m_Col = 0;
    public int m_Row = 0;

    private void Update()
    {
        if (LevelManager.Instance != null && !LevelManager.Instance.m_Pause)
        {
            m_Timer += Time.deltaTime;
            if (m_Timer >= m_FireTime)
            {
                Destroy(gameObject);
            }
        }         
    }

    private void OnDestroy()
    {
        if (LevelManager.Instance != null && LevelManager.Instance.m_Flames.Count > 0)
        {
            List<Vector2> temp = LevelManager.Instance.m_Flames;
            temp.RemoveAt(temp.FindIndex(position => position == new Vector2Int(m_Col, m_Row)));
        }
    }

}
