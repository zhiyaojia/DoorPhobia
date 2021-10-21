using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoyLivingRoomDoor : MonoBehaviour
{
    float startTime;
    float stayTime;
    public Transform player;
    public PlayerControl playerControl;
    // Start is called before the first frame update
    void Start()
    {
        startTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.transform == player) {
            string currentRoom = playerControl.currentRoom;
            // update staytime and starttime
            stayTime = playerControl.secondsElapsed - startTime;
            startTime = playerControl.secondsElapsed;
            Debug.Log("staytime= " + stayTime);
            if (currentRoom == "BoyLivingRoom") {
                playerControl.currentRoom = "Corridor";
                playerControl.eachRoomStayTime["BoyLivingRoom"] = (float)(playerControl.eachRoomStayTime["BoyLivingRoom"]) + stayTime;
                // (float)(playerControl.eachRoomStayTime["BoyLivingRoom"]) += stayTime;
                Debug.Log("staytime= " + stayTime);
            }
            if (currentRoom == "Corridor") {
                playerControl.currentRoom = "BoyLivingRoom";
                playerControl.eachRoomStayTime["Corridor"] = (float)(playerControl.eachRoomStayTime["Corridor"]) + stayTime;
                Debug.Log("staytime= " + stayTime);
            }
            
        }
    }
}
