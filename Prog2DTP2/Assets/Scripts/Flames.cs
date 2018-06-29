using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flames : MonoBehaviour
{
    public float m_FireTime = 2f;

    private float m_Timer = 0;

    private void Update()
    {
        m_Timer += Time.deltaTime;
        if (m_Timer >= m_FireTime)
        {
            Destroy(gameObject);
        }
    }
}
