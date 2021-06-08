using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMoveCamera : MonoBehaviour
{
    [SerializeField] float autoCameraSpeed = 0.5f;

    Vector3 startValue;
    Vector3 endValue;
    float timeElapsed=0;
    // The total duration should be how long it'll take to get to the center
    float lerpDuration;

    // Start is called before the first frame update
    void Start()
    {
        startValue = transform.position;
        lerpDuration = Mathf.Abs(transform.position.z/autoCameraSpeed);
        Debug.Log(lerpDuration);
        endValue = new Vector3(0,0,0);
        StartCoroutine(LerpPosition(endValue, lerpDuration));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0f;
        Vector3 startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time/duration);
            time += Time.deltaTime;
            yield return null;
        }

    }
}
