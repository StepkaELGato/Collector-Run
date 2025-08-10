using UnityEngine;
using System;

public class CollectibleItem : MonoBehaviour
{
    public Action OnCollected;
    public virtual void Collect(GameObject collector)
    {
        Debug.Log($"{gameObject.name} was collected by {collector.name}");
        OnCollected?.Invoke();
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collect(other.gameObject);
        }
    }
}
