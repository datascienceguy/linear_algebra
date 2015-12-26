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
           
    }
}
