using UnityEngine;

namespace Utils
{
    public class CalculateMinDistance : MonoBehaviour
    {
        public static CalculateMinDistance Instance { get; private set; }

        private float closestDistance = float.MaxValue;
        private float previousDistance;

        private void Awake()
        {
            Instance = this;
        }

        public bool IsClosestDistance(Vector3 point1, Vector3 point2)
        {
            float currentDistance = Vector3.Distance(point1, point2);
            if (currentDistance < closestDistance)
            {
                closestDistance = currentDistance;
            }

            if (currentDistance > previousDistance && Mathf.Approximately(previousDistance, closestDistance))
            {
                closestDistance = previousDistance;
                return true;
            }

            previousDistance = currentDistance;

            return false;
        }
    }
}