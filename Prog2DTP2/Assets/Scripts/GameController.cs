using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private AudioManager m_AudioManager;
    private float m_TimeLimit;
    private float m_Time;


    private void Start()
    {
        m_AudioManager = AudioManager.Instance;
        m_AudioManager.GameStart();

    }

    private void Update()
    {
        m_Time += Time.deltaTime;

        if (m_TimeLimit <= m_Time)
        {
            
        }
    }

}
