
using UnityEngine;

[CreateAssetMenu()]
public class CuttingRecipeSO : ScriptableObject
{
   public KitchenObjectSO _input;
   public KitchenObjectSO _output;
   public int cuttingPorgressMax;
}
