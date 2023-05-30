using UnityEngine;

public class ClearCounter : BaseCounter
{
    
    // ReSharper disable once InconsistentNaming
    [SerializeField] private KitchenObjectSO _kitchenObjectSO;
    
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //There is no KitchenObject here
            if (player.HasKitchenObject())
            {
                //Player is carrying something
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                //Player not carrying anything
            }
        }
        else
        {
            //There is a KitchenObject here
            if (player.HasKitchenObject())
            {
                //Player is carrying something
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    // Player is holding a Plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        GetKitchenObject().DestroySelf();
                    }
                }
                else
                {
                    //Player is not carrying Plate but something else
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        //Counter is holding Plate

                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {  
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            else
            {
                //Player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

}
