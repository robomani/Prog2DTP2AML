using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenBombRange : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D i_Other)
    {
        if (i_Other.GetComponent<PlayerMovement>())
        {
            i_Other.GetComponent<PlayerMovement>().UpRange();
            Destroy(gameObject);
        }
    }
}
