using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    AudioManager m_AudioManager;

    private void Start()
    {
        m_AudioManager = AudioManager.Instance;
        m_AudioManager.MenuStart();
    }

    public void Game()
    {
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.ChangeLevel("Loading");
        }
    }
}
