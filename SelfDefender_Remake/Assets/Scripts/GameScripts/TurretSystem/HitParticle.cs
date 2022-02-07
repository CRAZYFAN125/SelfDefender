using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitParticle : MonoBehaviour
{
    public Vector3 targetSize;
    public AnimationCurve curve;
    public float duration = 5f;
    private float x  = 0;
    // Update is called once per frame
    void Update()
    {
        if (x < duration / 2)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetSize, curve.Evaluate(Time.deltaTime*(duration/2)));
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(.1f,.1f,.1f), curve.Evaluate(Time.deltaTime*(duration/2)));
        }
        if (x > duration)
        {
            Destroy(gameObject);
            return;
        }
        x+=Time.deltaTime;
    }
}
