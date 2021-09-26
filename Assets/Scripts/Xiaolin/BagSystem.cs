using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class BagSystem : MonoBehaviour
{
    public GameObject Inspection;
    public List<GameObject> objList = new List<GameObject>();//All the objects in the game
    public List<int> currList = new List<int>();//the objects in the current bag
    public bool BagState;
    public int currIndex;
    public PostProcessResources postProcessResources;
    
    void Awake()
    {
        PostProcessLayer postProcessLayer = Camera.main.gameObject.GetComponent<PostProcessLayer>();
        postProcessLayer.Init(postProcessResources);
        currIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("test");
        addObject();
        openBag();
        changeObj();

    }

    private void addObject()
    {
        //Debug.Log("test-addObject");
        if(Input.GetKeyDown(KeyCode.Z))
        {
            currList.Add(0);
        }
        else if(Input.GetKeyDown(KeyCode.X))
        {
            currList.Add(1);
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            currList.Add(2);
        }
    }

    private void openBag()
    {
        //Debug.Log("test-openBag");
        BagState = Inspection.activeSelf;
        if (Input.GetKeyDown(KeyCode.B))
        {
            BagState = !BagState;
            Inspection.SetActive(BagState);
            if (BagState)
            {
                objList[currList[0]].SetActive(true);
            }
            else
            {
                objList[currList[currIndex]].SetActive(false);
            }

        }
    }
    private void changeObj()
    {
        //Debug.Log("test-changeObj");
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(currIndex != 0)
            {
                objList[currList[currIndex]].SetActive(false);
                currIndex--;
                objList[currList[currIndex]].SetActive(true);
            }
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(currIndex != currList.Count - 1)
            {
                objList[currList[currIndex]].SetActive(false);
                currIndex++;
                objList[currList[currIndex]].SetActive(true);
            }

        }
    }
}
