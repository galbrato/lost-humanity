using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBehaviour : MonoBehaviour{
    [SerializeField] GameObject _BulletPrefab;
    [SerializeField] float _BulletSpeed = 3f;
    [SerializeField] float _FireRate = 2f;
    public string _ParentName;
    float _Couter;
    // Start is called before the first frame update
    void Start(){
        
        _Couter = 0;
    }

    // Update is called once per frame
    void Update(){
        _Couter += Time.deltaTime;
    }

    public bool _Shoot(Vector2 dir) {
        if (_Couter > (1 / _FireRate)) {
            _Couter = 0f;
            transform.transform.right = dir;
            GameObject bullet = Instantiate<GameObject>(_BulletPrefab, transform.position, transform.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.right * _BulletSpeed;
            bullet.name = _ParentName;
            return true;
        }
        return false;
    }
}
