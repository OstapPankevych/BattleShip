using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace BattleShip
{
    class Program
    {

        struct A
        {
            public byte a;
        }


        class B
        {
            A[] a;

            public B(byte size)
            {
                a = new A[size];

                for (byte i = 0; i < a.Length; i++)
                    a[i].a = i;
            }

            public A[] Get()
            {
                return a;
            }

            public void Show(byte i)
            {
                Console.WriteLine(a[i].a);
            }
        }

        static void Main(string[] args)
        {

            B b = new B(5);

            A[] a = b.Get();

            a[3] = new A() {a = 8};

            b.Show(3);
         

            Console.ReadLine();
        }
    }
}
