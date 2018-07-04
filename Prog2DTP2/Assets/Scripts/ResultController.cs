using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultController : MonoBehaviour
{
    private void Start()
    {
        if (LevelManager.Instance != null)
        {
            if (LevelManager.Instance.m_Win)
            {
                GetComponentInChildren<TextMeshProUGUI>().text = "You Won";
            }
            else
            {
                GetComponentInChildren<TextMeshProUGUI>().text = "You Lost";
            }
        }
    }

    public void Restart()
    {
        LevelManager.Instance.ChangeLevel("Loading");
    }
}
