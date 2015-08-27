using UnityEngine;
using System.Collections;

public class ExplosionTrigger : MonoBehaviour
{

    public float Damage;
    // Use this for initialization
    void Start()
    {
        if (Damage == 0)
        {
            Damage = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        GameObject OtherGameObject = other.gameObject;
        if (!(OtherGameObject.tag == "Player"))
        {
            if (OtherGameObject.tag == "Enemy")
            {
                OtherGameObject.GetComponent<Agent>().HP -= Damage;
            }
            else if (OtherGameObject.tag == "SpawnPoint")
            {
                OtherGameObject.GetComponent<EnemySpawner>().spawnHealth -= Damage;
            }
        }
    }
}