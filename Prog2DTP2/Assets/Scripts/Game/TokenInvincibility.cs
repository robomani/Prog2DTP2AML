using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenInvincibility : MonoBehaviour
{ 
    private void OnTriggerStay2D(Collider2D i_Other)
    {
        if (i_Other.GetComponent<PlayerMovement>())
        {
            if (i_Other.GetComponent<PlayerMovement>().m_Invincibility <= 0)
            {
                i_Other.GetComponent<PlayerMovement>().m_Invincibility = 0;
            }
            i_Other.GetComponent<PlayerMovement>().m_Invincibility += 5;
            Destroy(gameObject);
        }
    }
}
