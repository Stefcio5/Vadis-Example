using Interactable;
using UnityEngine;

public class TestEnemy : MonoBehaviour, IInteractable
{

    public void Interact(Transform interactorTransform)
    {
        GetComponent<LootDropper>().DropLoot();
    }

    public string GetInteractTask()
    {
        return "Drop Loot";
    }

    public string GetInteractName()
    {
        return this.gameObject.name;
    }
}
