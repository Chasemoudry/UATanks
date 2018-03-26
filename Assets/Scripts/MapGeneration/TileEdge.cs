using UnityEngine;

namespace MapGeneration
{
	public class TileEdge : MonoBehaviour
	{
		public Tile thisTile;
		public Tile otherTile;
		Enums.CardinalDirection direction;

		public void InitializeEdge(Tile thisTile, Tile otherTile, Enums.CardinalDirection direction)
		{
			this.thisTile = thisTile;
			this.otherTile = otherTile;
			this.direction = direction;

			thisTile.SetEdge(direction, this);
			this.transform.parent = thisTile.transform;
			this.name = "Edge " + direction.ToString();
			switch (direction)
			{
				case Enums.CardinalDirection.North:
					this.transform.localPosition = new Vector3(0f, 1f, 75f);
					break;
				case Enums.CardinalDirection.East:
					this.transform.localPosition = new Vector3(75f, 1f, 75f);
					break;
				case Enums.CardinalDirection.South:
					this.transform.localPosition = new Vector3(75f, 1f, 0f);
					break;
				case Enums.CardinalDirection.West:
					this.transform.localPosition = new Vector3(0f, 1f, 0f);
					break;
				default:
					break;
			}
			this.transform.localRotation = direction.ToRotation();
		}
	}
}
