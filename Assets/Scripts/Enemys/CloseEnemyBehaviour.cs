using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseEnemyBehaviour : MonoBehaviour{
    Rigidbody2D _Rigid;

    [SerializeField] string _TargetTag;
    Transform _Target = null;
    [SerializeField] float _Speed = 3f;
    [SerializeField] float _StopingDistance = 0.5f;


    float _Counter;

    MeleeWeapon _Melee;
    // Start is called before the first frame update
    void Start(){
        _Counter = 0;
        _Rigid = GetComponent<Rigidbody2D>();
        _Melee = GetComponentInChildren<MeleeWeapon>();
    }

    // Update is called once per frame
    void Update(){

        _Counter += Time.deltaTime;
        if(_Target != null) {
            Vector2 dir = (Vector2)(_Target.position - transform.position);
            if(dir.magnitude > _StopingDistance) {
                _Rigid.velocity = dir.normalized * _Speed;
            } else {
                _Rigid.velocity = Vector2.zero;
                _Attack(dir);
            }
        } else {
            _FindTarget();
        }
    }

    private bool _FindTarget() {

        GameObject[] objs = GameObject.FindGameObjectsWithTag(_TargetTag);
        if (objs.Length > 0) _Target = objs[Random.Range(0, objs.Length)].transform;
        if (_Target == null) return false;
        return true;
    }

    void _Attack(Vector2 dir) {
        //Debug.Log("Atacando " + _Target.name);
        _Melee._Attack(dir);
    }
}
