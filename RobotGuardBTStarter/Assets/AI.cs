using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using Panda; //plugin panda//

//Não consegui comentar no script do Wander.BT//

public class AI : MonoBehaviour
{
    //Referencias//
    public Transform player;
    public Transform bulletSpawn;
    public Slider healthBar;   
    public GameObject bulletPrefab;
    NavMeshAgent agent;
    //Variaveis//
    public Vector3 destination; // The movement destination.
    public Vector3 target;      // The position to aim to.
    float health = 100.0f;
    float rotSpeed = 5.0f;
    float visibleRange = 80.0f;
    float shotRange = 40.0f;

    void Start()
    {
        //Atribuindo componentes e stopdistance//
        agent = this.GetComponent<NavMeshAgent>();
        agent.stoppingDistance = shotRange - 5; //for a little buffer
        //Regeneração de vida//
        InvokeRepeating("UpdateHealth",5,0.5f);
    }

    void Update()
    {
        //Barra de vida//
        Vector3 healthBarPos = Camera.main.WorldToScreenPoint(this.transform.position);
        healthBar.value = (int)health;
        healthBar.transform.position = healthBarPos + new Vector3(0,60,0);
    }
    //Sistema de vida + regeneração//
    void UpdateHealth()
    {
       if(health < 100)
        health ++;
    }
    //Sistema de dano//
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "bullet")
        {
            health -= 10;
        }
    }

    //Metodos do Wander.BT//
    [Task]
    //Pegando um ponto aleatório no mapa//
    public void PickRandomDestination()
    {
        Vector3 dest = new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100));
        agent.SetDestination(dest);
        Task.current.Succeed();
    }
    [Task]
    //Movendo-se para ele//
    public void MoveToDestination()
    {
        if (Task.isInspected)
            Task.current.debugInfo = string.Format("t={0:0.00}", Time.time);
        //Se o resto da distancia for menor que a distancia de parada, task completada com sucesso//
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            Task.current.Succeed();
        }
    }

}

