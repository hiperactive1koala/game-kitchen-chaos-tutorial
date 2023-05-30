using UnityEngine;
// ReSharper disable Unity.PreferAddressByIdToGraphicsParams

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";

    [SerializeField] private Player _player;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetBool(IS_WALKING,_player.IsWalking());
    }
}
