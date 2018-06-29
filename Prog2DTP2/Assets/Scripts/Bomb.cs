using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public AudioClip m_DropSound;
    public AudioClip m_FuseSound;
    public AudioClip m_BoomSound;

    public AudioSource m_AudioSource;

    public float m_FuseTime = 2f;
    public float m_DeleteTime = 3f;

    public int m_PosX;
    public int m_PosY;

    public int m_Range;

    public GameObject m_Flame;

    private float m_Timer = 0;

    private int m_Rendu = 0;
    private Color m_Color;

    private void Start()
    {
        m_Color = gameObject.GetComponent<Renderer>().material.color;
    }

    private void Update()
    {
        m_Timer += Time.deltaTime;
        gameObject.GetComponent<Renderer>().material.color = Color.Lerp(Color.white, Color.blue, Mathf.PingPong(Time.time * m_DeleteTime, 1.0f));
        if (m_Timer > 0.2f && m_Timer < m_FuseTime && m_Rendu == 0)
        {
            m_Rendu++;
            m_AudioSource.clip = m_FuseSound;
            m_AudioSource.Play();
        }
        else if(m_Timer >= m_FuseTime && m_Timer < m_DeleteTime && m_Rendu == 1)
        {
            m_Rendu++;
            m_AudioSource.clip = m_BoomSound;
            m_AudioSource.Play();
        }
        else if(m_Timer >= m_DeleteTime)
        {
            Debug.Log("PosX : " + m_PosX + " / PosY : " + m_PosY);
            for (int i = 1; i <= m_Range; i++)
            {
                if (LevelGenerator.Instance.GetTileTypeAtPos(m_PosY + i, m_PosX) == ETileType.Floor || LevelGenerator.Instance.GetTileTypeAtPos(m_PosY + i, m_PosX) == ETileType.Trap)
                {
                    Debug.Log("Bas -> X: " + m_PosX + " | Y: " + (m_PosY + i));
                    GameObject flame = Instantiate(m_Flame, LevelGenerator.Instance.GetPositionAt(m_PosY + i, m_PosX), Quaternion.identity);
                }
                else if (LevelGenerator.Instance.GetTileTypeAtPos(m_PosY + i, m_PosX) == ETileType.DestructibleWall)
                {
                    LevelGenerator.Instance.SetTileTypeAtPos(m_PosY + i, m_PosX, ETileType.Floor);
                    break;
                }
                else
                {
                    Debug.Log(LevelGenerator.Instance.GetTileTypeAtPos(m_PosY + i, m_PosX));
                    break;
                }
            }

            for (int i = 1; i <= m_Range; i++)
            {
                if (LevelGenerator.Instance.GetTileTypeAtPos(m_PosY - i, m_PosX) == ETileType.Floor || LevelGenerator.Instance.GetTileTypeAtPos(m_PosY + i, m_PosX) == ETileType.Trap)
                {
                    Debug.Log("UP -> X: " + m_PosX + " | Y: " + (m_PosY - i));
                    GameObject flame = Instantiate(m_Flame, LevelGenerator.Instance.GetPositionAt(m_PosY - i, m_PosX), Quaternion.identity);
                }
                else if (LevelGenerator.Instance.GetTileTypeAtPos(m_PosY - i, m_PosX) == ETileType.DestructibleWall)
                {
                    LevelGenerator.Instance.SetTileTypeAtPos(m_PosY - i, m_PosX, ETileType.Floor);
                    break;
                }
                else
                {
                    Debug.Log(LevelGenerator.Instance.GetTileTypeAtPos(m_PosY - i, m_PosX));
                    break;
                }
            }

            for (int i = 1; i <= m_Range; i++)
            {
                if (LevelGenerator.Instance.GetTileTypeAtPos(m_PosY, m_PosX + i) == ETileType.Floor || LevelGenerator.Instance.GetTileTypeAtPos(m_PosY + i, m_PosX) == ETileType.Trap)
                {
                    Debug.Log("Right -> X: " + (m_PosX + i) + " | Y: " + m_PosY);
                    GameObject flame = Instantiate(m_Flame, LevelGenerator.Instance.GetPositionAt(m_PosY, m_PosX + i), Quaternion.identity);
                }
                else if (LevelGenerator.Instance.GetTileTypeAtPos(m_PosY, m_PosX + i) == ETileType.DestructibleWall)
                {
                    LevelGenerator.Instance.SetTileTypeAtPos(m_PosY, m_PosX + i, ETileType.Floor);
                    break;
                }
                else
                {
                    Debug.Log(LevelGenerator.Instance.GetTileTypeAtPos(m_PosY, m_PosX + i));
                    break;
                }
            }

            for (int i = 1; i <= m_Range; i++)
            {
                if (LevelGenerator.Instance.GetTileTypeAtPos(m_PosY, m_PosX - i) == ETileType.Floor || LevelGenerator.Instance.GetTileTypeAtPos(m_PosY + i, m_PosX) == ETileType.Trap)
                {
                    Debug.Log("Left -> X: " + (m_PosX - i) + " | Y: " + m_PosY);
                    GameObject flame = Instantiate(m_Flame, LevelGenerator.Instance.GetPositionAt(m_PosY, m_PosX - i), Quaternion.identity);
                }
                else if (LevelGenerator.Instance.GetTileTypeAtPos(m_PosY, m_PosX - i) == ETileType.DestructibleWall)
                {
                    LevelGenerator.Instance.SetTileTypeAtPos(m_PosY, m_PosX - i, ETileType.Floor);
                    break;
                }
                else
                {
                    Debug.Log(LevelGenerator.Instance.GetTileTypeAtPos(m_PosY, m_PosX - i));
                    break;
                }
            }
            Destroy(gameObject);
        }
    }
}
