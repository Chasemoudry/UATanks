using UnityEngine;
using MapVector2 = MapGeneration.Map.MapVector2;
using CardinalDirection = Enums.CardinalDirection;

namespace MapGeneration
{
	public static class MapDirections
	{
		public const int Count = 4;

		private static MapVector2[] directionVectors =
		{
			new MapVector2(0, 1),
			new MapVector2(1, 0),
			new MapVector2(0, -1),
			new MapVector2(-1, 0)
		};

		private static CardinalDirection[] cardinalOpposites =
		{
			CardinalDirection.South,
			CardinalDirection.West,
			CardinalDirection.North,
			CardinalDirection.East
		};

		private static Quaternion[] cardinalRotations =
		{
			Quaternion.identity,
			Quaternion.Euler(0f, 90f, 0f),
			Quaternion.Euler(0f, 180f, 0f),
			Quaternion.Euler(0f, 270f, 0f)
		};

		public static CardinalDirection GetRandomDirection()
		{
			return (CardinalDirection)Random.Range(0, Count);
		}

		public static MapVector2 ToMapVector2(this CardinalDirection direction)
		{
			return directionVectors[(int)direction];
		}

		public static CardinalDirection GetOpposite(this CardinalDirection direction)
		{
			return cardinalOpposites[(int)direction];
		}

		public static Quaternion ToRotation(this CardinalDirection direction)
		{
			return cardinalRotations[(int)direction];
		}
	}
}