using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    private AudioManager m_AudioManager;
    public float m_TimeLimit;
    private float m_Time;
    public TextMeshProUGUI m_TimeText;
    public TextMeshPro m_PlayerHp;

    private bool m_Menu = false;

    private void Start()
    {
        if (AudioManager.Instance != null)
        {
            m_AudioManager = AudioManager.Instance;
            m_AudioManager.GameStart();
        }
        

    }

    private void Update()
    {
        if (m_Menu)
        {

        }
        else if (LevelManager.Instance != null && !LevelManager.Instance.m_Pause)
        {
            m_Time += Time.deltaTime;
            if (m_TimeLimit <= m_Time)
            {
                if (LevelManager.Instance != null)
                {
                    LevelManager.Instance.m_Win = false;
                    LevelManager.Instance.ChangeLevel("Result");
                }

            }
            else if (m_PlayerHp.text != "0")
            {
                m_TimeText.text = ((int)(m_TimeLimit - m_Time)).ToString();
                if (LevelManager.Instance.m_Enemy.Count == 0)
                {
                    LevelManager.Instance.m_Win = true;
                    LevelManager.Instance.ChangeLevel("Result");
                }
            }
        }
        else if(LevelManager.Instance != null && LevelManager.Instance.m_Pause)
        {
            m_TimeText.text = "PAUSE";
        }
    }

    public void MainMenu()
    {
        if (LevelManager.Instance != null)
        {
            Time.timeScale = 1;
            m_Menu = true;
            LevelManager.Instance.ChangeLevel("MainMenu");            
        }
    }

    public void Pause()
    {
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.m_Pause = !LevelManager.Instance.m_Pause;
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale = 0;
            }
        }
    }

}
