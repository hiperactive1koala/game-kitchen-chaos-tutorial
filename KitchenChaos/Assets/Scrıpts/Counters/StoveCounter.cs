using System;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventsArgs> OnProgressChanged;

    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs: EventArgs
    {
        public State state;
    }
    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned
    }
    
    [SerializeField] private FryingRecipeSO[] _fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] _burningRecipesSOArray;

    private State _state;
    private float _fryingTimer;
    private FryingRecipeSO _fryingRecipe;
    private float _burningTimer;
    private BurningRecipeSO _burningRecipe;

    private void Start()
    {
        _state = State.Idle;
    }

    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (_state) 
            { 
                case State.Idle: 
                    break; 
                
                case State.Frying: 
                    _fryingTimer += Time.deltaTime;
                    
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventsArgs() { 
                        progressNormalized = _fryingTimer / _fryingRecipe._fryingTimerMax
                    });
                    
                    if (_fryingTimer> _fryingRecipe._fryingTimerMax) 
                    {
                        //Fried
                        
                        GetKitchenObject().DestroySelf();
                        
                        KitchenObject.SpawnKitchenObject(_fryingRecipe._output, this);

                        _burningRecipe = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                        _state = State.Fried;
                        _burningTimer = 0f;
                        
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs()
                        {
                            state = _state
                        });
                    }
                    break;
                
                case State.Fried:
                    _burningTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventsArgs()
                    { 
                        progressNormalized = _burningTimer / _burningRecipe._burningTimerMax
                    });
                    if (_burningTimer> _burningRecipe._burningTimerMax) 
                    {
                        //Burned
                        
                        GetKitchenObject().DestroySelf();
                        
                        KitchenObject.SpawnKitchenObject(_burningRecipe._output, this);

                        _state = State.Burned;
                        
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs()
                        {
                            state = _state
                        });
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventsArgs()
                        { 
                            progressNormalized = 0f
                        });
                    }
                    break;
                
                case State.Burned:
                    break;
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //There is no KitchenObject here
            if (player.HasKitchenObject())
            {
                //Player is carrying something
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    // Player carrying something that can Fried
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    
                    _fryingRecipe = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    _state = State.Frying;
                    _fryingTimer = 0f;
                    
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs()
                    {
                        state = _state
                    });
                    
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventsArgs()
                    { 
                        progressNormalized = _fryingTimer / _fryingRecipe._fryingTimerMax
                    });
                }
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
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                        _state = State.Idle;
                
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs()
                        {
                            state = _state
                        });
                
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventsArgs()
                        { 
                            progressNormalized = 0f
                        });
                    }
                }
            }
            else
            {
                //Player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);

                _state = State.Idle;
                
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs()
                {
                    state = _state
                });
                
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventsArgs()
                { 
                    progressNormalized = 0f
                });
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO cuttingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return cuttingRecipeSO!=null;
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipeSO in _fryingRecipeSOArray)
        {
            if (fryingRecipeSO._input==inputKitchenObjectSO) return fryingRecipeSO;
        }

        return null;
    }

    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BurningRecipeSO burningRecipeSO in _burningRecipesSOArray)
        {
            if (burningRecipeSO._input==inputKitchenObjectSO) return burningRecipeSO;
        }

        return null;
    }


    public bool IsFried()
    {
        return _state == State.Fried;
    }

}
