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
			return true;	
		}

		public bool IsEqualTo(Line l)
		{
			return true;	
		}

		public Vector FindIntersection(Line l)
		{
			return new Vector(new List<double>{0, 0});
		}
		
		private int FirstNonzeroIndex(Vector v)
		{
			return v.Coordinates.FindIndex(c => Math.Abs(c) >= TOLERANCE);
		}
				
		
	}
}
