using UnityEngine;

public class Drone : MonoBehaviour
{
    public int DroneID { get; private set; }

    public void Initialize(int id)
    {
        DroneID = id;
        Debug.Log($"Drone ID {DroneID}: Инициализирован.");
    }
}