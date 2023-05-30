using UnityEngine;

public class StoveBurnFlashingBarUI : MonoBehaviour
{
    private const string IS_FLASHING = "IsFlashing";

    [SerializeField] private StoveCounter _stoveCounter;

    private Animator _animator;


    private void Awake() => _animator = GetComponent<Animator>();

    private void Start()
    {
        _stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
        
        _animator.SetBool(IS_FLASHING, false);
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventsArgs e)
    {
        const float burnShowProgressAmount = .5f;
        bool show = _stoveCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount;
        
        _animator.SetBool(IS_FLASHING, show);
    }
    
}
