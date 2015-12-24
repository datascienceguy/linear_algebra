using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Vectors
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Problem 1");
            Problem1();
            Console.WriteLine("---------------");

            Console.WriteLine("Problem 2");
            Problem2();
            Console.WriteLine("---------------");

            Console.WriteLine("Problem 3");
            Problem3();
            Console.WriteLine("---------------");

            Console.WriteLine("Problem 4");
            Problem4();
            Console.WriteLine("---------------");
            
            Console.WriteLine("Problem 5");
            Problem5();
            Console.WriteLine("---------------");            

            Console.WriteLine("Problem 6");
            Problem6();
            Console.WriteLine("---------------"); 
        }
        
        private static void Problem1()
        {
            var v1 = new Vector(new List<double>{8.218, -9.341});
            var v2 = new Vector(new List<double>{-1.129, 2.111});
            v1.Add(v2).Print();

            var v3 = new Vector(new List<double>{7.119, 8.215});
            var v4 = new Vector(new List<double>{-8.223, 0.878});
            v3.Minus(v4).Print();

            var v5 = new Vector(new List<double>{1.671, -1.012, -0.318});
            v5.MultiplyScalar(7.41).Print();
        }
        
        private static void Problem2()
        {
            var v1 = new Vector(new List<double>{-0.221, 7.437});
            Console.WriteLine(v1.Magnitude());
            
            var v2 = new Vector(new List<double>{8.813, -1.331, -6.247});
            Console.WriteLine(v2.Magnitude());
            
            var v3 = new Vector(new List<double>{5.581, -2.136});
            v3.Normalize().Print();
            
            var v4 = new Vector(new List<double>{1.996, 3.108, -4.554});
            v4.Normalize().Print();
        }
        
        private static void Problem3()
        {
           var v1 = new Vector(new List<double>{7.887, 4.138});
           var v2 = new Vector(new List<double>{-8.802, 6.776});
           Console.WriteLine(v1.Dot(v2)); 

           var v3 = new Vector(new List<double>{-5.955, -4.904, -1.874});
           var v4 = new Vector(new List<double>{-4.496, -8.755, 7.103});
           Console.WriteLine(v3.Dot(v4)); 

           var v5 = new Vector(new List<double>{3.183, -7.627});
           var v6 = new Vector(new List<double>{-2.668, 5.319});
           Console.WriteLine(v5.DotAngle(v6, false)); 

           var v7 = new Vector(new List<double>{7.35, 0.221, 5.188});
           var v8 = new Vector(new List<double>{2.751, 8.259, 3.985});
           Console.WriteLine(v7.DotAngle(v8, true)); 
        }
        
        private static void Problem4()
        {
           var v1 = new Vector(new List<double>{-7.579, -7.88});
           var v2 = new Vector(new List<double>{22.737, 23.64});
           Console.WriteLine("Parallel? " + v1.IsParallelTo(v2).ToString()); 
           Console.WriteLine("Orthoganal? " + v1.IsOrthoganalTo(v2).ToString()); 

           var v3 = new Vector(new List<double>{-2.029, 9.97, 4.172});
           var v4 = new Vector(new List<double>{-9.231, -6.639, -7.245});
           Console.WriteLine("Parallel? " + v3.IsParallelTo(v4).ToString()); 
           Console.WriteLine("Orthoganal? " + v3.IsOrthoganalTo(v4).ToString()); 

           var v5 = new Vector(new List<double>{-2.328, -7.284, -1.214});
           var v6 = new Vector(new List<double>{-1.821, 1.072, -2.94});
           Console.WriteLine("Parallel? " + v5.IsParallelTo(v6).ToString()); 
           Console.WriteLine("Orthoganal? " + v5.IsOrthoganalTo(v6).ToString()); 
 
           var v7 = new Vector(new List<double>{2.118, 4.827});
           var v8 = new Vector(new List<double>{0, 0});
           Console.WriteLine("Parallel? " + v7.IsParallelTo(v8).ToString()); 
           Console.WriteLine("Orthoganal? " + v7.IsOrthoganalTo(v8).ToString()); 
        }       
        
        private static void Problem5()
        {
           var v1 = new Vector(new List<double>{3.039, 1.879});
           var b1 = new Vector(new List<double>{0.825, 2.036});
           v1.Projection(b1).Print();
           
           var v2 = new Vector(new List<double>{-9.88, -3.264, -8.159});
           var b2 = new Vector(new List<double>{-2.155, -9.353, -9.473});
           v2.Component(b2).Print();

           var v3 = new Vector(new List<double>{3.009, -6.172, 3.692, -2.51});
           var b3 = new Vector(new List<double>{6.404, -9.144, 2.759, 8.718});
           v3.Projection(b3).Print();
           v3.Component(b3).Print();
        }         

        private static void Problem6()
        {
           var v1 = new Vector(new List<double>{8.462, 7.893, -8.187});
           var w1 = new Vector(new List<double>{6.984, -5.975, 4.778});
           v1.CrossProduct(w1).Print();
           
           var v2 = new Vector(new List<double>{-8.987, -9.838, 5.031});
           var w2 = new Vector(new List<double>{-4.268, -1.861, -8.866});
           Console.WriteLine("Parallelogram area: " + v2.ParallelogramArea(w2));

           var v3 = new Vector(new List<double>{1.5, 9.547, 3.691});
           var w3 = new Vector(new List<double>{-6.007, 0.124, 5.772});
           Console.WriteLine("Triangle area: " + v3.TriangleArea(w3));
        }     
   
    }
}
