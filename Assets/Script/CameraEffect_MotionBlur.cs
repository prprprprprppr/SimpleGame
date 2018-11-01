using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffect_MotionBlur : MonoBehaviour {
    
    private bool MotionBlurEN = false;

    [SerializeField]
    private float blurSize;

    private Material mat;
    private Camera cam;
    private Matrix4x4 PreViewProjMatrix;

    private void Awake()
    {
        mat = new Material(Shader.Find("Hidden/MotionBlur"));
        cam = GetComponent<Camera>();
        cam.depthTextureMode |= DepthTextureMode.Depth;
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if(MotionBlurEN && mat != null)
        {
            mat.SetFloat("_BlurSize", blurSize);
            mat.SetMatrix("_PreViewProjMatrix", PreViewProjMatrix);
            Matrix4x4 curViewProjMatrix = cam.projectionMatrix * cam.worldToCameraMatrix;
            mat.SetMatrix("_CurViewProjMatrix_Inverse", curViewProjMatrix.inverse);
            PreViewProjMatrix = curViewProjMatrix;

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
