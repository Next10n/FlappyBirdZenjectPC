using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    [Inject]
    private SignalManager _signalManager;
    [Inject]
    private GameConfig _gameConfig;
    [Inject]
    private ScoreController _scoreController;

    [Inject]
    private SoundController _soundController;

    [Inject]
    private TimeController _timeController;

    [SerializeField]
    private AudioSource _jumpSound;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    protected float _gravityScale;

    private bool _canJump = true;

    private static float _jumpCooldown = 0.3f;

    private void Awake()
    {
        _scoreController.SetScore(0);
        SetGravity(_gameConfig.GravityScale);
    }

    private void Update()
    {
        Jump();
    }



    public void AddForce()
    {
        Rigidbody2D rg = gameObject.GetComponent<Rigidbody2D>();
        rg.velocity = Vector2.zero;
        rg.AddForce(Vector2.up * _gameConfig.PlayerForce);
    }


    private void Jump()
    {
        if(!_timeController.IsPaused())
            if (_canJump)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    PlayJumpSound();
                    AddForce();
                    SetJumpFalse();
                    Invoke("SetJumpTrue", _jumpCooldown);
                }
                else if (Input.touchCount != 0)
                {
                    if(Input.touches[0].position.y < 500)
                    {
                        PlayJumpSound();
                        AddForce();
                        SetJumpFalse();
                        Invoke("SetJumpTrue", _jumpCooldown);
                    }
   

                }

            }

    }

    private void PlayJumpSound()
    {
        if(_soundController.SoundCheck())
            _jumpSound.Play();
    }


    public void SetGravity(float gravity)
    {
        _gravityScale = gravity;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = gravity;
    }



    public class PlayerFabrik : PlaceholderFactory<PlayerController>
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pass")
        {
            _signalManager._signalBus.TryFire<ScoreSignal>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _animator.SetTrigger("Dead");
        _signalManager._signalBus.TryFire<GameEndSignal>();
    }

    private void SetJumpFalse()
    {
        _animator.SetBool("IsFlying", true);
        _canJump = false;
    }

    private void SetJumpTrue()
    {
        _animator.SetBool("IsFlying", false);
        _canJump = true;
    }


}
