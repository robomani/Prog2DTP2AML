using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Coroutine m_Shake;
    public void Shake()
    {
        if (m_Shake != null)
        {
            StopCoroutine(m_Shake);
        }
        m_Shake = StartCoroutine(CameraShake());
    }

    private IEnumerator CameraShake()
    {
        Camera.main.gameObject.transform.position = Camera.main.gameObject.transform.position + Vector3.one;
        yield return new WaitForSeconds(0.1f);
        Camera.main.gameObject.transform.position = Camera.main.gameObject.transform.position - Vector3.one * 2;
        yield return new WaitForSeconds(0.1f);
        Camera.main.gameObject.transform.position = Camera.main.gameObject.transform.position + Vector3.one;

    }
}
