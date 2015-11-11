using UnityEngine;
using System.Collections;

public class TileLayer : Layer {

	private const int NO_TILE = -1;

	public GameObject[] tilePrefabs;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override Vector3 calcPosition( int xIndex, int yIndex )
	{
		return new Vector3 (xIndex * GameMapConst.BLOCK_SIZE, yIndex * GameMapConst.BLOCK_SIZE - GameMapConst.BLOCK_HEIGHT);
	}

	public override void parse( int[,] data )
	{
		
		int i, j;
		
		for (i = 0; i < GameMapConst.MAP_INDEX_SIZE; ++ i) {
			for (j = 0; j < GameMapConst.MAP_INDEX_SIZE; ++ j) {
				if (data [i, j] != NO_TILE) {
					Instantiate (tilePrefabs [data [i, j]],
					             calcPosition (i, j),
					             Quaternion.identity);
				}
			}
		}
	}
}
