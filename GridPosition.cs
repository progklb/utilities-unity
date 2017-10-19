using UnityEngine;
using System.Collections;

namespace Utilities 
{
	/// <summary>
	/// Stores a 2 dimensional coordinate with integer components. 
	/// Has convenience functionality to return neighboring positions.
	/// </summary>
	public class GridPosition 
	{
		#region VARIABLES
		public int x, y;
		public int gridSize = 1;
		#endregion


		public enum Neighbor
		{
			N, NW, W, SW, S, SE, E, NE
		}


		#region CONSTRUCTORS
		public GridPosition() { }

		public GridPosition(int x, int y) 
		{
			this.x = x;
			this.y = y;
		}
		public GridPosition(GridPosition pos) 
		{
			this.x = pos.x;
			this.y = pos.y;
		}
		#endregion


		#region PUBLIC-FACING API
		/// <summary>
		/// Sets a custom grid size, as opposed to the default (1). This represents the spacing between neighboring cells.
		/// For example, if gridSize = 10, then grid position's (0, 0) northern neighbor (N) will be at position (0, 10).
		/// </summary>
		public void SetGridSize(int size)
		{
			gridSize = size;
		}

		public void SetPosition(int x, int y) 
		{
			this.x = x;
			this.y = y;
		}

		public GridPosition GetPosition() 
		{
			return this;
		}

		public Vector2 GetPositionAsVector2() 
		{
			return new Vector2 (x, y);
		}

		public Vector3 GetPositionAsVector3() 
		{
			return new Vector3 (x, y, 0);
		}

		public GridPosition GetNeighborPosition(Neighbor dir) 
		{
			switch (dir) 
			{
				case Neighbor.N:	return new GridPosition	(x, 			y + gridSize);	
				case Neighbor.NW:	return new GridPosition	(x - gridSize, 	y + gridSize);
				case Neighbor.W:	return new GridPosition	(x - gridSize, 	y);
				case Neighbor.SW:	return new GridPosition	(x - gridSize, 	y - gridSize);
				case Neighbor.S:	return new GridPosition	(x, 			y - gridSize);
				case Neighbor.SE:	return new GridPosition	(x + gridSize, 	y - gridSize);
				case Neighbor.E:	return new GridPosition	(x + gridSize, 	y);
				case Neighbor.NE:	return new GridPosition	(x + gridSize, 	y + gridSize);
				default:
					Debug.Log (this.ToString() + ": No such neighboring position exists. Aren't you familiar with simple compass directions? Also, the GridPosition you just got back was garbage: (0,0)");
					return new GridPosition (0, 0);
			}
		}

		/// <summary>
		///Returns a description of this item in the form of (x,y).
		/// </summary>
		public override string ToString() {
			return ("(" + x + "," + y +")");
		}
		#endregion
	}
}
