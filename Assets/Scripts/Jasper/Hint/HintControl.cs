using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintControl : MonoBehaviour
{
    public Shader myShader;

    [Header("Materials")]
    public Material targetMaterial;
    public Material highlightMaterial;
    public Material hintMaterial;
    private Material myMaterial;

    public List<MeshRenderer> highlightObjects = new List<MeshRenderer>();
    private List<Material> highlightObjectOriginalMaterials = new List<Material>();

    private float intensity = 0.0f;
    private float paddingTime = 2.0f;
    private float currTimer = 0.0f;

    void Awake()
    {
        myMaterial = new Material(myShader);
        myMaterial.SetColor("_HighlightColor", highlightMaterial.color);
        myMaterial.SetColor("_TargetColor", targetMaterial.color);
        myMaterial.SetFloat("_bwBlend", 1.0f);

        hintMaterial.SetColor("_TargetColor", targetMaterial.color);
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
        //myMaterial.SetFloat("_bwBlend", intensity);
        Graphics.Blit(source, destination, myMaterial);
    }

    private void OnEnable()
    {
        highlightObjectOriginalMaterials.Clear();
        for (int i = 0; i < highlightObjects.Count; i++)
        {
            highlightObjectOriginalMaterials.Add(highlightObjects[i].material);
            highlightObjects[i].material = hintMaterial;
        }
    }

    private void OnDisable()
    {
        for(int i = 0; i < highlightObjects.Count; i++)
        {
            highlightObjects[i].material = highlightObjectOriginalMaterials[i];
        }
        highlightObjectOriginalMaterials.Clear();
        highlightObjects.Clear();
    }
}
