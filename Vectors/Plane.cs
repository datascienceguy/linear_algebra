using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vectors
{
    public class Plane
    {
        public Vector NormalVector { get; set; }
        public Vector BasePoint { get; set; }
		public int Dimension { get; set; }
        public double Constant { get; set; }
		private double TOLERANCE = Math.Pow(10, -10);
		
		public Plane(Vector normalVector = null, double constant = 0)
        {
			this.Dimension = 3;
			if( normalVector == null){
				var allZeroes = new List<double>( new double[this.Dimension] );
				normalVector = new Vector(allZeroes);
			}
			
			this.NormalVector = normalVector;
			this.Constant = constant;
			this.SetBasePoint();
        }
		
		public void SetBasePoint()
		{
			var basePointCoords = new List<double>( new double[this.Dimension] ).ToArray();
			var initIndex = FirstNonzeroIndex(this.NormalVector);
			if( initIndex < 0 ){
				this.BasePoint = null;
			} else {
				var initCoefficient = this.NormalVector.Coordinates.ElementAt(initIndex);
				
				basePointCoords[initIndex] = this.Constant / initCoefficient;
				this.BasePoint = new Vector(basePointCoords.ToList());
			}
		}
		
		public void Print()
		{
			var initIndex = FirstNonzeroIndex(this.NormalVector);
			var output = "";
			if( initIndex < 0 ){
				output = "Empty plane";
			} else {
				var coords = this.NormalVector.Coordinates.ToArray();
				for(var i = initIndex; i < coords.Count(); i++){
					var signPrefix = coords[i] < 0 ? " - " : (i > initIndex) ? " + " : "";
					var numericPrefix = Math.Abs(coords[i]) == 1 ? "" : Math.Abs(coords[i]).ToString();
					if(numericPrefix != "0"){
						output += signPrefix + numericPrefix + "x_" + (i+1);					
					}
				}
				output += " = " + this.Constant;			
			}
			Console.WriteLine(output);
		}
		
		public bool IsParallelTo(Plane p)
		{
			// Two Planes are parallel if the normal vectors are parallel
			return this.NormalVector.IsParallelTo( p.NormalVector );	
		}

		public bool IsEqualTo(Plane p)
		{
			if(this.NormalVector.IsZero()){
				if(! p.NormalVector.IsZero()){
					return false;
				} else {
					var diff = this.Constant - p.Constant;
					return IsNearZero(diff);
				}
			} else if(p.NormalVector.IsZero()){
				return false;
			}
			
			// Two Planes are equal (coincidental Planes) iff vector connecting point on each Plane is
			// orthoganal to the Planes normal vectors
			var basePointDiff = this.BasePoint.Minus(p.BasePoint); // Vector connecting a point on each Plane 
			return this.IsParallelTo(p) && basePointDiff.IsOrthoganalTo(p.NormalVector);
		}
		
		public bool Equals(Plane p)
		{
			return this.NormalVector.Equals(p.NormalVector) &&
				   this.Constant == p.Constant;
		}
		
		public Plane DeepCopy()
		{
			var newCoords = this.NormalVector.Coordinates.ToList();
			var newNormalVector = new Vector(newCoords);
			return new Plane(newNormalVector, this.Constant);
		}
		
		public int FirstNonzeroIndex(Vector v)
		{
			return v.Coordinates.FindIndex(c => ! IsNearZero(c) );
		}
		
		public int FirstNonzeroIndex()
		{
			return this.NormalVector.Coordinates.FindIndex(c => ! IsNearZero(c) );
		}

		public double GetColumn(int i)
		{
			return this.NormalVector.Coordinates.ElementAt(i);
		}
				
		private bool IsNearZero(double value)
		{
			return Math.Abs(value) < TOLERANCE;
		}
	}
}
