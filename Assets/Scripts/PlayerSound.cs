using UnityEngine;


public class PlayerSound : MonoBehaviour
{
    public float volume = 1f;

    private PlayerController player;
    private float footstepTimer = 0f;
    private float footstepTimerMax = 0.3f;

    private void Awake()
    {
        player = GetComponent<PlayerController>();
    }

    private void Update()
    {
        footstepTimer -= Time.deltaTime;
        if (player.IsWalking() && player.IsGrounded)
        {
            if (footstepTimer < 0)
            {
                footstepTimer = footstepTimerMax;
                AudioManager.Instance.PlayerFootStepSound(player.transform.position, volume);
            }
        }
    }
}