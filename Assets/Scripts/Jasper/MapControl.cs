using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControl : MonoBehaviour
{
    public float validStateDistance;

    public Material redMaterial, blueMaterial;

    public List<GameObject> StateList;
    private List<MeshCollider> ColliderList;
    private List<bool> stateColor;

    private BoxCollider myCollider;

    private int currHoverIndex = 0;
    private bool isHovering = false;

    void Start()
    {
        ColliderList = new List<MeshCollider>();
        stateColor = new List<bool>();

        myCollider = GetComponentInParent<BoxCollider>();
        for (int i = 0; i < StateList.Count; i++)
        {
            StateList[i].GetComponent<cakeslice.Outline>().OnDisable();
            ColliderList.Add(StateList[i].GetComponent<MeshCollider>());
            stateColor.Add(false);
        }

        StartCoroutine("Hover");
    }

    void Update()
    {
        if (isHovering == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                stateColor[currHoverIndex] = !stateColor[currHoverIndex];
                StateList[currHoverIndex].GetComponent<MeshRenderer>().material = stateColor[currHoverIndex] ?blueMaterial:redMaterial;
            }
        }
    }

    IEnumerator Hover()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.2f);

            RaycastHit hit, innerHit;
            isHovering = false;
            if (myCollider.Raycast(PlayerControl.Instance.rayFromScreenCenter, out hit, Mathf.Infinity))
            {
                for (int i = 0; i < StateList.Count; i++)
                {
                    if (Vector3.Distance(hit.point, StateList[i].transform.position) < validStateDistance)
                    {
                        if (ColliderList[i].Raycast(PlayerControl.Instance.rayFromScreenCenter, out innerHit, Mathf.Infinity))
                        {
                            isHovering = true;
                            if (i != currHoverIndex)
                            { 
                                StateList[currHoverIndex].GetComponent<cakeslice.Outline>().OnDisable();
                                StateList[i].GetComponent<cakeslice.Outline>().OnEnable();
                                currHoverIndex = i;
                            }
                            break;
                        }
                    }
                }
            }
        }
    }
}
