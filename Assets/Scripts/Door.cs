using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour{
    SpriteRenderer _Dark = null;
    public bool _Open = true;
    BoxCollider2D _mColl = null;
    Transform _Destiny = null;

    ComodoBehaviour _NextComodo = null;
    // Start is called before the first frame update
    void Start(){
        _Dark = transform.Find("PortaDark").GetComponent<SpriteRenderer>();
        if(_Dark == null) {
            Debug.LogError("ERRO! esta faltando um objeto chamado PortaDark numa porta");
        }
        _mColl = GetComponent<BoxCollider2D>();

        _Destiny = transform.Find("Destiny");
        if (_Destiny == null) {
            Debug.LogError("ERRO! esta faltando um objeto chamado Destiny numa porta");
        }
        ComodoBehaviour[] comodos = FindObjectsOfType<ComodoBehaviour>();
        foreach (ComodoBehaviour comodo in comodos) {
            if(Vector3.Distance(comodo.transform.position, _Destiny.position) < Camera.main.orthographicSize) {
                _NextComodo = comodo;
                break;
            }
        }
        if (_NextComodo == null) {
            _mColl.isTrigger = false;
            SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer sprite in sprites) {
                sprite.enabled = false;
            }
            this.enabled = false;
        } 
    }

    // Update is called once per frame
    void Update(){
      
       
    }

    public void _OpenDoor() {
        _Dark.enabled = true;
        _Open = true;
        _mColl.isTrigger = true;
    }

    public void _CloseDoor() {
        _Dark.enabled = false;
        _Open = false;
        _mColl.isTrigger = false;

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (_Open && collision.CompareTag("Player")) {
            collision.transform.position = _Destiny.position;
            _NextComodo._FirstEnter();
        }
    }
}
