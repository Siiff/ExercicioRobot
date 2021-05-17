using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drive : MonoBehaviour {
    //Script de movimentação de disparos do Personagem//
    //Variaveis//
	float speed = 20.0F;
    float rotationSpeed = 120.0F;
    //Referencias//
    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    void Update() {
        //Pegando os inputs e multiplicando por deltatime//
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);
        //Disparo//
        if(Input.GetKeyDown("space"))
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward*2000);
        }
    }
}
