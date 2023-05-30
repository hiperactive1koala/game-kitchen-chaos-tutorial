using System;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{

    [SerializeField] private Transform _container;
    [SerializeField] private Transform _recipeTemplate;


    private void Awake() { _recipeTemplate.gameObject.SetActive(false); }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawn += Delivery_OnRecipeSpawn;
        DeliveryManager.Instance.OnRecipeCompleted += Delivery_OnRecipeCompleted;
        
        UpdateVisual();
    }

    private void Delivery_OnRecipeCompleted(object sender, EventArgs e) { UpdateVisual(); }

    private void Delivery_OnRecipeSpawn(object sender, EventArgs e) { UpdateVisual(); }

    private void UpdateVisual()
    {
        foreach (Transform child in _container)
        {
            if (child == _recipeTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList())
        {
            Transform recipeTransform = Instantiate(_recipeTemplate, _container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSO);
        }

    }
}
