using UnityEngine;
using MapVector2 = MapGeneration.Map.MapVector2;

namespace MapGeneration
{
	public static class MapDirections
	{
		public const int DirectionCount = 4;

		private static MapVector2[] directionVectors =
			{
				new MapVector2(0, 1),	// North
				new MapVector2(1, 0),	// East
				new MapVector2(0, -1),	// South
				new MapVector2(-1, 0)   // West
			};

		public static Enums.CardinalDirection GetRandomDirection()
		{
			return (Enums.CardinalDirection)Random.Range(0, DirectionCount);
		}

		public static MapVector2 ToMapVector2(this Enums.CardinalDirection direction)
		{
			return directionVectors[(int)direction];
		}
	}
}