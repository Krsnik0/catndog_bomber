using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private GameMap map;
	// Use this for initialization
	void Start () {

		map = transform.GetComponentInChildren<GameMap>();
		map.initGameMap ();

		map.parseMap(
			new int[,] {
			{-1, -1, -1, -1, -1},
			{-1, 0, -1, -1, -1},
			{-1, -1, -1, -1, -1},
			{-1, -1, -1, -1, -1},
			{-1, -1, -1, -1, -1}
		},
		new int[,] {
			{0, 0, 0, 0, 0},
			{0, -1, -2, -2, 0},
			{0, -2, 0, -2, 0},
			{0, -2, -2, -2, 0},
			{0, 0, 0, 0, 0}
		}
		);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
