using UnityEngine;
using UnityEngine.UI;

public class PlateIconsSingleUI: MonoBehaviour
{
    [SerializeField] private Image _image;
    public void SetKitchenObjectSO(KitchenObjectSO kitchenObject)
    {
        _image.sprite = kitchenObject._sprite;
    }
}