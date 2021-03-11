using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour{
    [SerializeField] string TargetTag;
    [SerializeField] int _Damage = 1;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag(TargetTag)) {
            collision.gameObject.GetComponent<Life>()._TakeDamage(_Damage);
        }
        if (collision.name != name) {
            Destroy(gameObject);
        }
    }
}
