using UnityEngine;

public class StoveCounterSound: MonoBehaviour
{

    [SerializeField] private StoveCounter _stoveCounter;


    private AudioSource _audioSource;
    private float _warningSoundTimer;
    private bool playWarningSound;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
        _stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventsArgs e)
    {
        const float burnShowProgressAmount = .5f;
        playWarningSound = _stoveCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool playSound = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;

        if (playSound) _audioSource.Play();
        else _audioSource.Pause();
    }


    private void Update()
    {
        if (playWarningSound)
        {
            _warningSoundTimer -= Time.deltaTime; 
            if (_warningSoundTimer <= 0f) 
            { 
                float warningSoundTimerMax = .2f; 
                _warningSoundTimer = warningSoundTimerMax;
                
                SoundManager.Instance.PlayWarningSound(_stoveCounter.transform.position);
            }
        }
    }
}