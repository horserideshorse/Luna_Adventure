using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDestroy : MonoBehaviour
{
    public float destroyTime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Timer());
    }

    IEnumerator Timer() {
        yield return new WaitForSecondsRealtime(destroyTime);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        StopCoroutine(Timer());
    }
}
