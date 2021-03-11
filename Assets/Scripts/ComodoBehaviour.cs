using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComodoBehaviour : MonoBehaviour {
    public bool _IsDiscovered = false;
    bool _IsOpened = false;
    List<Life> _Enemys;
    List<Life> _Allys;
    List<Door> _Portas;
    private void Awake() {
        
    }

    // Start is called before the first frame update
    void Start() {
        if (!_IsDiscovered) transform.Find("Darkness").GetComponent<SpriteRenderer>().enabled = true;

        _Enemys = new List<Life>();
        _Allys = new List<Life>();

        //achando os mosntros dentro do comodo
        Vector2 Size = 2 * Camera.main.orthographicSize * Vector2.one;
        Collider2D[] monsters = Physics2D.OverlapBoxAll(transform.position, Size, 0f);
        foreach (var item in monsters) {
            if (item.CompareTag("Player")) {
                Debug.Log(name + " achei " + item.name);

                _Allys.Add(item.GetComponent<Life>());
                item.gameObject.SetActive(false);
            } else if (item.CompareTag("Enemy")) {

                Life l = item.GetComponent<Life>();
                _Enemys.Add(l);
                l._MyList = _Enemys;
                item.gameObject.SetActive(false);
            }
        }

        _Portas = new List<Door>(GetComponentsInChildren<Door>(false));
    }

    // Update is called once per frame
    void Update() {
        if (_IsClear()) {
            _OpenDoors();
        }
    }

    bool _IsClear() {
        return _Enemys.Count <= 0;
    }

    void _OpenDoors() {
        if (_IsOpened) return;
        _IsOpened = true;
        foreach (var item in _Portas) {
            if(item.enabled)item._OpenDoor();
        }
    }

    public void _FirstEnter() {
        if (_IsDiscovered) return;
        _IsDiscovered = true;

        transform.Find("Darkness").GetComponent<SpriteRenderer>().enabled = false;
        if (_IsClear()) {
            return;
        }
        foreach (var item in _Portas) {
            if(item.enabled)item._CloseDoor();
        }

        foreach (Life item in _Enemys) {
            item.gameObject.SetActive(true);
        }
        foreach (Life item in _Allys) {
            item.gameObject.SetActive(true);
        }

    }
}
