using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintControl : MonoBehaviour
{
    public Shader myShader;

    [Header("Materials")]
    public Material targetMaterial;
    public Material highlightMaterial;
    public Material hintMaterial;
    public Material highlightStartMateial;
    private Material myMaterial;

    [HideInInspector] public List<Renderer> hintObjects = new List<Renderer>();
    [HideInInspector] public List<Material> hintObjectOriginalMaterials = new List<Material>();

    [Header("Time Setting")]
    public float paddingTime = 0.0f;
    private float intensity = 1.0f;
    private float currTimer = 0.0f;

    [Header("Mode Setting")]
    public Text text;
    private bool hintIsInBag = false;

    void Awake()
    {
        myMaterial = new Material(myShader);
        myMaterial.SetColor("_HighlightColor", highlightMaterial.color);
        myMaterial.SetColor("_TargetColor", targetMaterial.color);
        myMaterial.SetColor("_HighlightStartColor", highlightStartMateial.color);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (intensity < 1.0f)
        {
            myMaterial.SetFloat("_bwBlend", intensity);
        }
        Graphics.Blit(source, destination, myMaterial);
    }

    private void OnEnable()
    {
        intensity = 0.0f;
        currTimer = 0.0f;

        if (hintObjects.Count > 0 && hintObjects[0].gameObject.activeInHierarchy == false)
        {
            hintIsInBag = true;
            text.text = "Hint is the " + hintObjects[0].gameObject.name + " in the bag";
            text.gameObject.SetActive(true);
        }
        else
        {
            hintIsInBag = false ;
            for (int i = 0; i < hintObjects.Count; i++)
            {
                hintObjects[i].material = hintMaterial;
            }
        }

        StartCoroutine(TurnOnHint());
    }

    private void OnDisable()
    {
        if (hintIsInBag)
        {
            text.gameObject.SetActive(false);
        }
        else
        {
            for (int i = 0; i < hintObjects.Count; i++)
            {
                if (hintObjectOriginalMaterials[i] == null)
                {
                    Debug.LogError("Missing original materials");
                }
                hintObjects[i].material = hintObjectOriginalMaterials[i];
            }
        }
    }

    IEnumerator TurnOnHint()
    {
        while (currTimer < paddingTime)
        {
            currTimer += Time.deltaTime;
            intensity = currTimer / paddingTime;
            yield return null;
        }
    }

    public void TurnOff()
    {
        currTimer = paddingTime;
        StartCoroutine(TurnOffHint());
    }

    IEnumerator TurnOffHint()
    {
        while (currTimer > 0.0f)
        {
            currTimer -= Time.deltaTime;
            intensity = currTimer / paddingTime;
            yield return null;
        }
        enabled = false;
    }
}
