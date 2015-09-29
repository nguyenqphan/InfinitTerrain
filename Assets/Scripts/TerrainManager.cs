﻿using UnityEngine;
using System.Collections;

public class TerrainManager : MonoBehaviour {
	public Sprite[] Sprites;
	public int HorizontalTiles = 25;
	public int VerticalTiles = 25;
	public int Key = 1;
	public Transform Player;
	public float MaxDistanceFromCenter = 7;
	public RuntimeAnimatorController WaterAnimation;
	public int WaterTileIndex = -1;
	
	private SpriteRenderer[,] _renderers;
	
	public Sprite SelectRandomSprite(int x, int y, out bool isWater)
	{
		int index = RandomHelper.Range (x, y, Key, Sprites.Length);
		isWater = (WaterTileIndex == index);
		return Sprites [index];
	}
	void RedrawMap()
	{
		transform.position = new Vector3((int)Player.position.x, (int)Player.position.y, Player.position.z);
		for (int x = 0; x < HorizontalTiles; x++) {
			for (int y = 0; y < VerticalTiles; y++) {
				var spriteRenderer = _renderers[x,y];
				bool isWater = false;
				spriteRenderer.sprite = SelectRandomSprite(
					(int)transform.position.x + x,
					(int)transform.position.y + y,
					out isWater);
				var animator = spriteRenderer.gameObject.GetComponent<Animator>();
				
				if (isWater)
				{
					if (animator == null)
					{
						animator = spriteRenderer.gameObject.AddComponent<Animator>();
						animator.runtimeAnimatorController = WaterAnimation;
					}
				}
				else
				{
					if(animator != null)
					{
						GameObject.Destroy(animator);
					}
				}
			}
		}
	}
	
	// Use this for initialization
	void Start () {
		int sortIndex = 0;
		var offset = new Vector3 (0 - HorizontalTiles / 2, 0 - VerticalTiles / 2, 0);
		_renderers = new SpriteRenderer[HorizontalTiles,VerticalTiles];
		for (int x = 0; x < HorizontalTiles; x++) {
			for (int y = 0; y < VerticalTiles; y++) {
				var tile = new GameObject();
				tile.transform.position = new Vector3(x, y, 0) + offset;
				var renderer = _renderers[x,y] = tile.AddComponent<SpriteRenderer>();
				renderer.sortingOrder = sortIndex--;
				tile.name = "Terrain " + tile.transform.position;
				tile.transform.parent = transform;
			}
		}
		RedrawMap ();
	}
	
	// Update is called once per frame
	void Update () {
		if (MaxDistanceFromCenter < Vector3.Distance (Player.position, transform.position)) {
			RedrawMap ();
		}
	}
}
