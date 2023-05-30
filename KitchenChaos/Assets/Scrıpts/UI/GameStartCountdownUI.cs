using System;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI _countdownText;

   private const string NUMBER_POPUP = "NumberPopup";
   private Animator _animator;
   private int _previousCountdownNumber;

   private void Awake()
   {
      _animator = GetComponent<Animator>(); 
   }

   private void Start()
   {
      GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        
      Hide();
   }

   private void GameManager_OnStateChanged(object sender, EventArgs e)
   {
      if (GameManager.Instance.IsCountdownToStartActive()) Show();
      else Hide();
   }

   private void Update()
   {
      int countDownNumber = Mathf.CeilToInt(GameManager.Instance.GetCountdownToStartTimer());
      _countdownText.text = countDownNumber.ToString();

      if (_previousCountdownNumber != countDownNumber)
      {
         _previousCountdownNumber = countDownNumber;
         _animator.SetTrigger(NUMBER_POPUP); 
         SoundManager.Instance.PlayCountdownSound();
      }
   }

   private void Show()
   {
      gameObject.SetActive(true);
   }

   private void Hide()
   {
      gameObject.SetActive(false);
   }
}
