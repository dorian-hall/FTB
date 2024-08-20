using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class Goal : MonoBehaviour
{
    SplineAnimate splineAnimate;
    // Start is called before the first frame update
    void Start()
    {
        splineAnimate = GetComponent<SplineAnimate>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!splineAnimate.IsPlaying){ Destroy(gameObject); }
    }
}
