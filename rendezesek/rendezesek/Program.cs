using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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

        static void Csere<T>(List<T> lista, int i, int j)
        {
            T temp = lista[i];
            lista[i] = lista[j];
            lista[j] = temp;
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

        static int Helye_balra<T>(List<T> lista, int i, Func<T, T, int> comparator)
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
                        Csere(lista, i, i - 1);
                }
            }
        }
        #endregion

        #region Buborékos rendezés
        static void Buborekos_rendezes<T>(List<T> lista, Func<T, T, int> comparator)
        {
            for (int meddig = lista.Count; 0 < meddig; meddig--)
            {
                for (int i = 1; i < meddig - 1; i++)
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

        #region Összefésüléses rendezés
        static void MergeSort<T>(List<T> lista, Func<T, T, int> c) => MergeSort<T>(lista, 0, lista.Count - 1, new T[lista.Count], c);

        static void MergeSort<T>(List<T> lista, int e, int v, T[] temp, Func<T, T, int> c)
        {
            if (e < v)
            {
                int k = (e + v) / 2;
                MergeSort(lista, e, k, temp, c);
                MergeSort(lista, k + 1, v, temp, c);
                Merge(lista, e, k, v, temp, c);
            }
        }

        static void Merge<T>(List<T> lista, int e, int k, int v, T[] temp, Func<T, T, int> c)
        {
            int i = e;
            int j = k + 1;
            int t = e;
            while (i <= k && j <= v)
            {
                int hasonlitas = c(lista[i], lista[j]);
                if (hasonlitas == -1)
                {
                    temp[t] = lista[i];
                    i++;
                    t++;
                }
                else if (hasonlitas == 1)
                {
                    temp[t++] = lista[j++];
                }
                else
                {
                    temp[t++] = lista[i++];
                    temp[t] = lista[j];
                    t++;
                    j++;
                }
            }
            while (i <= k)
            {
                temp[t] = lista[i];
                i++;
                t++;
            }
            while (j <= v)
            {
                temp[t++] = lista[j++];
            }
            for (int l = e; l <= v; l++) // visszamásol
            {
                lista[l] = temp[l];
            }
        }
        #endregion

        #region Tesztelés

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

        static void GagyiTeszt()
        {
            List<int> lista = new List<int> { 3, 0, 1, 8, 7, 2, 5, 4, 9, 6 };
            List<int> listb = new List<int> { 0, 3, 8, -8, 7, 5, 4, -9, 6, 75 };
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

        static void Teszt(int minertek, int maxertek, int minhossz, int maxhossz, int db, Action<List<int>, Func<int, int, int>> rendezes, bool show = false)
        {
            int win = 0;
            List<int>[] randomlisták = new List<int>[db];
            for (int i = 0; i < db; i++)
            {
                randomlisták[i] = RandomLista(minertek, maxertek, minhossz, maxhossz);
            }
            Stopwatch watch = Stopwatch.StartNew();
            for (int i = 0; i < db; i++)
            {
                rendezes(randomlisták[i], Szokasos_rendezes);
            }
            watch.Stop();
            for (int i = 0; i < db; i++)
            {
                if (Jo_e(randomlisták[i]))
                    win++;
            }

            Console.WriteLine($"A teszt eredménye: {win}/{db}, Runtime: {watch.ElapsedMilliseconds} milisec\n");
        }
        #endregion

        static void Main(string[] args)
        {
            Console.WriteLine("Minimumkiválasztásos rendezés:");
            Teszt(-5, 10, 1000, 1000, 1000, MinimumKiválasztásosRendezés);
            Console.WriteLine("Beszúró rendezés:");
            Teszt(-5, 10, 1000, 1000, 1000, Beszuro_rendezes);
            Console.WriteLine("Buborékos rendezés:");
            Teszt(-5, 10, 1000, 1000, 1000, Buborekos_rendezes);
            Console.WriteLine("Összefésüléses rendezés:");
            Teszt(-5, 10, 1000, 1000, 1000, MergeSort);
        }
    }
}
