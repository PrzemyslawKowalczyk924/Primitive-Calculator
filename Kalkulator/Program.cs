using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalkulator
{
    class Program
    {
        static void Main(string[] args)
        {
            double memorySave = 0;
            double memoryReload = 0;
            double memoryPlus = 0;
            double memoryMinus = 0;
            bool koniec = false;
            WriteLineColor("Kalkulator:", ConsoleColor.Yellow);
            while (!koniec)
            {
                double liczba1 = GetLiczba1(ref memorySave);
                string operation = PrzetwórzOperacje().ToString();
                double liczba2 = GetLiczba2(ref memorySave);
                double wynik = PodajWynik(liczba1, operation, liczba2);

                Console.WriteLine("Czy chcesz użyć operacji pamięciowych? TAK czy NIE");
                string tak = "tak";
                string nie = "nie";
                string wywolac = Console.ReadLine();
                if (wywolac == tak)
                {
                    MemorySwich(ref memorySave, ref memoryReload, ref memoryPlus, ref memoryMinus, wynik);
                }
                else 
                {
                    if (wywolac == nie)
                    {
                        Console.WriteLine("Anulowano");
                    }

                }

                WriteLineColor("Kalkulator:", ConsoleColor.Yellow);
            }
        }

        static void MemorySwich(ref double memorySave, ref double memoryReload, ref double memoryPlus, ref double memoryMinus, double wynik)
        {
            PamięćOperacyjna komendaOperacyjna = Memory();
            switch (komendaOperacyjna)
            {
                case PamięćOperacyjna.MS:
                    memorySave = wynik;
                    Console.WriteLine($"M. Zapisano w pamięci {memorySave}");
                    break;

                case PamięćOperacyjna.MR:
                    memoryReload = memorySave;
                    Console.WriteLine($"Reload: {memorySave}");
                    break;

                case PamięćOperacyjna.Mplus:
                    memorySave += wynik;
                    Console.WriteLine($"M+: {memorySave}");
                    break;

                case PamięćOperacyjna.Mminus:
                    memorySave -= wynik;
                    Console.WriteLine($"M-: {memorySave}");
                    break;

                case PamięćOperacyjna.MC:
                    memorySave = 0;
                    break;

                case PamięćOperacyjna.Koniec:
                    break;

                default:
                    throw new Exception("Error");
            }

        }

        static PamięćOperacyjna Memory()
        {
            string komenda;
            Console.WriteLine("Wybierz operację:");
            Console.WriteLine($"1. MS (Memory Save)");
            Console.WriteLine($"2. MR (Memory Reload)");
            Console.WriteLine($"3. M+ (Add Memory to current equal)");
            Console.WriteLine($"4. M- (Dicress Memory of current equal)");
            Console.WriteLine($"5. MC (Clear Memory)");
            Console.WriteLine($"6. Koniec");

            do
            {
                komenda = Console.ReadLine();
                if (!CzyKomendaPoprawna(komenda))
                {
                    Console.WriteLine("Komenda nie poprawna");
                }
            }
            while (!CzyKomendaPoprawna(komenda));
            return (PamięćOperacyjna)int.Parse(komenda);
        }

        private static bool CzyKomendaPoprawna(string komenda)
        {
            return komenda == "6" || komenda == "5" || komenda == "4" || komenda == "3" || komenda == "2" || komenda == "1";
        }

        private static double PodajWynik(double liczba1, string operation, double liczba2)
        {
            double wynik;
            Console.WriteLine("=");
            Operacje operacje = Cyknij(operation);
            switch (operacje)
            {
                case Operacje.dodawanie:
                    wynik = liczba1 + liczba2;
                    Console.WriteLine(wynik);
                    return wynik;
                case Operacje.odejmowanie:
                    wynik = liczba1 - liczba2;
                    Console.WriteLine(wynik);
                    return wynik;
                case Operacje.mnożenie:
                    wynik = liczba1 * liczba2;
                    Console.WriteLine(wynik);
                    return wynik;
                case Operacje.dzielenie:
                    wynik = liczba1 / liczba2;
                    Console.WriteLine(wynik);
                    return wynik;
                default:
                    throw new Exception("OMG!");
            }
        }

        private static Operacje Cyknij(string operation)
        {
            if (operation== "dodawanie")
            {
                return Operacje.dodawanie;
            }
            else
            {
                if (operation == "odejmowanie")
                {
                    return Operacje.odejmowanie;
                }
            }
            if (operation == "mnożenie")
            {
                return Operacje.mnożenie;
            }
            else
            {
                if (operation == "dzielenie")
                {
                    return Operacje.dzielenie;
                }
            }
            return (Operacje)double.Parse(operation);
        }

        private static void WriteLineColor(string tekst, ConsoleColor kolor)
        {
            Console.ForegroundColor = kolor;
            Console.WriteLine(tekst);
            Console.ResetColor();
        }

        private static double GetLiczba1(ref double memorySave)
        {
            string komenda1 = Console.ReadLine();
            while(komenda1 == "MR")
            {
                return memorySave;
            }
            bool PobranaLiczba = double.TryParse(komenda1, out _);
            while (PobranaLiczba == false)
            {
                WriteLineColor("Zły format liczby. Podaj jeszcze raz.", ConsoleColor.Red);
                komenda1 = Console.ReadLine();
                PobranaLiczba = double.TryParse(komenda1, out _);
            }
            return double.Parse(komenda1);
        }

        private static double GetLiczba2(ref double memorySave)
        {
            string komenda2 = Console.ReadLine();
            while (komenda2 == "MR")
            {
                return memorySave;
            }
            bool PobranaLiczba = double.TryParse(komenda2, out _);

            while (PobranaLiczba == false)
            {
                WriteLineColor("Zły format liczby. Podaj jeszcze raz.", ConsoleColor.Red);
                komenda2 = Console.ReadLine();
                PobranaLiczba = double.TryParse(komenda2, out _);
            }
            return double.Parse(komenda2);
        }

        private static Operacje PrzetwórzOperacje()
        {
            string komenda = Console.ReadLine();

            Operacje symbol = WywolajKomende(komenda);
            switch(symbol)
            {
                case Operacje.dodawanie:
                    break;
                case Operacje.odejmowanie:
                    break;
                case Operacje.mnożenie:
                    break;
                case Operacje.dzielenie:
                    break;
                default:
                    throw new Exception("Wywaliło mi błąd, aż strach!");
            }
            return symbol;
        }

        private static Operacje WywolajKomende(string komenda)
        {
            if (komenda == "+")
            {
                return Operacje.dodawanie;
            }
            else
            {
                if (komenda == "-")
                {
                    return Operacje.odejmowanie;
                }
            }
            if (komenda == "*")
            {
                return Operacje.mnożenie;
            }
            else
            {
                if (komenda == "/")
                {
                    return Operacje.dzielenie;
                }
            }
            return (Operacje)double.Parse(komenda);
        }
    }
}

enum Operacje
{
    dodawanie = 1,
    odejmowanie = 2,
    mnożenie = 3,
    dzielenie = 4
}

enum PamięćOperacyjna
{
    MS = 1,
    MR = 2,
    Mplus = 3,
    Mminus = 4,
    MC = 5,
    Koniec = 6
}