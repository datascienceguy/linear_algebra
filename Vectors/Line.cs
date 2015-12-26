using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vectors
{
    public class Line
    {
        public Vector NormalVector { get; set; }
        public Vector BasePoint { get; set; }
		public int Dimension { get; set; }
        public double Constant { get; set; }
		private double TOLERANCE = Math.Pow(10, -10);
		
		public Line(Vector normalVector = null, double constant = 0)
        {
			this.Dimension = 2;
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
				output = "0";
			} else {
				var coords = this.NormalVector.Coordinates.ToArray();
				for(var i = initIndex; i < coords.Count(); i++){
					var prefix = coords[i] < 0 ? " - " : (i > initIndex) ? " + " : "";
					output += prefix + coords[i] + "x_" + (i+1);					
				}
				output += " = " + this.Constant;			
			}
			Console.WriteLine(output);
		}
		
		public bool IsParallelTo(Line l)
		{
			// Two lines are parallel if the normal vectors are parallel
			return this.NormalVector.IsParallelTo( l.NormalVector );	
		}

		public bool IsEqualTo(Line l)
		{
			if(this.NormalVector.IsZero()){
				if(! l.NormalVector.IsZero()){
					return false;
				} else {
					var diff = this.Constant - l.Constant;
					return IsNearZero(diff);
				}
			} else if(l.NormalVector.IsZero()){
				return false;
			}
			
			// Two lines are equal (coincidental lines) iff vector connecting point on each line is
			// orthoganal to the lines normal vectors
			var basePointDiff = this.BasePoint.Minus(l.BasePoint); // Vector connecting a point on each line 
			return this.IsParallelTo(l) && basePointDiff.IsOrthoganalTo(l.NormalVector);
		}

		public Vector FindIntersection(Line l)
		{
			if(this.IsParallelTo(l)){
				Console.WriteLine("No intersection, the lines are parallel and/or equal");
				return null;
			} else {
				// Given 2 lines: Ax + By = k1, Cx + Dy = k2
				// x = (D*k1 - B*k2) / (A*D - B*C)
				// y = (-C*k1 + A*k2) / (A*D - B*C)
				var A = this.NormalVector.Coordinates[0];
				var B = this.NormalVector.Coordinates[1];
				var C = l.NormalVector.Coordinates[0];
				var D = l.NormalVector.Coordinates[1];
				var k1 = this.Constant;
				var k2 = l.Constant;
				
				var x = (D*k1 - B*k2) / (A*D - B*C);
				var y = (-1*C*k1 + A*k2) / (A*D - B*C);
				return new Vector(new List<double>{x,y}); 
			}
		}
		
		private int FirstNonzeroIndex(Vector v)
		{
			return v.Coordinates.FindIndex(c => ! IsNearZero(c) );
		}
				
		private bool IsNearZero(double value)
		{
			return Math.Abs(value) < TOLERANCE;
		}
	}
}
