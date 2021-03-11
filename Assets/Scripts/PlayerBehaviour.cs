using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour{
    public static PlayerBehaviour _Main;
    Rigidbody2D _Rigid;
    [SerializeField] float _Speed = 2f;
    MeleeWeapon _Melee;
    ShootBehaviour[] Eyes;
    public bool _HaveControl = true;

    private void Awake() {
        _Main = this;
    }

    // Start is called before the first frame update
    void Start(){
        Eyes = GetComponentsInChildren<ShootBehaviour>();
        foreach (ShootBehaviour eye in Eyes) {
            eye._ParentName = name;
        }
        _Rigid = GetComponent<Rigidbody2D>();
        _Melee = GetComponentInChildren<MeleeWeapon>();
    }

    // Update is called once per frame
    void Update(){
        if (_HaveControl) {
            Vector2 dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            _Rigid.velocity = dir.normalized * _Speed;

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;
            dir = mousePos - transform.position;
            if (Input.GetButtonDown("Fire2")) {
                _Melee._Attack(dir);
            }
            if (Input.GetButtonDown("Fire1")) {
                foreach (ShootBehaviour item in Eyes) {
                    item._Shoot(dir);
                }
            }
        }
    }
}
