using UnityEngine;
using System.Collections;

abstract public class Layer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public abstract Vector3 calcPosition( int xIndex, int yIndex );
	public abstract void parse( int[,] data );
}
