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

		public Map.MapVector2 tilePosition;
		public Transform Player_Spawn;

		[SerializeField]
		private AISpawnInfo[] AI_Spawns;
		[SerializeField]
		private Transform[] Pickup_Spawns;

		private void Start()
		{
			return;

			// TODO: Spawn sequence

			foreach (AISpawnInfo spawnInfo in this.AI_Spawns)
			{
				GameManager.SpawnAIVehicle("Ship_AI_" + spawnInfo.behaviourType.ToString(),
					spawnInfo.spawnPosition.position, spawnInfo.spawnPosition.rotation,
					spawnInfo.waypoints);
			}
		}
	}
}