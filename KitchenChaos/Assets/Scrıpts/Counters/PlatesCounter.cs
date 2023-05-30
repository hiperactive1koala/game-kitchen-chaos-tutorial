
using System;
using UnityEngine;

public class PlatesCounter : BaseCounter
{

    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;
    
    
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
    private float _spawnPlateTimer;
    private float _spawnPlateTimerMax = 4f;
    private int _platesSpawnAmount;
    private int _platesSpawnAmountMax = 4;


    private void Update()
    {
        _spawnPlateTimer += Time.deltaTime;
        if (GameManager.Instance.IsGamePlaying() || _spawnPlateTimer > _spawnPlateTimerMax)
        {
            _spawnPlateTimer = 0f;

            if (_platesSpawnAmount< _platesSpawnAmountMax)
            {
                _platesSpawnAmount++;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            //Player hand is empty
            if (_platesSpawnAmount> 0)
            {
                //There at least one plate
                _platesSpawnAmount--;

                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
