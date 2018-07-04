using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float m_Speed = 1;

    private int m_CurrentRow;
    private int m_CurrentCol;

    private float m_Invincibility = 3f;

    private bool m_IsMoving = false;

    private Vector2 m_InitialPos;
    private Vector2 m_WantedPos;

    private float m_PercentageCompletion;

    public void Setup(int aRow, int aCol)
    {
        m_CurrentRow = aRow;
        m_CurrentCol = aCol;
    }

    // Update is called once per frame
    private void Update()
    {
        if (m_Invincibility > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().material.color = Color.blue;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().material.color = Color.white;
        }

        if (LevelManager.Instance != null && !LevelManager.Instance.m_Pause)
        {
            List<Vector2> temp = LevelManager.Instance.m_Flames;
            Vector2Int pos = new Vector2Int(m_CurrentCol, m_CurrentRow);

            m_Invincibility -= Time.deltaTime;
            if (m_Invincibility <= 0 && (temp.Exists(position => position == pos) || temp.Exists(position => position == m_WantedPos)))
            {
                Destroy(gameObject);
            }

            if (!m_IsMoving)
            {
                float askMoveHorizontal = 0;
                float askMoveVertical = 0;

                

                int move = Random.Range(0, 3);
                switch (move)
                {
                    case 0:
                        askMoveHorizontal = 1;
                        askMoveVertical = 0;
                        break;
                    case 1:
                        askMoveHorizontal = -1;
                        askMoveVertical = 0;
                        break;
                    case 2:
                        askMoveVertical = 1;
                        askMoveHorizontal = 0;
                        break;
                    case 3:
                        askMoveVertical = -1;
                        askMoveHorizontal = 0;
                        break;
                }

                if (askMoveHorizontal != 0 && (LevelGenerator.Instance.GetTileTypeAtPos(m_CurrentRow, m_CurrentCol + (int)askMoveHorizontal) == ETileType.Floor || LevelGenerator.Instance.GetTileTypeAtPos(m_CurrentRow, m_CurrentCol + (int)askMoveHorizontal) == ETileType.Trap))
                {
                    if (askMoveHorizontal > 0)
                    {
                        gameObject.GetComponent<Animator>().SetBool("WalkRight", true);
                        gameObject.GetComponent<SpriteRenderer>().flipX = false;
                    }
                    else
                    {
                        gameObject.GetComponent<Animator>().SetBool("WalkLeft", true);
                        gameObject.GetComponent<SpriteRenderer>().flipX = true;
                    }
                    m_IsMoving = true;
                    m_PercentageCompletion = 0f;

                    m_InitialPos = transform.position;
                    m_WantedPos = LevelGenerator.Instance.GetPositionAt(m_CurrentRow, m_CurrentCol + (int)askMoveHorizontal);

                    m_CurrentCol += (int)askMoveHorizontal;
                }
                else if (askMoveVertical != 0 && (LevelGenerator.Instance.GetTileTypeAtPos(m_CurrentRow - (int)askMoveVertical, m_CurrentCol) == ETileType.Floor || LevelGenerator.Instance.GetTileTypeAtPos(m_CurrentRow - (int)askMoveVertical, m_CurrentCol) == ETileType.Trap))
                {
                    if (askMoveVertical > 0)
                    {
                        gameObject.GetComponent<Animator>().SetBool("WalkUp", true);
                    }
                    else
                    {
                        gameObject.GetComponent<Animator>().SetBool("WalkDown", true);
                    }
                    m_IsMoving = true;
                    m_PercentageCompletion = 0f;

                    m_InitialPos = transform.position;
                    m_WantedPos = LevelGenerator.Instance.GetPositionAt(m_CurrentRow - (int)askMoveVertical, m_CurrentCol);

                    m_CurrentRow -= (int)askMoveVertical;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (LevelManager.Instance != null && !LevelManager.Instance.m_Pause)
        {
            if (m_IsMoving)
            {
                m_PercentageCompletion += Time.fixedDeltaTime * m_Speed;
                m_PercentageCompletion = Mathf.Clamp(m_PercentageCompletion, 0f, 1f);

                transform.position = Vector3.Lerp(m_InitialPos, m_WantedPos, m_PercentageCompletion);

                if (m_PercentageCompletion >= 1)
                {
                    m_IsMoving = false;
                    gameObject.GetComponent<Animator>().SetBool("WalkUp", false);
                    gameObject.GetComponent<Animator>().SetBool("WalkDown", false);
                    gameObject.GetComponent<Animator>().SetBool("WalkRight", false);
                    gameObject.GetComponent<Animator>().SetBool("WalkLeft", false);
                }
            }
        }
    }

    private void OnDestroy()
    {
        if (LevelManager.Instance != null && LevelManager.Instance.m_Enemy.Count > 0)
        {
            List<EnemyMovement> temp = LevelManager.Instance.m_Enemy;

            temp.RemoveAt(temp.FindIndex(position => position == this));
        }
    }

    public Vector2 GetPos()
    {
        return new Vector2(m_CurrentCol, m_CurrentRow);
    }
}
