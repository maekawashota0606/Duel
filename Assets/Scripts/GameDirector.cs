using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    [SerializeField, Header("ReadOnly")]
    private gameState _gameState = gameState.waiting;
    private delegate void OnUpdate();
    private OnUpdate _onUpdate = null;
    [SerializeField]
    private GameObject _playersRoot = null;
    [SerializeField]
    private GameObject _spinersRoot = null;
    private List<Player> _players = new List<Player>(8);
    private List<Spiner> _spiners = new List<Spiner>(8);

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
        AddPlayersToList();
        GenerateSpiners();
        AddSpinersToList();
        _onUpdate = OnWaiting;
    }

    private void GenerateSpiners()
    {
        // ルートオブジェクトからプレイヤーを取得
        foreach (Player player in _players)
        {
            // コマを生成
            GameObject spiner = Instantiate(SpinersLoader.LoadSpiner(player.GetCharaNum()));

            spiner.transform.parent = _spinersRoot.transform;
            // プレイヤーに自身のコマを持たせる
            player.SetSpiner(spiner.GetComponent<Spiner>());
        }
    }

    private void AddPlayersToList()
    {
        _players.Clear();
        foreach (Player player in _playersRoot.GetComponentsInChildren<Player>())
            _players.Add(player);
    }

    private void AddSpinersToList()
    {
        _spiners.Clear();
        foreach (Player player in _players)
            _spiners.Add(player.GetSpiner());
    }
        
    private void OnWaiting()
    {
        _gameState = gameState.waiting;

        bool isReadyAll = true;
        foreach(Player player in _players)
        {
            // 各プレイヤーのフリック入力を待つ
            if (!player.GetIsReady())
            {
                isReadyAll = false;
                player.SetIsReady(player.Flick());
            }

#if UNITY_EDITOR
            // 1P以外は無視
            break;
#endif
        }

        // 全員の準備ができたら開始
        if (isReadyAll)
            _onUpdate = Onfighting;
    }

    private void Onfighting()
    {
        _gameState = gameState.fighting;
        foreach(Player player in _players)
        {
            player.MyUpdate();
        }

        foreach(Spiner spiner in _spiners)
        {
            spiner.MyUpdate();
        }
    }

    private void OnEnded()
    {
        _gameState = gameState.ended;
    }
}
