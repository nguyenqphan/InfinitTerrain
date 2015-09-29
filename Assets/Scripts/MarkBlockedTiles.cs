using UnityEngine;
using System.Collections;

public class MarkBlockedTiles : MonoBehaviour {
	
	public TerrainManager Terrain_Manager;
	private float radius = 0.25f;
	private float tileRange = 4;
	
	void OnDrawGizmos()
	{
		if (!Application.isPlaying)
			return;
		
		for (float x = transform.position.x - tileRange; x <= transform.position.x + tileRange; x += radius) {
			for (float y = transform.position.y - tileRange; y <= transform.position.y + tileRange; y += radius) {
				Vector3 worldPos = new Vector3(x, y, 0);
				bool isBlocked = false;
				Terrain_Manager.SelectRandomSprite(x, y, out isBlocked);
				
				if (isBlocked)
				{
					UnityEditor.Handles.DrawWireDisc(worldPos, Vector3.back, radius);
				}
			}
		}
	}
}
