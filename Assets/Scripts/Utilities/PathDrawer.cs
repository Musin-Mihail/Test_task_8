using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class PathDrawer : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        private readonly List<Vector3> _pathPoints = new();
        private const float MinDistanceForPoint = 0.5f;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.startWidth = 0.1f;
            _lineRenderer.endWidth = 0.1f;
            _lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            _lineRenderer.startColor = Color.blue;
            _lineRenderer.endColor = Color.cyan;
        }

        private void Update()
        {
            AddPointToPath(transform.position);
        }

        /// <summary>
        /// Добавляет точку к пути, если она достаточно далеко от предыдущей.
        /// </summary>
        /// <param name="newPoint">Новая позиция для добавления.</param>
        private void AddPointToPath(Vector3 newPoint)
        {
            if (_pathPoints.Count == 0 || Vector3.Distance(_pathPoints[^1], newPoint) > MinDistanceForPoint)
            {
                _pathPoints.Add(newPoint);
                _lineRenderer.positionCount = _pathPoints.Count;
                _lineRenderer.SetPositions(_pathPoints.ToArray());
                if (_pathPoints.Count > 50)
                {
                    _pathPoints.RemoveAt(0);
                }
            }
        }
    }
}