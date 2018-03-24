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

		public Transform Player_Spawn;
		[SerializeField]
		private AISpawnInfo[] AI_Spawns;
		public Transform[] Pickup_Spawns;

		private void Start()
		{
			// TODO: Spawn sequence
			GameManager.SpawnPlayerVehicle("Ship_Player", this.Player_Spawn.position,
				this.Player_Spawn.rotation);

			foreach (AISpawnInfo spawnInfo in this.AI_Spawns)
			{
				GameManager.SpawnAIVehicle("Ship_AI_" + spawnInfo.behaviourType.ToString(),
					spawnInfo.spawnPosition.position, spawnInfo.spawnPosition.rotation,
					spawnInfo.waypoints);
			}
		}
	}
}