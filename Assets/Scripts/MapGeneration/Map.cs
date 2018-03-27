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
				CreateTile(this.GetRandomPosition())
			};

			while (activeTiles.Count > 0)
			{
				yield return null;
				SpawnRemainingTiles(activeTiles);
			}

			GameManager.RebuildNavMesh();


			foreach (Tile tile in this.TileMap)
			{
				yield return null;
				tile.SpawnAgents();
			}

			yield return null;
			this.SpawnPlayer();

			CameraSystem.EnableCamera();
		}

		private void SpawnRemainingTiles(List<Tile> activeTiles)
		{
			int currentIndex = activeTiles.Count - 1;
			Tile currentTile = activeTiles[currentIndex];

			if (currentTile.IsInitialized)
			{
				activeTiles.RemoveAt(currentIndex);
				return;
			}

			Enums.CardinalDirection direction = currentTile.GetRandomUnintializedDirection();
			MapVector2 newTilePosition = currentTile.TilePosition + direction.ToMapVector2();

			if (this.IsValidCoordinate(newTilePosition))
			{
				Tile neighbor = GetTile(newTilePosition);

				if (neighbor == null)
				{
					neighbor = CreateTile(newTilePosition);
					BuildConnector(currentTile, neighbor, direction);
					activeTiles.Add(neighbor);
				}
				else
				{
					BuildWall(currentTile, neighbor, direction);
				}
			}
			else
			{
				BuildWall(currentTile, null, direction);
			}
		}

		private Tile CreateTile(MapVector2 cellPosition)
		{
			Tile newTile;

			switch (Random.Range(0, 2))
			{
				case 0:
					newTile = Instantiate(Resources.Load<GameObject>("Blocks/Arena")).GetComponent<Tile>();
					break;
				case 1:
					newTile = Instantiate(Resources.Load<GameObject>("Blocks/Pipes")).GetComponent<Tile>();
					break;
				default:
					newTile = Instantiate(Resources.Load<GameObject>("Blocks/Arena")).GetComponent<Tile>();
					break;
			}

			this.TileMap[cellPosition.x, cellPosition.z] = newTile;

			newTile.TilePosition = new MapVector2(cellPosition.x, cellPosition.z);
			newTile.name += " Tile " + cellPosition.x + ", " + cellPosition.z;
			newTile.transform.parent = this.transform;
			newTile.transform.localPosition = new Vector3(cellPosition.x * 75, 0f, cellPosition.z * 75);

			return newTile;
		}

		private void BuildWall(Tile tile1, Tile tile2, Enums.CardinalDirection direction)
		{
			TileEdge wall = Instantiate(Resources.Load<GameObject>("Blocks/Wall")).GetComponent<TileEdge>();
			wall.InitializeEdge(tile1, tile2, direction);

			if (tile2 != null)
			{
				wall = Instantiate(Resources.Load<GameObject>("Blocks/Wall")).GetComponent<TileEdge>();
				wall.InitializeEdge(tile2, tile1, direction.GetOpposite());
			}
		}

		private void BuildConnector(Tile tile1, Tile tile2, Enums.CardinalDirection direction)
		{
			TileEdge connector = Instantiate(Resources.Load<GameObject>("Blocks/Connector")).GetComponent<TileEdge>();
			connector.InitializeEdge(tile1, tile2, direction);

			connector = Instantiate(Resources.Load<GameObject>("Blocks/Connector")).GetComponent<TileEdge>();
			connector.InitializeEdge(tile2, tile1, direction.GetOpposite());
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
			GetTile(GetRandomPosition()).SpawnPlayer();
		}
	}
}
