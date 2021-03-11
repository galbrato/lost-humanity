using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarEnemy : MonoBehaviour{
    Rigidbody2D _Rigid;

    [SerializeField] string _TargetTag;
    Transform _Target = null;
    [SerializeField] float _Speed = 3f;
    [SerializeField] float _KeepDistance = 0.5f;

    Vector3 _Destiantion;
    float _Counter;

    ShootBehaviour _Eye;
    // Start is called before the first frame update
    void Start() {
        _Counter = 0;
        _Rigid = GetComponent<Rigidbody2D>();
        _Eye = GetComponentInChildren<ShootBehaviour>();
        _Eye._ParentName = name;

    }

    // Update is called once per frame
    void Update() {
        
        _Counter += Time.deltaTime;
        if (_Target != null) {
            Vector2 dir = (Vector2)(_Target.position - transform.position);
            _Attack(dir);
            if (dir.magnitude < _KeepDistance && _Rigid.velocity.magnitude == 0) {
                _ChangeDestiantion();
            } else {
                Vector2 dest = _Destiantion - transform.position;
                if (dest.magnitude < 0.1) {
                    _Rigid.velocity = Vector2.zero;
                } else {
                    _Rigid.velocity = dest.normalized * _Speed;
                }
            }
        } else {
            _FindTarget();
        }
    }

    void _ChangeDestiantion() {
        float maxVertical = Camera.main.transform.position.y + Camera.main.orthographicSize;
        float minVertical = Camera.main.transform.position.y - Camera.main.orthographicSize;
        float maxHorizontal = Camera.main.transform.position.x + (Camera.main.orthographicSize * Screen.width / Screen.height);
        float minHorizontal = Camera.main.transform.position.x - (Camera.main.orthographicSize * Screen.width / Screen.height); ;
        //Debug.Log("maxVertical " +  maxVertical + " / maxHorizontal " + maxHorizontal);
        _Destiantion.x = Random.Range(maxHorizontal, minHorizontal);
        _Destiantion.y = Random.Range(maxVertical, minVertical);
    }

    private bool _FindTarget() {

        GameObject[] objs = GameObject.FindGameObjectsWithTag(_TargetTag);
        if(objs.Length>0) _Target = objs[Random.Range(0,objs.Length)].transform;
        if (_Target == null) return false;
        return true;
    }

    void _Attack(Vector2 dir) {
        //Debug.Log("Atacando " + _Target.name);
        _Eye._Shoot(dir);
    }
}
