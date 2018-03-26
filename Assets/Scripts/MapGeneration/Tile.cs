using UnityEngine;

namespace MapGeneration
{
	public class Tile : MonoBehaviour
	{
		[System.Serializable]
		private struct AISpawnInfo
		{
			public Transform spawnPosition;
			public Enums.AI_BehaviourType behaviourType;
			public Transform[] waypoints;
		}

		public bool IsInitialized { get { return this.initializedEdgeCount == MapDirections.Count; } }

		public Map.MapVector2 tilePosition;
		public Transform Player_Spawn;

		private const string ResourcePrefix = "Ship_AI_";

		[SerializeField]
		private AISpawnInfo[] AI_Spawns;
		[SerializeField]
		private Transform[] Pickup_Spawns;
		[SerializeField]
		private Transform floorObject;

		private TileEdge[] edges = new TileEdge[MapDirections.Count];
		private int initializedEdgeCount;

		private void Start()
		{
			this.floorObject.Rotate(new Vector3(0, Random.Range(0, 4) * 90f, 0));

			// TODO: Spawn sequence
		}

		public void SpawnAgents()
		{
			foreach (AISpawnInfo spawnInfo in this.AI_Spawns)
			{
				GameManager.SpawnAIVehicle(
					ResourcePrefix + spawnInfo.behaviourType.ToString(),
					spawnInfo.spawnPosition.position, spawnInfo.spawnPosition.rotation,
					spawnInfo.waypoints)
					.transform.parent = this.transform;
			}
		}

		public void SpawnPlayer()
		{
			GameManager.SpawnPlayerVehicle(this.Player_Spawn.position, this.Player_Spawn.rotation);
		}

		public TileEdge GetEdge(Enums.CardinalDirection direction)
		{
			return this.edges[(int)direction];
		}

		public void SetEdge(Enums.CardinalDirection direction, TileEdge edge)
		{
			this.edges[(int)direction] = edge;
			this.initializedEdgeCount += 1;
		}

		public Enums.CardinalDirection GetRandomUnintializedDirection()
		{
			int skips = Random.Range(0, MapDirections.Count - this.initializedEdgeCount);

			for (int i = 0; i < MapDirections.Count; i++)
			{
				if (this.edges[i] == null)
				{
					if (skips == 0)
					{
						return (Enums.CardinalDirection)i;
					}

					skips -= 1;
				}
			}

			throw new System.InvalidOperationException("Tile has no uninitialized directions left.");
		}
	}
}