using Bases;
using UnityEngine;
using Utilities;

namespace Drones
{
    public class Drone : MonoBehaviour
    {
        public int DroneID { get; private set; }
        public GameObject BaseObj { get; private set; }
        public Base Base { get; private set; }
        public Enums.DroneState State { get; set; }

        public void Initialize(int id, GameObject baseObj)
        {
            DroneID = id;
            BaseObj = baseObj;
            Base = baseObj.GetComponent<Base>();
            GetComponent<MeshRenderer>().material = baseObj.GetComponent<MeshRenderer>().material;
            Debug.Log($"Drone ID {DroneID}: Инициализирован. {Base}");
        }
    }
}