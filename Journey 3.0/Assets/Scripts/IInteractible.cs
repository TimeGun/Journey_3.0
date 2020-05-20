using UnityEngine;

public interface IInteractible
{
    GameObject getGameObject();

    bool isActive();

    void StartInteraction(Transform parent);
    

    void StopInteraction();
    
}
