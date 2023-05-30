using UnityEngine;
using UnityEngine.UI;

public class GamePlayingClockUI : MonoBehaviour
{
    [SerializeField] private Image _timeImage;

    private void Update()
    {
        _timeImage.fillAmount = GameManager.Instance.GetGamePlayingTimerNormalized();
    }
}
