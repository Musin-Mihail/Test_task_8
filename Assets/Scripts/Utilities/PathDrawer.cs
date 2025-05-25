using System.Collections.Generic;
using Managers;
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
            _lineRenderer = gameObject.AddComponent<LineRenderer>();
            _lineRenderer.startWidth = 0.1f;
            _lineRenderer.endWidth = 0.1f;
            _lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            _lineRenderer.startColor = Color.blue;
            _lineRenderer.endColor = Color.cyan;

            if (GameManager.Instance)
            {
                _lineRenderer.enabled = GameManager.Instance.DrawDronePath;
            }
            else
            {
                Debug.LogWarning("GameManager.Instance is null. Состояние PathDrawer не установлено из настроек.");
            }
        }

        private void Update()
        {
            if (_lineRenderer.enabled)
            {
                AddPointToPath(transform.position);
            }
            else if (_pathPoints.Count > 0)
            {
                _pathPoints.Clear();
                _lineRenderer.positionCount = 0;
            }
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

        public void SetDrawingActive(bool isActive)
        {
            _lineRenderer.enabled = isActive;
            if (!isActive)
            {
                _pathPoints.Clear();
                _lineRenderer.positionCount = 0;
            }
        }
    }
}