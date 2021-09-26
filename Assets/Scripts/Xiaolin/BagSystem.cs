using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagSystem : MonoBehaviour
{
    public GameObject Inspection;
    public List<GameObject> objList = new List<GameObject>();//All the objects in the game
    public List<int> currList = new List<int>();//the objects in the current bag
    private HashSet<int> currSet = new HashSet<int>();
    public bool BagState;
    public int currIndex;
    // Start is called before the first frame update
    void Start()
    {
        currIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {

        addObject();
        openBag();
        changeObj();

    }

    private void addObject()
    {

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!currSet.Contains(0))
            {
                currList.Add(0);
                currSet.Add(0);
            }

        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            if (!currSet.Contains(1))
            {
                currList.Add(1);
                currSet.Add(1);
            }
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            if (!currSet.Contains(2))
            {
                currList.Add(2);
                currSet.Add(2);
            }
        }
    }

    private void openBag()
    {

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

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currIndex != 0)
            {
                objList[currList[currIndex]].SetActive(false);
                currIndex--;
                objList[currList[currIndex]].SetActive(true);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currIndex != currList.Count - 1)
            {
                objList[currList[currIndex]].SetActive(false);
                currIndex++;
                objList[currList[currIndex]].SetActive(true);
            }

        }
    }
}
