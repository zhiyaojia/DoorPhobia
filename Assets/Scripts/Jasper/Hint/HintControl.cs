using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintControl : MonoBehaviour
{
    public Shader myShader;
    private Material myMaterial;
    private float intensity = 0.0f;

    private bool isShowingHint = false;
    private float paddingTime = 2.0f;
    private float currTimer = 0.0f;

    void Start()
    {
        myMaterial = new Material(myShader);
    }

    private void Update()
    {
        if (intensity < 1.0f)
        {
            currTimer += Time.deltaTime;
            intensity = currTimer / paddingTime;
        }
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        myMaterial.SetFloat("_bwBlend", intensity);
        Graphics.Blit(source, destination, myMaterial);
    }
}
