using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControl : MonoBehaviour
{
    [Tooltip("The max distance between the state position and raycast hit point that will trigger mesh collider ray cast")]
    public float validStateDistance;

    public Material notSelectedMaterial, selectedMaterial;

    [Tooltip("States that want to be selected")]
    public List<string> stateCandidateName;
    public List<GameObject> StateList;
    private List<MeshCollider> ColliderList;
    private List<bool> stateStatus; // false indicates not selected
    private HashSet<string> stateCandidateNameSet;

    private BoxCollider myCollider;

    private long correctCandidateNumber = 0;
    private long currentCandidateNumber = 0;
    private int currHoverIndex = 0;
    private bool isHovering = false;

    private void Awake()
    {
        ColliderList = new List<MeshCollider>();
        stateStatus = new List<bool>();
        stateCandidateNameSet = new HashSet<string>();

        myCollider = GetComponentInParent<BoxCollider>();

        foreach (string name in stateCandidateName)
        {
            stateCandidateNameSet.Add(name);
        }

        for (int i = 0; i < StateList.Count; i++)
        {
            ColliderList.Add(StateList[i].GetComponent<MeshCollider>());
            stateStatus.Add(false);

            if (stateCandidateNameSet.Contains(StateList[i].name))
            {
                long stateNum = 1 << i;
                correctCandidateNumber = correctCandidateNumber ^ stateNum;
            }
        }
    }

    private void OnEnable()
    {
        StartCoroutine("Hover");
    }

    private void OnDisable()
    {
        StateList[currHoverIndex].GetComponent<cakeslice.Outline>().OnDisable();
        StopCoroutine("Hover");
    }

    void Update()
    {
        if (isHovering == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                stateStatus[currHoverIndex] = !stateStatus[currHoverIndex];
                if (stateStatus[currHoverIndex] == true)
                {
                    int stateNum = 1 << currHoverIndex;
                    currentCandidateNumber = currentCandidateNumber ^ stateNum;
                }
                else
                {
                    int stateNum = ~(1 << currHoverIndex);
                    currentCandidateNumber = currentCandidateNumber & stateNum;
                }
                if (currentCandidateNumber == correctCandidateNumber)
                {
                    print("correct states");
                }

                StateList[currHoverIndex].GetComponent<MeshRenderer>().material =
                    stateStatus[currHoverIndex] ? selectedMaterial : notSelectedMaterial;
            }
        }
    }

    IEnumerator Hover()
    {
        while (true)
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