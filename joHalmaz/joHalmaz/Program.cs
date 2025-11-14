using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace joHalmaz
{
    internal class Program
    {
        class JoHalmaz
        {
            List<int> lista;

            public JoHalmaz()
            {
                lista = new List<int>();
            }

            private JoHalmaz(List<int> lista)
            {
                this.lista = lista;
            }

            public void Add(int cucc)
            {
                int i = Helye(cucc);
                if (i == this.lista.Count || this.lista[i] != cucc)
                    this.lista.Insert(i, cucc);
            }

            private int Helye(int cucc)
            {
                int e = 0; //pseudo kód: 1
                int v = lista.Count - 1; //pseudo kód: lista.Hossz
                int k;
                if (lista.Count == 0)
                    return 0;
                do
                {
                    k = (e + v) / 2; //pseudo kód: Lefele_kerekít((e+v)/2)
                    if (lista[k] < cucc)
                        e = k + 1;
                    else if (cucc < lista[k])
                        v = k - 1;
                }while (e <= v && lista[k] != cucc);

                return cucc == lista[k] ? k : e;
            }

            public override string ToString() => "[ " + string.Join(", ", lista) + "]";

            public int FindIndex(int cucc)
            {
                int h = Helye(cucc);
                return (h < this.lista.Count && this.lista[h] == cucc) ? h : -1;
            }

            public bool Contains(int cucc) => lista[Helye(cucc)] == cucc;

            public static JoHalmaz operator +(JoHalmaz A, JoHalmaz B) // Únió
            {
                List<int> eredmenylista = new List<int>(A.lista.Count + B.lista.Count);
                int i = 0;
                int j = 0;
                while (i < A.lista.Count && j < B.lista.Count)
                {
                    if (A.lista[i] < B.lista[j])
                    {
                        eredmenylista.Add(A.lista[i]);
                        i++;
                    }
                    else if (A.lista[i] > B.lista[j])
                    {
                        eredmenylista.Add(B.lista[j]);
                        j++;
                    }
                    else
                    {
                        eredmenylista.Add(A.lista[i]);
                        i++;
                        j++;
                    }
                }
                while (i < A.lista.Count)
                {
                    eredmenylista.Add(A.lista[i]);
                    i++;
                }
                while (j < B.lista.Count)
                {
                    eredmenylista.Add(B.lista[j]);
                    j++;
                }
                return new JoHalmaz(eredmenylista);
            }

            public static JoHalmaz operator *(JoHalmaz A, JoHalmaz B) // metszet
            {
                List<int> eredmeny = new List<int>();
                int i = 0;
                int j = 0;

                while (i < A.lista.Count && j < B.lista.Count)
                {
                    if (A.lista[i] < B.lista[j])
                        i++;
                    else if (A.lista[i] > B.lista[j])
                        j++;
                    else
                    {
                        eredmeny.Add(A.lista[i]);
                        i++;
                        j++;
                    }
                }
                return new JoHalmaz(eredmeny);
            }

            public static JoHalmaz operator -(JoHalmaz A, JoHalmaz B) // kivonás
            {
                List<int> eredmenylista = new List<int>(A.lista.Count + B.lista.Count);
                int i = 0;
                int j = 0;
                while (i < A.lista.Count && j < B.lista.Count)
                {
                    if (A.lista[i] < B.lista[j])
                    {
                        eredmenylista.Add(A.lista[i]);
                        i++;
                    }
                    else if (A.lista[i] > B.lista[j])
                    {
                        j++;
                    }
                    else
                    {
                        i++;
                        j++;
                    }
                }
                while (i < A.lista.Count)
                {
                    eredmenylista.Add(A.lista[i]);
                    i++;
                }
                return new JoHalmaz(eredmenylista);
            }
        }

        static void Main(string[] args)
        {
            JoHalmaz h = new JoHalmaz();
            h.Add(3);
            h.Add(2);
            h.Add(10);
            h.Add(7);
            h.Add(8);
            h.Add(0);
            h.Add(1);
            h.Add(6);
            h.Add(5);
            h.Add(4);
            h.Add(3);
            h.Add(2);
            h.Add(9);
            Console.WriteLine(h);

            JoHalmaz g = new JoHalmaz();
            g.Add(1);
            g.Add(3);
            g.Add(8);
            g.Add(11);
            g.Add(13);
            g.Add(17);
            g.Add(5);
            g.Add(2);
            Console.WriteLine(g);
            Console.WriteLine(h + g);
            Console.WriteLine(h * g);
            Console.WriteLine(h - g);
            Console.WriteLine(g - h);
        }
    }
}
