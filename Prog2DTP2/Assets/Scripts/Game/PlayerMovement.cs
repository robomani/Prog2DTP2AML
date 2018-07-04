using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float m_Speed;
    

    public GameObject m_BombPrefab;
    private GameObject m_Bomb;

    private int m_CurrentRow;
    private int m_CurrentCol;

    private bool m_IsMoving = false;

    private Vector2 m_InitialPos;
    private Vector2 m_WantedPos;

    public int m_HP = 3;
    public int m_Range = 1;
    public float m_Invincibility = 0;

    public TextMeshPro m_LifeText;
    public TextMeshPro m_RangeText;

    private float m_PercentageCompletion;

    public int Row
    {
        get { return m_CurrentRow; }
    }

    public int Col
    {
        get { return m_CurrentCol; }
    }

    public void Setup(int aRow, int aCol)
    {
        m_CurrentRow = aRow;
        m_CurrentCol = aCol;
    }

    private void Start()
    {
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.m_Player = this;
        }
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
            m_Invincibility -= Time.deltaTime;

            List<Vector2> temp = LevelManager.Instance.m_Flames;
            Vector2Int pos = new Vector2Int(m_CurrentCol, m_CurrentRow);
            if (m_Invincibility <= 0f && (temp.Exists(position => position == pos) || temp.Exists(position => position == m_WantedPos)))
            {
                LoseLife();
            }

            List<EnemyMovement> tempEnemy = LevelManager.Instance.m_Enemy;
            if (m_Invincibility <= 0f && (tempEnemy.Exists(position => position.GetPos() == pos) || tempEnemy.Exists(position => position.GetPos() == m_WantedPos)))
            {
                LoseLife();
            }

            if (!m_IsMoving)
            {
                float askMoveHorizontal = Input.GetAxisRaw("Horizontal");
                float askMoveVertical = Input.GetAxisRaw("Vertical");

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

            if (Input.GetButtonDown("Bomb") && !m_IsMoving && m_Bomb == null)
            {
                m_Bomb = Instantiate(m_BombPrefab, transform.position, Quaternion.identity);
                m_Bomb.GetComponent<Bomb>().m_PosX = m_CurrentCol;
                m_Bomb.GetComponent<Bomb>().m_PosY = m_CurrentRow;
                m_Bomb.GetComponent<Bomb>().m_Range = m_Range;
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

    public void LoseLife()
    {
        m_HP--;
        m_LifeText.text = m_HP.ToString();
        if (m_HP <= 0)
        {
            if (LevelManager.Instance != null)
            {
                LevelManager.Instance.m_Win = false;
                LevelManager.Instance.ChangeLevel("Result");
            }
        }
        m_Invincibility = 2f;
    }

    public void UpRange()
    {
        StartCoroutine(RangeTemp());
    }

    private void OnDestroy()
    {
        if(LevelManager.Instance != null)
        {
            LevelManager.Instance.m_Player = null;
        }
    }

    private IEnumerator RangeTemp()
    {
        m_Range++;
        m_RangeText.text = m_Range.ToString();
        yield return new WaitForSeconds(10f);
        m_Range--;
        m_RangeText.text = m_Range.ToString();
    }
}
