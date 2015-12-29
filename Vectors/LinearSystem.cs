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
		
		public LinearSystem ComputeRREF()
		{
			var system = this.DeepCopy();
			var t = system.ComputeTriangularForm();
			var rowCount = system.Rows.Count;
			var colCount = system.GetRow(0).Dimension;
			for(var col = 1; col < colCount; col++){
				for(var row = 0; row < rowCount && col < rowCount && row != col; row++){
					var currValue = t.GetItem(row, col); 
					if(currValue != 0){
						var scalar = currValue * -1;
						t.AddMultipleTimesRowToRow(scalar, col, row);					
					}
				}
			}
			
			for(var row = colCount; row < rowCount; row++){
				t.SetRow(row, new Plane());
			}
			
			return t;			
		}
		
		public LinearSystem ComputeTriangularForm()
		{
			var system = this.DeepCopy();
			try {
				system = this.PivotOrder(system);
				system = this.Elimination(system);
				return system;
			} catch(Exception ex){
				Console.WriteLine(ex.Message);
				return null;			
			}
		}
		
		private LinearSystem PivotOrder(LinearSystem system)
		{
			var rowCount = system.Rows.Count;
			var colCount = system.GetRow(0).Dimension;

			// First make sure each row has a leading pivoting variable (non-zero)
			for(var row = 0; row < rowCount && row < colCount; row++){
				var pivotValue = system.GetItem(row, row);
				if(pivotValue == 0){
					var swapped = false;
					for(var swapRow = row + 1; swapRow < rowCount; swapRow++){
						if(!swapped && system.GetItem(swapRow, row) != 0){
							system.SwapRows(row, swapRow);
							swapped = true;
						}
					}
				}
			}
			return system;			
		}
		
		private LinearSystem Elimination(LinearSystem system)
		{
			var rowCount = system.Rows.Count;
			var colCount = system.GetRow(0).Dimension;
			for (int sourceRow = 0; sourceRow < Math.Min(rowCount, colCount); sourceRow++){
				// Make pivot variable == 1
				var sourceValue = system.GetItem(sourceRow, sourceRow);
				if(sourceValue != 0){
					var sourceScalar = 1/sourceValue;
					system.MultiplyCoefficientAndRow(sourceScalar, sourceRow);
				} 

				// For all rows below pivot variable, make value zero
				for (int destRow = sourceRow + 1; destRow < rowCount; destRow++){
					var destValue = system.GetItem(destRow, sourceRow);
					var destScalar = destValue < 0 ? destValue : -1 * destValue;
					if(destScalar != 0){
						system.AddMultipleTimesRowToRow(destScalar, sourceRow, destRow);
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
				
		private double GetItem(int row, int col)
		{
			return this.GetRow(row).GetColumn(col);
		}				
				
		private bool IsNearZero(double value)
		{
			return Math.Abs(value) < TOLERANCE;
		}
	}
}
