using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    public IEnumerator Shake(float duration = 0.15f, float magnitude = 0.2f)
    {
        Vector3 orignalPosition = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.position = new Vector3(orignalPosition.x + x, orignalPosition.y + y, transform.position.z);

            elapsed += Time.deltaTime;

            yield return 0;
        }

        transform.position = orignalPosition;
    }


}