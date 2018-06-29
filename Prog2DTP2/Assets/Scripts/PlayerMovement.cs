using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float m_Speed;
    public int m_Range = 1;

    public GameObject m_Bomb;

    private int m_CurrentRow;
    private int m_CurrentCol;

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

        if (Input.GetButtonDown("Bomb") && !m_IsMoving)
        {
            GameObject bomb = Instantiate(m_Bomb,transform.position,Quaternion.identity);
            bomb.GetComponent<Bomb>().m_PosX = m_CurrentCol;
            bomb.GetComponent<Bomb>().m_PosY = m_CurrentRow;
            bomb.GetComponent<Bomb>().m_Range = m_Range;
        }
    }

    private void FixedUpdate()
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
