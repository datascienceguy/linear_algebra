using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Vectors
{
    public class Lesson2
    {
        public void Run()
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
        }
        
        private void Problem1()
        {
            var n1 = new Vector(new List<double>{4.046, 2.836});
            var c1 = 1.21;
            var l1 = new Line(n1, c1);
            var n2 = new Vector(new List<double>{10.115, 7.09});
            var c2 = 3.025;
            var l2 = new Line(n2, c2);            
            l1.Print();
            l2.Print();
            var intersection1 = l1.FindIntersection(l2);
            if(intersection1 != null){ intersection1.Print(); }
            Console.WriteLine("Parallel? " + l1.IsParallelTo(l2).ToString());
            Console.WriteLine("Equal? " + l1.IsEqualTo(l2).ToString());

            var n3 = new Vector(new List<double>{7.204, 3.182});
            var c3 = 8.68;
            var l3 = new Line(n3, c3);
            var n4 = new Vector(new List<double>{8.172, 4.114});
            var c4 = 9.883;
            var l4 = new Line(n4, c4);            
            l3.Print();
            l4.Print();
            var intersection2 = l3.FindIntersection(l4);
            if(intersection2 != null){ intersection2.Print(); }
            Console.WriteLine("Parallel? " + l3.IsParallelTo(l4).ToString());
            Console.WriteLine("Equal? " + l3.IsEqualTo(l4).ToString());

            var n5 = new Vector(new List<double>{1.182, 5.562});
            var c5 = 6.744;
            var l5 = new Line(n5, c5);
            var n6 = new Vector(new List<double>{1.773, 8.343});
            var c6 = 9.525;
            var l6 = new Line(n6, c6);            
            l5.Print();
            l6.Print();
            var intersection3 = l5.FindIntersection(l6);
            if(intersection3 != null){ intersection3.Print(); }
            Console.WriteLine("Parallel? " + l5.IsParallelTo(l6).ToString());
            Console.WriteLine("Equal? " + l5.IsEqualTo(l6).ToString());

        }
        
       private void Problem2()
        {
            var n1 = new Vector(new List<double>{-0.412, 3.806, 0.728});
            var c1 = -3.46;
            var p1 = new Plane(n1, c1);
            var n2 = new Vector(new List<double>{1.03, -9.515, -1.82});
            var c2 = 8.65;
            var p2 = new Plane(n2, c2);
            p1.Print();
            p2.Print();
            Console.WriteLine("Parallel? " + p1.IsParallelTo(p2).ToString());
            Console.WriteLine("Equal? " + p1.IsEqualTo(p2).ToString());            

            var n3 = new Vector(new List<double>{2.611, 5.528, 0.283});
            var c3 = 4.6;
            var p3 = new Plane(n3, c3);
            var n4 = new Vector(new List<double>{7.715, 8.306, 5.342});
            var c4 = 3.76;
            var p4 = new Plane(n4, c4);
            p3.Print();
            p4.Print();
            Console.WriteLine("Parallel? " + p3.IsParallelTo(p4).ToString());
            Console.WriteLine("Equal? " + p3.IsEqualTo(p4).ToString());    

            var n5 = new Vector(new List<double>{-7.926, 8.625, -7.212});
            var c5 = -7.952;
            var p5 = new Plane(n5, c5);
            var n6 = new Vector(new List<double>{-2.642, 2.875, -2.404});
            var c6 = -2.443;
            var p6 = new Plane(n6, c6);
            p5.Print();
            p6.Print();
            Console.WriteLine("Parallel? " + p5.IsParallelTo(p6).ToString());
            Console.WriteLine("Equal? " + p5.IsEqualTo(p6).ToString()); 

        }        

        private void Problem3()
        {
            var p0 = new Plane(new Vector(new List<double>{1,1,1}), 1);
            var p1 = new Plane(new Vector(new List<double>{0,1,0}), 2);
            var p2 = new Plane(new Vector(new List<double>{1,1,-1}), 3);
            var p3 = new Plane(new Vector(new List<double>{1,0,-2}), 2);
            
            var s = new LinearSystem(new List<Plane>{p0, p1, p2, p3});
            s.Print();
            
Console.WriteLine("---------1-------");
            s.SwapRows(0, 1);
            s.Print();
            if(! (s.GetRow(0) == p1 && s.GetRow(1) == p0 && s.GetRow(2) == p2 && s.GetRow(3) == p3)){
                Console.WriteLine("Test 1 failed");
            }
            
Console.WriteLine("---------2-------");
            s.SwapRows(1, 3);
            s.Print();
            if(! (s.GetRow(0) == p1 && s.GetRow(1) == p3 && s.GetRow(2) == p2 && s.GetRow(3) == p0)){
                Console.WriteLine("Test 2 failed");
            }            
            
Console.WriteLine("---------3-------");
            s.SwapRows(3, 1);
            s.Print();
            if(! (s.GetRow(0) == p1 && s.GetRow(1) == p0 && s.GetRow(2) == p2 && s.GetRow(3) == p3)){
                Console.WriteLine("Test 3 failed");
            }          

Console.WriteLine("---------4-------");
            s.MultiplyCoefficientAndRow(1, 0);
            s.Print();
            if(! (s.GetRow(0).Equals(p1) && s.GetRow(1) == p0 && s.GetRow(2) == p2 && s.GetRow(3) == p3)){
                Console.WriteLine("Test 4 failed");
            }          

Console.WriteLine("---------5-------");
            s.MultiplyCoefficientAndRow(-1, 2);
            s.Print();
            var newP2 = new Plane(new Vector(new List<double>{-1,-1,1}), -3);  
            if(! (s.GetRow(0).Equals(p1) && s.GetRow(1) == p0 && s.GetRow(2).Equals(newP2) && s.GetRow(3) == p3)){
                Console.WriteLine("Test 5 failed");
            }          
            
Console.WriteLine("---------6-------");
            s.MultiplyCoefficientAndRow(10, 1);        
            s.Print();
            var newP1 = new Plane(new Vector(new List<double>{10,10,10}), 10);      
            if(! (s.GetRow(0).Equals(p1) && s.GetRow(1).Equals(newP1) && s.GetRow(2).Equals(newP2) && s.GetRow(3) == p3)){
                Console.WriteLine("Test 6 failed");
            }                      
            
Console.WriteLine("---------7-------");
            s.AddMultipleTimesRowToRow(0, 0, 1);
            s.Print();
            if(! (s.GetRow(0).Equals(p1) && s.GetRow(1).Equals(newP1) && s.GetRow(2).Equals(newP2) && s.GetRow(3) == p3)){
                Console.WriteLine("Test 7 failed");
            }  

Console.WriteLine("---------8-------");
            s.AddMultipleTimesRowToRow(1, 0, 1);
            s.Print();
            var newNewP1 = new Plane(new Vector(new List<double>{10,11,10}), 12);  
            if(! (s.GetRow(0).Equals(p1) && s.GetRow(1).Equals(newNewP1) && s.GetRow(2).Equals(newP2) && s.GetRow(3) == p3)){
                Console.WriteLine("Test 8 failed");
            }  

Console.WriteLine("---------9-------");
            s.AddMultipleTimesRowToRow(-1, 1, 0);
            s.Print();
            var newP0 = new Plane(new Vector(new List<double>{-10,-10,-10}), -10);  
            if(! (s.GetRow(0).Equals(newP0) && s.GetRow(1).Equals(newNewP1) && s.GetRow(2).Equals(newP2) && s.GetRow(3) == p3)){
                Console.WriteLine("Test 9 failed");
            }  
        }           
    }
}
