using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBagObjs : MonoBehaviour
{
    public List<GameObject> list = new List<GameObject>();
    public int currIndex;
    public int getCurrIndex()
    {
        return this.currIndex;
    }

    public void showObj()
    {
        list[this.currIndex].SetActive(true);
    }

    public void hideObj()
    {
        list[this.currIndex].SetActive(false);
    }
    public void addCurrIndex()
    {
        this.currIndex++;
    }
    public void minusCurrIndex()
    {
        this.currIndex--;
    }

    public int getCount()
    {
        return list.Count;
    }
}
