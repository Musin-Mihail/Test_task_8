using UnityEngine;

namespace Utilities
{
    public class Enums : MonoBehaviour
    {
        public enum DroneState
        {
            SearchingForResource,
            MovingToResource,
            CollectingResource,
            DeliveringResource
        }
    }
}