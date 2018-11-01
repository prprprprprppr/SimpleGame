using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleHPBar : MonoBehaviour {
    
    [Range(0.0f,1.0f)]
    public float Per;
    public float Offset;
    
    private RectTransform rt;

    private void Awake()
    {
        rt = GetComponent<RectTransform>();
    }

    private void Update()
    {
        rt.offsetMax = new Vector2(Offset * (Per-1), 0);
    }

}
