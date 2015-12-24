using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vectors
{
    public class Vector
    {
        public List<double> Coordinates { get; set; }
        public int Dimension { get; set; }
		private double TOLERANCE = Math.Pow(10, -10);
		
		public Vector(List<double> coordinates)
        {
			this.Coordinates = coordinates;
			this.Dimension = coordinates.Count;
        }
		
		public void Print()
		{
			Console.WriteLine("Vector: (" + String.Join(",", Coordinates) + ")"); 
		}
		
		public bool Equals (Vector v)
		{	
			return v != null && this.Coordinates.SequenceEqual(v.Coordinates);
		}
		
		public Vector Add(Vector v)
		{
			Func<double, double, double> sum = (a, b) => a + b;
			return Apply(sum, v);
		}
		
		public Vector Minus(Vector v)
		{
			Func<double, double, double> minus = (a, b) => a - b;
			return Apply(minus, v);
		}		

		public double Dot(Vector v)
		{
			Func<double, double, double> multiply = (a, b) => a * b;
			var multiplyVector = Apply(multiply, v);
			return multiplyVector.Coordinates.Sum();
		}
		
		public double DotAngle(Vector v, bool asDegrees = true)
		{
			var v1Normalized = this.Normalize();
			var v2Normalized = v.Normalize();
			var dotProduct = v1Normalized.Dot(v2Normalized);
			var angle = Math.Acos( TruncateDouble(dotProduct) );
			return asDegrees ? angle * 180/Math.PI : angle;
		}
		
		public Vector MultiplyScalar(double scalar)
		{
			return new Vector(this.Coordinates.Select(x => x * scalar).ToList());
		}
		
		public double Magnitude()
		{
			return Math.Sqrt(this.Coordinates.Sum(x => x * x));
		}
		
		public Vector Normalize()
		{
			try {
				var scalar = 1 / this.Magnitude();
				return this.MultiplyScalar(scalar); 
			} catch(DivideByZeroException){
				throw new Exception("Can't normalize the zero vector");
			}
		}
		
		public bool IsParallelTo(Vector v)
		{
			return this.IsZero() ||
					v.IsZero() ||
					this.DotAngle(v) == 0 ||
					this.DotAngle(v) == 180;
		}
		
		public bool IsOrthoganalTo(Vector v)
		{
			return Math.Abs(this.Dot(v)) <= TOLERANCE;
		}
		
		public bool IsZero()
		{
			return this.Magnitude() <= TOLERANCE;
		}
		
		private Vector Apply(Func<double, double, double> func, Vector v)
		{
			if( this.Dimension != v.Dimension){
				throw new Exception("Dimensions of vectors must be equal");
			}
			
			var newCoordinates = this.Coordinates
						.Select( (x, index) => func(x, v.Coordinates.ElementAt(index)) )
						.ToList();
			return new Vector(newCoordinates);
		}
		
		private double TruncateDouble(double value)
		{
			// Truncate to 5 decimals due to strange acos issues with -1.0000000
			return Math.Truncate(value * 100000) / 100000;
		}
    }
}
