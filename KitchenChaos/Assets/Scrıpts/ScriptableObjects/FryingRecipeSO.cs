
using UnityEngine;

[CreateAssetMenu()]
public class FryingRecipeSO : ScriptableObject
{
   public KitchenObjectSO _input;
   public KitchenObjectSO _output;
   public float _fryingTimerMax;
}
