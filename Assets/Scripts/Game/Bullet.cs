using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    
    public float lifeTime = 3f;

    private void OnEnable() {
        Invoke(nameof(DestroySelf), lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
    }

    public void DestroySelf() {
        GameObject.Destroy(gameObject);
    }
}
