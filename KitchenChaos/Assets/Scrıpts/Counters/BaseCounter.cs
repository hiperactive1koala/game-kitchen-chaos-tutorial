using System;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    public static event EventHandler OnAnyObjectPlacedHere;
    
    public static void ResetStaticData() { OnAnyObjectPlacedHere = null; }
    
    
    
    [SerializeField] private Transform _counterTopPoint;


    private KitchenObject _kitchenObject;

    public virtual void Interact(Player player) { Debug.LogError("BaseCounter.Interact();"); }
    
    public virtual void InteractAlternate(Player player) { }
    
    public Transform GetKitchenObjectFollowTransform() { return _counterTopPoint; }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this._kitchenObject = kitchenObject;
        if (kitchenObject != null) OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
    }

    public KitchenObject GetKitchenObject() { return _kitchenObject; }

    public void ClearKitchenObject() { _kitchenObject = null; }

    public bool HasKitchenObject() { return _kitchenObject != null; }
    
}
