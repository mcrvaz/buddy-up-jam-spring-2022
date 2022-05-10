using UnityEngine;

public class PlayerSounds
{
    const float FOOTSTEPS_INTERVAL = 0.75f;

    readonly Player player;
    readonly AudioManager audioManager;
    readonly AudioSource audioSource;
    readonly AudioSource footstepsAudioSource;

    float nextFootstepTime = float.MinValue;

    public PlayerSounds (
        Player player,
        AudioManager audioManager,
        AudioSource audioSource,
        AudioSource footstepsAudioSource
    )
    {
        this.player = player;
        this.audioManager = audioManager;
        this.audioSource = audioSource;
        this.footstepsAudioSource = footstepsAudioSource;

        player.OnHit += HandleHit;
        player.OnDeath += HandleDeath;
        player.Movement.OnGroundedStatusChanged += HandleGroundedStatusChanged;
    }

    public void Update ()
    {
        TryPlayFootsteps();
    }

    void TryPlayFootsteps ()
    {
        if (!player.Movement.IsGrounded)
            return;
        if (!player.Movement.IsMoving)
            return;
        if (Time.time < nextFootstepTime)
            return;
        PlayFootsteps();
    }

    void PlayFootsteps ()
    {
        audioManager.PlayFootsteps(footstepsAudioSource);
        nextFootstepTime = Time.time + FOOTSTEPS_INTERVAL;
    }

    void HandleGroundedStatusChanged (bool grounded)
    {
        if (!grounded)
            return;
        audioManager.PlayFootsteps(footstepsAudioSource);
        nextFootstepTime = Time.time + FOOTSTEPS_INTERVAL;
    }

    void HandleHit ()
    {
        audioManager.PlayPlayerHit(audioSource);
    }

    void HandleDeath ()
    {
        audioManager.PlayPlayerDeath(audioSource);
    }
}