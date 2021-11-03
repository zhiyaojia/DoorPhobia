using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorControl : MonoBehaviour
{
    public static ElevatorControl Instance { get; set; }

    [Header("Animation")]
    public Animation elevatorAnimation;
    public List<Animation> gateAnimations;

    [Header("Three Floors")]
    public List<GameObject> Floors;

    [Header("Elevator Settings")]
    public Transform elevatorModel;
    public Collider airWall;
    public int currentFloorIndex = 3;

    private HashSet<int> accessableFloors;
    private int direction;
    private IEnumerator movingCoroutine;

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
        accessableFloors = new HashSet<int>();
        accessableFloors.Add(3);
        accessableFloors.Add(2);
    }

    void Start()
    {
        direction = 0;
        movingCoroutine = null;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            PressButton(true);
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            PressButton(false);
        }
    }

    public void MakeFloorAccessable(int index)
    {
        accessableFloors.Add(index);
    }

    public void PressButton(bool up)
    {
        direction = up ? 1 : -1;
        int targetFloor = currentFloorIndex + direction;
        if (accessableFloors.Contains(targetFloor) == false)
        {
            return;
        }
        gateAnimations[currentFloorIndex - 1].Play("Close");
        currentFloorIndex = targetFloor;
        airWall.enabled = true;
    }

    public void StartMove()
    {
        if (direction > 0)
        {
            if (currentFloorIndex == 2)
            {
                elevatorAnimation.Play("One_Two");
            }
            else
            {
                elevatorAnimation.Play("Two_Three");
            }
        }
        else
        {
            if (currentFloorIndex == 2)
            {
                elevatorAnimation.Play("Three_Two");
            }
            else
            {
                elevatorAnimation.Play("Two_One");
            }
        }

        movingCoroutine = UpdatePlayerPosition();
        StartCoroutine(movingCoroutine);
    }

    IEnumerator UpdatePlayerPosition()
    {
        float lastY = elevatorModel.position.y;
        float currentY;
        float diffY;
        Transform player = PlayerControl.Instance.transform;
        Vector3 playerPos = player.position;
        float playerY = playerPos.y;
        while (true)
        {
            currentY = elevatorModel.position.y;
            diffY = currentY - lastY;
            playerY += diffY;
            playerPos.y = playerY;
            player.position = playerPos;
            print(Time.realtimeSinceStartup + ", " + diffY + ", " + elevatorModel.position.y + ", " + playerPos.y);
            yield return null;

            lastY = currentY;
        }
    }

    public void OpenDoor()
    {
        gateAnimations[currentFloorIndex - 1].Play("Open");
        if (movingCoroutine != null)
        {
            StopCoroutine(movingCoroutine);
        }
    }

    public void FinishOpenDoor()
    {
        airWall.enabled = false;
    }
}
