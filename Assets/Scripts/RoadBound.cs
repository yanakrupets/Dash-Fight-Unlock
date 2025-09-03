using System;
using Interfaces;
using UnityEngine;
using Zenject;

public class RoadBound : MonoBehaviour
{
    public event Action<Obstacle> OnTrigger;
    
    public void SetPosition(Vector3 position)
    {
        transform.localPosition = position;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Consts.Obstacle) && other.TryGetComponent<Obstacle>(out var obstacle))
        {
            OnTrigger?.Invoke(obstacle);
        }
    }
}
