using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour{
    [SerializeField] int _MaxLife = 10;
    int _ActualLife;
    public List<Life> _MyList = null;
    // Start is called before the first frame update
    void Start(){
        _ActualLife = _MaxLife;
    }
    public bool _TakeDamage(int damage) {
        _ActualLife -= damage;
        if(_ActualLife <= 0) {
            _Die();
        }
        return true;
    }

    void _Die() {
        //rip
        if (_MyList != null) _MyList.Remove(this);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update(){
        
    }
}
