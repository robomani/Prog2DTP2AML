using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialisator : MonoBehaviour
{
    private void Start()
    {
        LevelManager.Instance.ChangeLevel("MainMenu");
    }
}
