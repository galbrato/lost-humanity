using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour{
    [SerializeField] float _AttackRate = 1f;
    [SerializeField] int _Damage = 2;
    float _Couter;

    DamageZone _DamageZone = null;
    // Start is called before the first frame update
    void Start(){
        _DamageZone = transform.GetComponentInChildren<DamageZone>();
        if(_DamageZone == null) {
            Debug.LogError("by " + name + ": Não foi encontrado a damage zone como filho desse objeto");
        }
        _Couter = 0f;
    }

    // Update is called once per frame
    void Update(){
        _Couter += Time.deltaTime;

    }

    public bool _Attack(Vector2 dir) {
        if(_Couter > (1 / _AttackRate)) {
            _Couter = 0;
            transform.transform.right = dir;
            _DamageZone._DoDamage(_Damage);
            return true;
        }
        return false;
    }
}
