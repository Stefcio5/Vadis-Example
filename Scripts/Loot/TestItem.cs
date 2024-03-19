using Interactable;
using UnityEngine;

public class TestItem : MonoBehaviour, IInteractable
{
    public void Interact(Transform interactorTransform)
    {
        ObjectPoolManager.instance.DespawnGameObject(gameObject);
    }

    public string GetInteractTask()
    {
        return "Podnieś";
    }

    public string GetInteractName()
    {
        return $"Pick up {this.gameObject.name}";
    }
}
