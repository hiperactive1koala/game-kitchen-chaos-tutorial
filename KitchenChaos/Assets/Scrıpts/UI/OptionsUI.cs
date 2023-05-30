using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    
    public static OptionsUI Instance { get; private set; }
    
    
    [SerializeField] private Button _soundEffectsButton;
    [SerializeField] private Button _musicButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _moveUpButton;
    [SerializeField] private Button _moveDownButton;
    [SerializeField] private Button _moveLeftButton;
    [SerializeField] private Button _moveRightButton;
    [SerializeField] private Button _interactButton;
    [SerializeField] private Button _interactAlternateButton;
    [SerializeField] private Button _pauseButton;   
    [SerializeField] private Button _gamepadInteractButton;
    [SerializeField] private Button _gamepadInteractAlternateButton;
    [SerializeField] private Button _gamepadPauseButton;
    [SerializeField] private TextMeshProUGUI _soundEffectsText;
    [SerializeField] private TextMeshProUGUI _musicText;
    [SerializeField] private TextMeshProUGUI _moveUpText;
    [SerializeField] private TextMeshProUGUI _moveDownText;
    [SerializeField] private TextMeshProUGUI _moveLeftText;
    [SerializeField] private TextMeshProUGUI _moveRightText;
    [SerializeField] private TextMeshProUGUI _interactText;
    [SerializeField] private TextMeshProUGUI _interactAlternateText;
    [SerializeField] private TextMeshProUGUI _pauseText;
    [SerializeField] private TextMeshProUGUI _gamepadInteractText;
    [SerializeField] private TextMeshProUGUI _gamepadInteractAlternateText;
    [SerializeField] private TextMeshProUGUI _gamepadPauseText;
    [SerializeField] private Transform _pressToRebindKeyTransform;


    private Action _onCloseButtonAction;

    private void Awake()
    {
        Instance = this;
        _soundEffectsButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        _musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        _closeButton.onClick.AddListener(() =>
        {
            Hide();
            _onCloseButtonAction();
        });

        

        _moveUpButton.onClick.AddListener((() => RebindBinding(GameInput.Binding.MoveUp)));
        _moveDownButton.onClick.AddListener((() => RebindBinding(GameInput.Binding.MoveDown)));
        _moveLeftButton.onClick.AddListener((() => RebindBinding(GameInput.Binding.MoveLeft)));
        _moveRightButton.onClick.AddListener((() => RebindBinding(GameInput.Binding.MoveRight)));
        _interactButton.onClick.AddListener((() => RebindBinding(GameInput.Binding.Interact)));
        _interactAlternateButton.onClick.AddListener((() => RebindBinding(GameInput.Binding.InteractAlternate)));
        _pauseButton.onClick.AddListener((() => RebindBinding(GameInput.Binding.Pause)));   
        _gamepadInteractButton.onClick.AddListener((() => RebindBinding(GameInput.Binding.GamePadInteract)));
        _gamepadInteractAlternateButton.onClick.AddListener((() => RebindBinding(GameInput.Binding.GamePadInteractAlternate)));
        _gamepadPauseButton.onClick.AddListener((() => RebindBinding(GameInput.Binding.GamePadPause)));
    }

    private void Start()
    {
        GameManager.Instance.OnGameUnpaused += (sender, args) => Hide();
        
        UpdateVisual();
        Hide();
        HidePressToRebindKey();
    }

    private void UpdateVisual()
    {
        _soundEffectsText.text ="Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10) ;
        _musicText.text ="Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10) ;

        _moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveUp);
        _moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveDown);
        _moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveLeft);
        _moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveRight);
        _interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        _interactAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        _pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause); 
        _gamepadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamePadInteract);
        _gamepadInteractAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamePadInteractAlternate);
        _gamepadPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamePadPause);

    }

    public void Show(Action onCloseButtonAction)
    {
        _onCloseButtonAction = onCloseButtonAction;
        
        gameObject.SetActive(true);
        
        _soundEffectsButton.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
    
    public void ShowPressToRebindKey()
    {
        _pressToRebindKeyTransform.gameObject.SetActive(true);
    }

    private void HidePressToRebindKey()
    {
        _pressToRebindKeyTransform.gameObject.SetActive(false);
    }


    private void RebindBinding(GameInput.Binding binding)
    {
        ShowPressToRebindKey();
        GameInput.Instance.RebindBinding(binding, () =>
        {
            HidePressToRebindKey();
            UpdateVisual();
        });
    }
}
