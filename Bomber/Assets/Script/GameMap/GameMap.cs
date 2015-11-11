using UnityEngine;

public class GameMap : MonoBehaviour {
	private ObjectLayer objLayer;
	private TileLayer tileLayer;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void initGameMap()
	{
		objLayer = transform.GetComponentInChildren<ObjectLayer> ();
		tileLayer = transform.GetComponentInChildren<TileLayer> ();
	}

	public void parseMap( int[,] tileData, int[,] blockData )
	{
		objLayer.parse (blockData);
		tileLayer.parse (tileData);
	}
}
