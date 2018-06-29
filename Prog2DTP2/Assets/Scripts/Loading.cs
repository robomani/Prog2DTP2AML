using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour
{
    public float m_TimeToLoad = 3;
    private float m_Timer = 0;

    private void Update()
    {
        m_Timer += Time.deltaTime;
        if (m_Timer >= m_TimeToLoad)
        {
            LevelManager.Instance.ChangeLevel("Game");
        }
    }
}
