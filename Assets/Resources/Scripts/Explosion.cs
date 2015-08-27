using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	// Use this for initialization
    public ParticleSystem ps;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if(ps.isStopped)
    {
        Destroy(gameObject);
    }
	}
}
