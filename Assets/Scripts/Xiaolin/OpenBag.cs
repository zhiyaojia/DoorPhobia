using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBag : MonoBehaviour
{
    public GameObject MyBag;
    public MyBagObjs myBagObjs;
    private bool BagState;
    //public int index;

    void Update()
    {
        openMyBag();
    }

    
    private void openMyBag()
    {
        BagState = MyBag.activeSelf;
        if (Input.GetKeyDown(KeyCode.B))
        {
            BagState = !BagState;
            MyBag.SetActive(BagState);
            if (BagState)
            {
                myBagObjs.showObj();
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    if (myBagObjs.getCurrIndex() == 0)
                    {
                        myBagObjs.showObj();
                    }
                    else
                    {
                        myBagObjs.hideObj();
                        myBagObjs.minusCurrIndex();
                        myBagObjs.showObj();
                    }
                    
                }
                else if(Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if (myBagObjs.getCurrIndex() == myBagObjs.getCount() - 1)
                    {
                        myBagObjs.showObj();
                    }
                    else
                    {
                        myBagObjs.hideObj();
                        myBagObjs.addCurrIndex();
                        myBagObjs.showObj();
                    }
                }
            }
            else
            {
                myBagObjs.hideObj();
            }
        }
    }
}
