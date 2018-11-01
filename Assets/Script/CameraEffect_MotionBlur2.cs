using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffect_MotionBlur2 : MonoBehaviour {
    
    private bool MotionBlurEN = false;

    [SerializeField]
    private float BlurScale;
    [SerializeField][Range(1,10)]
    private int _InterNum;

    private Material mat;
    

    private void Awake()
    {
        mat = new Material(Shader.Find("Hidden/MotionBlur2"));
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if(MotionBlurEN && mat != null)
        {
            mat.SetInt("_InterNum", _InterNum);
            mat.SetFloat("_Scale", BlurScale);

            Graphics.Blit(source, destination, mat);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }

    public void setMotionBlurEN(bool b)
    {
        MotionBlurEN = b;
    }
}
