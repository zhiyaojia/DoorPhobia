using UnityEngine;

public class ElevatorFunction : MonoBehaviour
{
    public void StartMove()
    {
        ElevatorControl.Instance.StartMove();
    }

    public void OpenDoor()
    {
        ElevatorControl.Instance.OpenDoor();
    }

    public void FinishOpenDoor()
    {
        ElevatorControl.Instance.FinishOpenDoor();
    }
}
