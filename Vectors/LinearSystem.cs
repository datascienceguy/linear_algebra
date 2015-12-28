using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vectors
{
    public class LinearSystem
    {
        public List<Plane> Rows { get; set; }
		public int Dimension { get; set; }
		private double TOLERANCE = Math.Pow(10, -10);
		
		public LinearSystem(List<Plane> planes)
        {
			this.Rows = planes;
			this.Dimension = planes[0].Dimension;
			foreach(var plane in planes){
				if(plane.Dimension != this.Dimension){
					throw new Exception("All planes must have the same dimension");
				}
			}
        }
		
		public List<int> IndicesOfFirstNonzeroTermPerRow()
		{
			var indices = new List<int>();
			this.Rows.ForEach(p => {
				var index = p.FirstNonzeroIndex(p.NormalVector);
				indices.Add(index);
			});	
			return indices;
		}	

		public void Print()
		{
			Console.WriteLine("Linear System:");
			this.Rows.ForEach(p => { p.Print(); });
		}

		public void SwapRows(int row1Index, int row2Index)
		{
			var row1 = this.GetRow(row1Index);
			var row2 = this.GetRow(row2Index);
			this.SetRow(row1Index, row2);
			this.SetRow(row2Index, row1);
		}
		
		public void MultiplyCoefficientAndRow(double coefficient, int rowIndex)
		{
			var newRow = this.MultiplyRow(coefficient, rowIndex);
			newRow.Constant *= coefficient;
			this.SetRow(rowIndex, newRow);
		}
		
		public void AddMultipleTimesRowToRow(double coefficient, int rowToAddIndex, int rowToBeAddedToIndex)
		{
			var rowToAdd = this.MultiplyRow(coefficient, rowToAddIndex);
			var rowToBeAddedTo = this.GetRow(rowToBeAddedToIndex);
			var newVector = rowToBeAddedTo.NormalVector.Add(rowToAdd.NormalVector);

			rowToBeAddedTo.NormalVector = newVector;
			rowToBeAddedTo.Constant += (coefficient * rowToAdd.Constant);
			this.SetRow(rowToBeAddedToIndex, rowToBeAddedTo);	
		}
		
		private Plane MultiplyRow(double coefficient, int rowIndex)
		{
			var row = this.GetRow(rowIndex);
			var copiedRow = new Plane(row.NormalVector, row.Constant);
			copiedRow.NormalVector = copiedRow.NormalVector.MultiplyScalar(coefficient);
			return copiedRow;
		}
		
		public Plane GetRow(int i)
		{
			return this.Rows.ElementAt(i);
		}
		
		public void SetRow(int i, Plane p)
		{
			var planesArray = this.Rows.ToArray();
			planesArray[i] = p;
			this.Rows = planesArray.ToList();
		}
		
		public LinearSystem ComputeTriangularForm()
		{
			var system = this.DeepCopy();
			var rowCount = this.Rows.Count;
			// First make sure each row has a leading pivoting variable (non-zero)
			for(var col = 0; (col + 1) < rowCount; col++){
				var row = system.GetRow(col);
				if(row.GetColumn(col) == 0){
					var swapIndex = -1;
					for(var swapRow = col + 1; swapRow < rowCount; swapRow++){
						if(system.GetRow(swapRow).GetColumn(col) != 0){
							swapIndex = swapRow;
						}						
					}
					if(swapIndex >= 0){
						system.SwapRows(col, swapIndex);
					} else {
						Console.WriteLine("No unique solution for system");
						return null;
					}
				}
			}
			return system;
		}
		
		public LinearSystem DeepCopy()
		{
			var newPlanes = new List<Plane>();
			this.Rows.ForEach(r => {
				newPlanes.Add(r.DeepCopy());
			});
			return new LinearSystem(newPlanes);
		}
				
		private bool IsNearZero(double value)
		{
			return Math.Abs(value) < TOLERANCE;
		}
	}
}
