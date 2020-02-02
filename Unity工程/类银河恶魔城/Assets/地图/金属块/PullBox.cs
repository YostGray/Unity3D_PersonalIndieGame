using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullBox : MonoBehaviour , ICouldBePull
{
    void ICouldBePull.BeGraped(GameObject gameObject)
    {
        this.gameObject.AddComponent<FixedJoint2D>().connectedBody = gameObject.GetComponent<Rigidbody2D>();
        this.gameObject.layer = 12;
    }

    void ICouldBePull.BeReleased()
    {
        Destroy(gameObject.GetComponent<FixedJoint2D>());
        gameObject.layer = 15;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

public interface ICouldBePull
{
    void BeGraped(GameObject gameObject);
    void BeReleased();
}
