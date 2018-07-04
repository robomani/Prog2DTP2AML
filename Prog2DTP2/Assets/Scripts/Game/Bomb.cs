using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public AudioClip m_DropSound;
    public AudioClip m_FuseSound;
    public AudioClip m_BoomSound;

    public AudioSource m_AudioSource;
    public EnemyMovement m_EnemyPrefab;

    public float m_FuseTime = 2f;
    public float m_DeleteTime = 3f;

    public int m_PosX;
    public int m_PosY;

    public int m_Range;

    public GameObject m_Flame;

    public Transform m_Camera;

    private float m_Timer = 0;

    private int m_Rendu = 0;


    private void Update()
    {
        
        gameObject.GetComponent<Renderer>().material.color = Color.Lerp(Color.white, Color.blue, Mathf.PingPong(Time.time * m_DeleteTime, 1.0f));
        if (LevelManager.Instance != null && !LevelManager.Instance.m_Pause)
        {   
            m_Timer += Time.deltaTime;
            
            if (m_Timer > 0.2f && m_Timer < m_FuseTime && m_Rendu == 0)
            {
                m_Rendu++;
                m_AudioSource.clip = m_FuseSound;
                m_AudioSource.Play();
            }
            else if (m_Timer >= m_FuseTime && m_Timer < m_DeleteTime && m_Rendu == 1)
            {
                m_Rendu++;
                m_AudioSource.clip = m_BoomSound;
                m_AudioSource.Play();
            }
            else if (m_Timer >= m_DeleteTime)
            {
                GameObject flameOnBomb = Instantiate(m_Flame, LevelGenerator.Instance.GetPositionAt(m_PosY, m_PosX), Quaternion.identity);
                flameOnBomb.GetComponent<Flames>().m_Col = m_PosX;
                flameOnBomb.GetComponent<Flames>().m_Row = m_PosY;
                if (LevelManager.Instance != null)
                {
                    LevelManager.Instance.m_Flames.Add(new Vector2Int(m_PosX, m_PosY));
                }
                for (int i = 1; i <= m_Range; i++)
                {
                    if (LevelGenerator.Instance.GetTileTypeAtPos(m_PosY + i, m_PosX) == ETileType.Trap)
                    {
                        GameObject flame = Instantiate(m_Flame, LevelGenerator.Instance.GetPositionAt(m_PosY + i, m_PosX), Quaternion.identity);
                        flame.GetComponent<Flames>().m_Col = m_PosX;
                        flame.GetComponent<Flames>().m_Row = m_PosY + i;

                        EnemyMovement ennemy = Instantiate(m_EnemyPrefab, transform.position, Quaternion.identity);
                        ennemy.Setup(m_PosY + i, m_PosX);
                        LevelManager.Instance.m_Enemy.Add(ennemy);

                        if (LevelManager.Instance != null)
                        {
                            LevelManager.Instance.m_Flames.Add(new Vector2(m_PosY + i, m_PosX));
                        }
                    }
                    else if (LevelGenerator.Instance.GetTileTypeAtPos(m_PosY + i, m_PosX) == ETileType.Floor)
                    {
                        GameObject flame = Instantiate(m_Flame, LevelGenerator.Instance.GetPositionAt(m_PosY + i, m_PosX), Quaternion.identity);
                        flame.GetComponent<Flames>().m_Col = m_PosX;
                        flame.GetComponent<Flames>().m_Row = m_PosY + i;
                        if (LevelManager.Instance != null)
                        {
                            LevelManager.Instance.m_Flames.Add(new Vector2(m_PosX, m_PosY + i));
                        }   
                    }
                    else if (LevelGenerator.Instance.GetTileTypeAtPos(m_PosY + i, m_PosX) == ETileType.DestructibleWall)
                    {
                        LevelGenerator.Instance.SetTileTypeAtPos(m_PosY + i, m_PosX, ETileType.Floor);
                        break;
                    }
                    else
                    {
                        break;
                    }
                }

                for (int i = 1; i <= m_Range; i++)
                {
                    if (LevelGenerator.Instance.GetTileTypeAtPos(m_PosY - i, m_PosX) == ETileType.Trap)
                    {
                        GameObject flame = Instantiate(m_Flame, LevelGenerator.Instance.GetPositionAt(m_PosY - i, m_PosX), Quaternion.identity);
                        flame.GetComponent<Flames>().m_Col = m_PosX;
                        flame.GetComponent<Flames>().m_Row = m_PosY - i;

                        EnemyMovement ennemy = Instantiate(m_EnemyPrefab, transform.position, Quaternion.identity);
                        ennemy.Setup(m_PosY - i, m_PosX);
                        LevelManager.Instance.m_Enemy.Add(ennemy);
                        if (LevelManager.Instance != null)
                        {
                            LevelManager.Instance.m_Flames.Add(new Vector2(m_PosY - i, m_PosX));
                        }
                    }
                    else if (LevelGenerator.Instance.GetTileTypeAtPos(m_PosY - i, m_PosX) == ETileType.Floor)
                    {
                        GameObject flame = Instantiate(m_Flame, LevelGenerator.Instance.GetPositionAt(m_PosY - i, m_PosX), Quaternion.identity);
                        flame.GetComponent<Flames>().m_Col = m_PosX;
                        flame.GetComponent<Flames>().m_Row = m_PosY - i;
                        if (LevelManager.Instance != null)
                        {
                            LevelManager.Instance.m_Flames.Add(new Vector2(m_PosX, m_PosY - i));
                        }
                    }
                    else if (LevelGenerator.Instance.GetTileTypeAtPos(m_PosY - i, m_PosX) == ETileType.DestructibleWall)
                    {
                        LevelGenerator.Instance.SetTileTypeAtPos(m_PosY - i, m_PosX, ETileType.Floor);
                        break;
                    }
                    else
                    {
                        break;
                    }
                }

                for (int i = 1; i <= m_Range; i++)
                {
                    if (LevelGenerator.Instance.GetTileTypeAtPos(m_PosY, m_PosX + i) == ETileType.Trap)
                    {
                        GameObject flame = Instantiate(m_Flame, LevelGenerator.Instance.GetPositionAt(m_PosY, m_PosX + i), Quaternion.identity);
                        flame.GetComponent<Flames>().m_Col = m_PosX + i;
                        flame.GetComponent<Flames>().m_Row = m_PosY;
                        EnemyMovement ennemy = Instantiate(m_EnemyPrefab, transform.position, Quaternion.identity);
                        ennemy.Setup(m_PosY, m_PosX + i);
                        LevelManager.Instance.m_Enemy.Add(ennemy);
                        if (LevelManager.Instance != null)
                        {
                            LevelManager.Instance.m_Flames.Add(new Vector2(m_PosY, m_PosX + i));
                        }
                    }
                    else if (LevelGenerator.Instance.GetTileTypeAtPos(m_PosY, m_PosX + i) == ETileType.Floor)
                    {
                        GameObject flame = Instantiate(m_Flame, LevelGenerator.Instance.GetPositionAt(m_PosY, m_PosX + i), Quaternion.identity);
                        flame.GetComponent<Flames>().m_Col = m_PosX + i;
                        flame.GetComponent<Flames>().m_Row = m_PosY;
                        if (LevelManager.Instance != null)
                        {
                            LevelManager.Instance.m_Flames.Add(new Vector2(m_PosX + i, m_PosY));
                        }
                    }
                    else if (LevelGenerator.Instance.GetTileTypeAtPos(m_PosY, m_PosX + i) == ETileType.DestructibleWall)
                    {
                        LevelGenerator.Instance.SetTileTypeAtPos(m_PosY, m_PosX + i, ETileType.Floor);
                        break;
                    }
                    else
                    {
                        break;
                    }
                }

                for (int i = 1; i <= m_Range; i++)
                {
                    if (LevelGenerator.Instance.GetTileTypeAtPos(m_PosY, m_PosX - i) == ETileType.Trap)
                    {
                        GameObject flame = Instantiate(m_Flame, LevelGenerator.Instance.GetPositionAt(m_PosY, m_PosX - i), Quaternion.identity);
                        flame.GetComponent<Flames>().m_Col = m_PosX - i;
                        flame.GetComponent<Flames>().m_Row = m_PosY;
                        EnemyMovement ennemy = Instantiate(m_EnemyPrefab, transform.position, Quaternion.identity);
                        ennemy.Setup(m_PosY, m_PosX - i);
                        LevelManager.Instance.m_Enemy.Add(ennemy);
                        if (LevelManager.Instance != null)
                        {
                            LevelManager.Instance.m_Flames.Add(new Vector2(m_PosX - i, m_PosY));
                        }
                    }
                    else if (LevelGenerator.Instance.GetTileTypeAtPos(m_PosY, m_PosX - i) == ETileType.Floor)
                    {
                        GameObject flame = Instantiate(m_Flame, LevelGenerator.Instance.GetPositionAt(m_PosY, m_PosX - i), Quaternion.identity);
                        flame.GetComponent<Flames>().m_Col = m_PosX - i;
                        flame.GetComponent<Flames>().m_Row = m_PosY;
                        if (LevelManager.Instance != null)
                        {
                            LevelManager.Instance.m_Flames.Add(new Vector2(m_PosX - i, m_PosY));
                        }
                    }
                    else if (LevelGenerator.Instance.GetTileTypeAtPos(m_PosY, m_PosX - i) == ETileType.DestructibleWall)
                    {
                        LevelGenerator.Instance.SetTileTypeAtPos(m_PosY, m_PosX - i, ETileType.Floor);
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                Camera.main.GetComponent<CameraController>().Shake();
                Destroy(gameObject);
            }
        }
        

    }

    
}
