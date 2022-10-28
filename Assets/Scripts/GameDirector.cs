using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    private gameState _gameState = gameState.waiting;
    private delegate void OnUpdate();
    private OnUpdate _onUpdate = null;
    [SerializeField]
    private Player _player_1 = null;
    [SerializeField]
    private Player _player_2 = null;
    private Spiner _spiner_1 = null;
    private Spiner _spiner_2 = null;
    private bool _isReady_1P = false;
    private bool _isReady_2P = false;

    private enum gameState
    {
        waiting,
        fighting,
        ended,
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        _onUpdate();
    }

    private void Init()
    {
        _onUpdate = OnWaiting;

        _spiner_1 = _player_1.GetSpiner();
        _spiner_2 = _player_2.GetSpiner();
    }

    private void OnWaiting()
    {
        if(!_isReady_1P)
            _isReady_1P = _player_1.Flick();
#if UNITY_EDITOR
        _isReady_2P = true;
#else
        if (!_isReady_2P)
            _isReady_2P = _player_2.Flick();
#endif
        if (_isReady_1P && _isReady_2P)
            _onUpdate = Onfighting;
    }

    private void Onfighting()
    {
        _player_1.MyUpdate();
        _spiner_1.MyUpdate();
        _player_2.MyUpdate();
        _spiner_2.MyUpdate();
    }

    private void OnEnded()
    {

    }
}
