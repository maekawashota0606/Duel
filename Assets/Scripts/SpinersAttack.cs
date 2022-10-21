using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SpinersAttack : MonoBehaviour
{
    [SerializeField]
    private float _range = 120;
    [SerializeField]
    private int _power = 35;
    [SerializeField]
    private float _duration = 1.0f;
    private float _time = 0;
    //private List<int> _hitPlayersNum = new List<int>(8);

    public IEnumerator Attack(Spiner spiner, UnityAction callBack)
    {
        transform.localPosition = spiner.GetDirection() * _range;
        // TODO:UŒ‚‚ÌŒü‚«‚ğ’²®


        _time += Time.deltaTime;
        if (_time < _duration)
            yield return null;

        _time = 0;
        callBack();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if(collision.gameObject.CompareTag("Spiner"))
            Debug.Log(collision.gameObject.name);
    }
}
