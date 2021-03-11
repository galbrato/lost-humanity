using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour{
    [SerializeField] string _TargetTag = "Enemy";
    float _Duration = 0.1f;
    float _Counter = 0f;
    SpriteRenderer _Sprite;

    [SerializeField] float _ForceBackDist = 1f;
    [SerializeField] SpriteRenderer _DeactivateThis;
    private void Start() {
        _Sprite = GetComponentInChildren<SpriteRenderer>(true);
        if(_Sprite == null) {
            Debug.LogError("Não achei o Sprite nso filhos");
        }
    }

    // Update is called once per frame
    void Update(){
        _DeactivateThis.enabled = false;
        _Counter += Time.deltaTime;
        if (_Counter > _Duration) {
            _DeactivateThis.enabled = true;
            _Sprite.enabled = false;
        }
    }

    public void _DoDamage(int damage) {
        _Sprite.enabled = true;
        _Counter = 0;
        Collider2D[]colls =  Physics2D.OverlapCircleAll(transform.position,0.5f);
        foreach (Collider2D coll in colls) {  
            if (coll.CompareTag(_TargetTag)) {
                //Aplicar dano ao inimigo
                coll.gameObject.GetComponent<Life>()._TakeDamage(damage);
                //Debug.Log("Eu " + name + " causei dano ao " + coll.name);
                Vector2 dir = coll.transform.position - transform.position;
                Vector3 aux = coll.transform.position + ((Vector3)dir.normalized * _ForceBackDist);
                coll.transform.position = aux;
            }
        }
    }
}
