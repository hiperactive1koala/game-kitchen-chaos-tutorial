using System;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnAnyObjectTrash;
    
    new public static void ResetStaticData()
    {
        OnAnyObjectTrash = null;
    }
    
    
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObject().DestroySelf();
            OnAnyObjectTrash?.Invoke(this, EventArgs.Empty);
        }
    }
}
