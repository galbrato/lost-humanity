using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMan : MonoBehaviour{
    [SerializeField] float _Speed = 5;
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        Vector3 p = PlayerBehaviour._Main.transform.position;
        float dist = 2 * (Camera.main.orthographicSize + 0.5f);
        Vector3 dest = new Vector3(
            Mathf.Round(p.x /dist) *dist, 
            Mathf.Round(p.y /dist) *dist, 
            Camera.main.transform.position.z);
        transform.position = Vector3.Lerp(transform.position, dest, Time.deltaTime*_Speed);
    }
}
