using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
  [SerializeField] private Button _playButton;
  [SerializeField] private Button _quitButton;


  private void Awake()
  {
    _playButton.onClick.AddListener(() =>
    {
      Loader.Load(Loader.Scene.GameScene);
    });
    _quitButton.onClick.AddListener(() =>
    {
      Application.Quit();
    });

    Time.timeScale = 1f;
  }
}
