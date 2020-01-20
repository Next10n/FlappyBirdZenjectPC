using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class CollumnsController : MonoBehaviour
{
    [Inject]
    [SerializeField]
    protected float _speed;
    [Inject]
    [SerializeField]
    protected float _leftBorderColumn;
    [Inject]
    [SerializeField]
    protected float _startWallPos;

    [Inject]
    private GameConfig _gameConfig;

    [Inject]
    private SignalManager _signalManager;

    private float _backOnResume = 1f;

    private void Awake()
    {
        _signalManager._signalBus.Subscribe<ResumeSignal>(OnResumeGame);
    }

    private void Update()
    {
        if(transform.position.x >= _leftBorderColumn)
        {
            transform.Translate(Vector3.left * Time.deltaTime * _speed);
        }
        else
        {
            MoveToStart();
        }
        
    }

    private void Start()
    {
        SetPassWidth();
    }

    private void SetPassWidth()
    {
        Vector3 localPos = transform.GetChild(0).localPosition;
        localPos.y += _gameConfig.PassWidth / 2;
        transform.GetChild(0).localPosition = localPos;
        localPos = transform.GetChild(1).localPosition;
        localPos.y -= _gameConfig.PassWidth / 2;
        transform.GetChild(1).localPosition = localPos;
        Vector2 size = transform.GetComponent<BoxCollider2D>().size;
        size.y = _gameConfig.PassWidth;
        transform.GetComponent<BoxCollider2D>().size = size;
    }



    private void MoveToStart()
    {
        transform.position = new Vector3(_startWallPos, Random.Range(_gameConfig.MinPassHieght, _gameConfig.MaxPassHieght), transform.position.z );
    }



    public class Factory : PlaceholderFactory<float, float, float, CollumnsController>
    {

    }

    private void OnResumeGame()
    {
        transform.position = new Vector3(transform.position.x + _backOnResume, transform.position.y, transform.position.z);
    }

    private void OnDestroy()
    {
        _signalManager._signalBus.TryUnsubscribe<ResumeSignal>(OnResumeGame);
    }

}
