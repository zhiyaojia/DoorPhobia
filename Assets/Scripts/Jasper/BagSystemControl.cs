using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagSystemControl : MonoBehaviour
{
    public GameObject Inspection;
    [Tooltip("Contains all objects player can store in the bag")]
    public List<GameObject> AllObjectsList = new List<GameObject>();
    [Tooltip("Contains current objects index in the bag")]
    public List<int> CurrentObjectIndexList = new List<int>();
    
    private bool bagIsOpening = false;
    private int currentInspectionObjectIndex = -1;

    public static BagSystemControl Instance { get; set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (bagIsOpening == false)
            {
                openBag();
            }
            else
            {
                closeBag();
            }
        }

        if(bagIsOpening == true && CurrentObjectIndexList.Count > 0)
        {
            changeObj();
        }
    }

    private void openBag()
    {
        bagIsOpening = true;
        Inspection.SetActive(true);
        if (CurrentObjectIndexList.Count > 0)
        {
            AllObjectsList[CurrentObjectIndexList[0]].SetActive(true);
            currentInspectionObjectIndex = 0;
        }
        PlayerControl.Instance.playerMovement.enabled = false;
    }

    private void closeBag()
    {
        bagIsOpening = false;
        Inspection.SetActive(false);
        if (currentInspectionObjectIndex >= 0)
        {
            AllObjectsList[currentInspectionObjectIndex].SetActive(false);
            currentInspectionObjectIndex = -1;
        }
        PlayerControl.Instance.playerMovement.enabled = true;
    }

    private void changeObj()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (currentInspectionObjectIndex > 0)
            {
                AllObjectsList[CurrentObjectIndexList[currentInspectionObjectIndex]].SetActive(false);
                currentInspectionObjectIndex--;
                AllObjectsList[CurrentObjectIndexList[currentInspectionObjectIndex]].SetActive(true);
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (currentInspectionObjectIndex < CurrentObjectIndexList.Count-1)
            {
                AllObjectsList[CurrentObjectIndexList[currentInspectionObjectIndex]].SetActive(false);
                currentInspectionObjectIndex++;
                AllObjectsList[CurrentObjectIndexList[currentInspectionObjectIndex]].SetActive(true);
            }

        }
    }

    public void AddObject(int index)
    {
        CurrentObjectIndexList.Add(index);
    }
}
