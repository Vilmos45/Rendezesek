using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rendezesek
{
    internal class Program
    {
        static Random r = new Random();

        #region MinimumKiválasztás
        static void MinimumKiválasztásosRendezés<T>(List<T> lista, Func<T, T, int> comparator)//Selection sort
        {
            for (int i = 0; i < lista.Count - 1; i++)
                Csere(lista, Legkisebb_elem_helye_innentol(lista, i, comparator), i);
        }

        static int Legkisebb_elem_helye_innentol<T>(List<T> lista, int innentol, Func<T, T, int> comparator) //comparator(5,7) = -1
        {
            int mini = innentol;
            for (int i = innentol + 1; i < lista.Count; i++)
            {
                if (comparator(lista[i], lista[mini]) == -1)
                    mini = i;
            }
            return mini;
        }

        static void Csere<T> (List<T> lista, int i, int j)
        {
            T temp = lista[i];
            lista[i] = lista[j];
            lista[j] = temp;
        }
        #endregion  M

        #region Compratorok
        static int Szokasos_rendezes(int a, int b)
        {
            return a.CompareTo(b);
        }

        static int Szokasos_rendezes2(int a, int b)
        {
            if (a < b)
                return -1;
            if (a == b)
                return 0;
            return 1;
        }

        static int Forditott_rendezes(int a, int b)
        {
            if (a < b)
                return 1;
            if (a == b)
                return 0;
            return -1;
        }

        static int Abszolútérték_szertint(int a, int b)
        {
            return Math.Abs(a).CompareTo(Math.Abs(b));
        }

        static int Alfabetikus_rendezés(string a, string b)
        {
            return a.CompareTo(b);
        }

        static int Hossz_alapján(string a, string b)
        {
            return a.Length.CompareTo(b.Length);
        }

        #endregion

        #region BeszuroRendezes
        static void Beszuro_rendezes<T>(List<T> lista, Func<T, T, int> comparator) // Insertion sort
        {
            for (int i = 1; i < lista.Count; i++) //F11 debug, F9, F10
                Balra_süllyeszt(lista, i, comparator);
        }
        
        static void Balra_süllyeszt<T>(List<T> lista, int i, Func<T, T, int> comparator)
        {
            int h = Helye_balra(lista, i, comparator); //Shift + F = exit debug
            Átemelés(lista, i, h); //Ctrl + . = függvény
        }

        static void Átemelés<T>(List<T> lista, int i, int h)
        {
            T temp = lista[i];
            for (int j = i; h < j; j--)
                lista[j] = lista[j - 1];
            lista[h] = temp;
        }

        static int Helye_balra<T>(List<T> lista, int i, Func<T,T,int>comparator)
        {
            int h = i - 1;
            while (0 <= h && comparator(lista[h], lista[i]) == 1)//lista[h] > lista[i]
                h--;
            return h + 1;
        }

        #endregion

        #region Gagyi Buborékos rendezés
        static void Gagyi_Buborekos_rendezes<T>(List<T> lista, Func<T, T, int> comparator)
        {
            for (int meddig = lista.Count; 0 < meddig; meddig--)
            {
                for (int i = 1; i < meddig; i++)
                {
                    if (comparator(lista[i - 1], lista[i]) == 1)
                        Csere(lista, i, i-1);
                }
            }
        }
        #endregion

        #region Buborékos rendezés
        static void Buborekos_rendezes<T>(List<T> lista, Func<T, T, int> comparator)
        {
            for (int meddig = lista.Count; 0 < meddig; meddig--)
            {
                for (int i = 1; i < meddig-1; i++)
                {
                    if (comparator(lista[i - 1], lista[i]) == 1)
                        Csere(lista, i, i - 1);
                }
                if (1 < meddig && comparator(lista[meddig - 2], lista[meddig - 1]) == 1)
                    Csere(lista, meddig - 2, meddig - 1);
                else
                    meddig--;
            }
        }
        #endregion

        #region Tesztelés
        static void GagyiTeszt()
        {
            List<int> lista = new List<int> { 3, 0, 1, 8, 7, 2, 5, 4, 9, 6 };
            List<int> listb = new List<int> { 0, 3, 8, -8, 7, 5, 4, -9, 6, 75};
            List<string> lists = new List<string> { "alma", "körte", "barack", "szőlő", "gerinc", "faóra", "termesz", "palack", "malac" };
            Console.WriteLine("     " + string.Join(", ", lista));

            MinimumKiválasztásosRendezés(listb, Abszolútérték_szertint);
            //lista.Sort();
            //Console.WriteLine("     Sorrendeben van-e: " + Jo_e(lista));

            Console.WriteLine("     " + string.Join(", ", lista));
        }

        static bool Jo_e(List<int> lista)
        {
            if (lista.Count < 2)
                return true;
            int i = 1;
            while (i < lista.Count && lista[i - 1] <= lista[i])
                i++;
            return i == lista.Count;
        }

        static List<int> RandomLista(int minErtek, int maxErtek, int minHossz, int maxHossz)
        {
            int hossz = r.Next(minHossz, maxHossz + 1);
            List<int> lista = new List<int>(hossz);
            for (int i = 0; i < hossz; i++)
            {
                lista.Add(r.Next(minErtek, maxErtek + 1));
            }
            return lista;
        }

        static void Teszt(int minErtek, int maxErtek, int minHossz, int maxHossz, int db, Action<List<int>, Func<int, int, int>> rendezes)
        {
            int win = 0;
            for (int i = 0; i < db; i++)
            {
                List<int> lista = RandomLista(minErtek, maxErtek, minHossz, maxHossz);
                Console.WriteLine($"{i+1}. előtte: {string.Join(", ", lista)}");
                rendezes(lista, Szokasos_rendezes);
                Console.WriteLine($"   utána : {string.Join(", ", lista)}");
                if (Jo_e(lista))
                    win++;
            }
            Console.WriteLine();
            Console.WriteLine($"A teszt eredménye: {win}/{db}");
        }

        #endregion

        static void Main(string[] args)
        {
            Teszt(-15, 10, 100, 100, 1000, Buborekos_rendezes);
        
        }
    }
}
