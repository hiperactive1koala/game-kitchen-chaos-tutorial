
using UnityEngine;

[CreateAssetMenu()]
public class BurningRecipeSO : ScriptableObject
{
   public KitchenObjectSO _input;
   public KitchenObjectSO _output;
   public float _burningTimerMax;
}
