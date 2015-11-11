using UnityEngine;
using System.Collections;

public class ObjectLayer : Layer {

	private const int NO_BLOCK = -2;
	private const int ENTRY_POINT = -1;

	public GameObject[] blockPrefabs;
	public GameObject[] enemyPrefabs;
	public GameObject playerPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override Vector3 calcPosition( int xIndex, int yIndex )
	{
		return new Vector3 (xIndex * GameMapConst.BLOCK_SIZE, yIndex * GameMapConst.BLOCK_SIZE);
	}

	public override void parse( int[,] data )
	{

		int i, j;
		
		for (i = 0; i < GameMapConst.MAP_INDEX_SIZE; ++ i) {
			for (j = 0; j < GameMapConst.MAP_INDEX_SIZE; ++ j) {
				if (data [i, j] == ENTRY_POINT) {
					Instantiate (playerPrefab,
					             calcPosition (i, j),
					             Quaternion.identity);
				} else if (data [i, j] != NO_BLOCK) {
					Instantiate (blockPrefabs [data [i, j]],
					             calcPosition (i, j),
					             Quaternion.identity);
				}
			}
		}
	}
}
