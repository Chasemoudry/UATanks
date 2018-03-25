using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapGeneration
{
	public class Map : MonoBehaviour
	{
		[System.Serializable]
		public struct MapVector2
		{
			public static MapVector2 operator +(MapVector2 v1, MapVector2 v2)
			{
				return new MapVector2(v1.x + v2.x, v1.z + v2.z);
			}

			public int x;
			public int z;

			public MapVector2(int x, int z)
			{
				this.x = x;
				this.z = z;
			}
		}

		public Tile[,] TileMap { get; private set; }

		public MapVector2 MapSize = new MapVector2(1, 1);

		private void Start()
		{
			this.StartCoroutine(this.GenerateMap());
		}

		private IEnumerator GenerateMap()
		{
			this.TileMap = new Tile[this.MapSize.x, this.MapSize.z];

			var activeTiles = new List<Tile>
			{
				// Spawn first tile
				SpawnTile(this.GetRandomPosition())
			};

			while (activeTiles.Count > 0)
			{
				yield return null;
				SpawnRemainingTiles(activeTiles);
			}

			GameManager.RebuildNavMesh();

			this.SpawnPlayer();
		}

		private void SpawnRemainingTiles(List<Tile> activeTiles)
		{
			int currentIndex = activeTiles.Count - 1;
			MapVector2 newTilePosition = activeTiles[currentIndex].tilePosition + MapDirections.GetRandomDirection().ToMapVector2();

			if (this.IsValidCoordinate(newTilePosition) && GetTile(newTilePosition) == null)
			{
				activeTiles.Add(SpawnTile(newTilePosition));
			}
			else
			{
				activeTiles.RemoveAt(currentIndex);
			}
		}

		private Tile SpawnTile(MapVector2 cellPosition)
		{
			//Debug.Log("Populating " + cellPosition.x + " , " + cellPosition.z + "!");

			Tile newTile = Instantiate(Resources.Load<GameObject>("Blocks/Arena")).GetComponent<Tile>();

			this.TileMap[cellPosition.x, cellPosition.z] = newTile;

			newTile.tilePosition = new MapVector2(cellPosition.x, cellPosition.z);
			newTile.name += " Tile " + cellPosition.x + ", " + cellPosition.z;
			newTile.transform.parent = this.transform;
			newTile.transform.localPosition =
				new Vector3(cellPosition.x * 75, 0f, cellPosition.z * 75);

			return newTile;
		}

		private MapVector2 GetRandomPosition()
		{
			return new MapVector2(Random.Range(0, this.MapSize.x), Random.Range(0, this.MapSize.z));
		}

		private bool IsValidCoordinate(MapVector2 vector)
		{
			return vector.x >= 0
				&& vector.x < this.MapSize.x
				&& vector.z >= 0
				&& vector.z < this.MapSize.z;
		}

		private Tile GetTile(MapVector2 cellPosition)
		{
			return this.TileMap[cellPosition.x, cellPosition.z];
		}

		private void SpawnPlayer()
		{
			//GameManager.SpawnPlayerVehicle(this.Player_Spawn.position, this.Player_Spawn.rotation);
		}
	}
}
