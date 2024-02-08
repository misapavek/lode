using System.ComponentModel;
using System.ComponentModel.Design;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Transactions;

namespace proejtk
{
    internal class Program
    {
        public static void ClearLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop);
        }
        static void Vypis(int[,] pole1, bool cheat)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("  |A |B |C |D |E |F |G |H |I |J");
            Console.ForegroundColor = ConsoleColor.White;
            for (int r = 0; r < pole1.GetLength(0); r++)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                if (r + 1 == 10)
                {
                    Console.Write(r + 1 + "|");
                }
                else
                {
                    Console.Write(r + 1 + " |");
                }
                Console.ForegroundColor = ConsoleColor.White;
                for (int s = 0; s < pole1.GetLength(1); s++)
                {

                    if (pole1[r, s] == 8)
                    {
                        Console.Write("💦 ");
                    }
                    else if (pole1[r, s] == 2)
                    {
                        Console.Write("🤯 ");
                    }
                    else if (pole1[r, s] == 0)
                    {
                        Console.Write("🌊 ");
                    }
                    else if (pole1[r, s] == 1 && cheat)
                    {
                        Console.Write("🚣 ");
                    }
                    else if (pole1[r, s] == 1)
                    {
                        Console.Write("🌊 ");
                    }
                    else if (pole1[r,s] == 3)
                    {
                        Console.Write("💀 ");
                    }
                }
                Console.WriteLine();

            }
            Console.WriteLine("Napiš pozici pro výstřel (např. A,2)");
        }
        static void Main(string[] args)
        {
            
            
            int[,] pole = new int[10, 10];
            bool opakovani = true;
            int[,] lode1 = new int[10, 10];
            int[,] lode2 = new int[10, 10];
            int[,] lode3 = new int[10, 10];
            Console.OutputEncoding = Encoding.UTF8;

            while(opakovani)
            {
                cheating = false;
                Napln(pole, lode1, lode2, lode3);
                Vypis(pole, cheating);
                int pokusy = 0;
                bool game = true;

                while (game)
                {
                    
                    pole = Shoot(pole, lode1, lode2, lode3);
                    
                    pokusy++;
                    Console.Clear();
                    CheckLode(pole, lode1, lode2, lode3);
                    Vypis(pole, cheating);
                    game = Check(pole);
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Podařilo se vám potopit všechny lodě!🗣️🗣️🗣️");
                Console.ForegroundColor = ConsoleColor.White;

                for (int x = 0; x < 5; x++)
                {

                    for (int i = 0; i < 7; i++)
                    {
                        Console.Write("Zabralo vám to ");
                        switch (i)
                        {
                            case 0:
                                Console.ForegroundColor = ConsoleColor.Red;
                                break;
                            case 1:
                                Console.ForegroundColor = ConsoleColor.Green;
                                break;
                            case 2:
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                break;
                            case 3:
                                Console.ForegroundColor = ConsoleColor.Blue;
                                break;
                            case 4:
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                break;
                            case 5:
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                break;
                            case 6:
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                break;

                        }
                        Console.Write(pokusy);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" střel");
                        Thread.Sleep(100);
                        ClearLine();
                    }
                }

                Console.WriteLine("Zabralo vám to " + pokusy + " střel");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Chcete hrát znovu❓❓ ('y' pro opakování)");
                Console.ForegroundColor = ConsoleColor.White;
                char vstup = Console.ReadKey().KeyChar;
                if (vstup.ToString().ToLower() ==  "y")
                {
                    Console.Clear();
                }
                else
                {
                    opakovani = false;
                }
            }
            
            
            




        }
        static int[,] Shoot(int[,] pole1, int[,] lode1, int[,] lode2, int[,] lode3)
        {
            string shot = "";
            int radek = 0;
            int sloupec = ' ';
            while (true)
            {
                string message = "Špatně zadané souřadnice.";
                shot = Console.ReadLine();
                if (shot.Contains(","))
                {

                    string[] idk = shot.Split(",");
                    idk[0] = idk[0].ToUpper();
                    sloupec = ConvertChar(idk[0]);
                    if (int.TryParse((idk[1]), out radek))
                    {
                        if (radek > 0 && radek <= 10 && 0 < sloupec && sloupec <= 10)
                        {
                            if (pole1[radek - 1, sloupec - 1] == 8 || pole1[radek - 1, sloupec - 1] == 2)
                            {
                                Console.Clear();
                                Vypis(pole1, cheating);
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Na tuto pozici už bylo vystřeleno");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            else
                            {
                                
                                break;
                            }

                        }
                        else if (radek == 100 && sloupec == 100)
                        {
                            if (cheating)
                            {
                                cheating = false;

                                Console.Clear();
                                Vypis(pole1, cheating);
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("📡 cheating OFF 🤫");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            else
                            {
                                cheating = true;

                                Console.Clear();
                                Vypis(pole1, cheating);
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("📡 cheating ON 😈");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            
                            
                        }

                        else
                        {
                            Console.Clear();
                            Vypis(pole1, cheating);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(message);
                            Console.ForegroundColor = ConsoleColor.White;

                        }
                    }
                    else
                    {
                        Console.Clear();
                        Vypis(pole1, cheating);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(message);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                else
                {
                    Console.Clear();
                    Vypis(pole1, cheating);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(message);
                    Console.ForegroundColor = ConsoleColor.White;
                }

            }

            int[,] newpole = pole1;
            for (int r = 0; r < newpole.GetLength(0); r++)
            {
                for (int s = 0; s < newpole.GetLength(1); s++)
                {
                    if (r == radek - 1 && s == sloupec - 1)
                    {
                        if (pole1[r, s] == 1)
                        {
                            newpole[r, s] = 2;
                            
                        }
                        else if (pole1[r, s] != 2)
                        {
                            newpole[r, s] = 8;
                        }

                        if (lode1[r,s] == 1)
                        {
                            lode1[r,s] = 2;
                        }

                        if (lode2[r, s] == 1)
                        {
                            lode2[r, s] = 2;
                        }

                        if (lode3[r, s] == 1)
                        {
                            lode3[r, s] = 2;
                        }

                    }
                }
            }




            return newpole;
        }

        static int ConvertChar(string sloupec1)
        {
            int sloupec = 10000;
            switch (sloupec1)
            {
                case "A":
                    sloupec = 1;
                    break;
                case "B":
                    sloupec = 2;
                    break;
                case "C":
                    sloupec = 3;
                    break;
                case "D":
                    sloupec = 4;
                    break;
                case "E":
                    sloupec = 5;
                    break;
                case "F":
                    sloupec = 6;
                    break;
                case "G":
                    sloupec = 7;
                    break;
                case "H":
                    sloupec = 8;
                    break;
                case "I":
                    sloupec = 9;
                    break;
                case "J":
                    sloupec = 10;
                    break;
                case "Z":
                    sloupec = 100;
                    break;
                default:
                    sloupec = 999;
                    break;


            }
            return sloupec;
        }
        static void Napln(int[,] pole1, int[,] lode1, int[,] lode2, int[,] lode3)
        {
            bool lod1 = false;
            bool lod2 = false;
            bool lod3 = false;

            for (int r = 0; r < lode1.GetLength(0); r++)
            {
                for (int s = 0; s < lode1.GetLength(1); s++)
                {
                    lode1[r, s] = 0;
                    lode2[r, s] = 0;
                    lode3[r, s] = 0;
                }
            }

            Random rndm = new Random();
            while (!lod1)
            {
                for (int r = 0; r < pole1.GetLength(0); r++)
                {
                    for (int s = 0; s < pole1.GetLength(1); s++)
                    {


                        if (rndm.Next(1, 76) == 50 && s + 4 <= pole1.GetLength(1) && !lod1)
                        {
                            pole1[r, s] = 1;
                            pole1[r, (s + 1)] = 1;
                            pole1[r, (s + 2)] = 1;
                            pole1[r, (s + 3)] = 1;


                            lode1[r, s] = 1;
                            lode1[r, s + 1] = 1;
                            lode1[r, s + 2] = 1;
                            lode1[r, s + 3] = 1;
                            lod1 = true;
                            s += 4;
                        }

                        else
                        {
                            pole1[r, s] = 0;
                        }

                    }


                }
            }

            while (!lod3)
            {
                for (int r = 0; r < pole1.GetLength(0); r++)
                {
                    for (int s = 0; s < pole1.GetLength(1) -1; s++)
                    {


                        if (rndm.Next(1, 76) == 30 && r + 3 <= pole1.GetLength(0) && s + 1 <= pole1.GetLength(1) && !lod3)
                        {
                            if (r - 1 < 0)
                            {
                                if (s + 2 > 9) //pravy horni roh
                                {
                                    if (pole1[r, s] != 1 && pole1[r + 1, s] != 1 && pole1[r + 2, s] != 1 && pole1[r + 3, s] != 1)  //check 
                                    {
                                        if (pole1[r, s + 1] != 1 && pole1[r + 1, s + 1] != 1 && pole1[r + 2, s + 1] != 1 && pole1[r, s - 1] != 1 && pole1[r + 1, s - 1] != 1 && pole1[r + 2, s - 1] != 1 && pole1[r + 3, s - 1] != 1 && pole1[r + 3, s + 1] != 1)//check sloupcu
                                        {
                                            pole1[r, s] = 1;
                                            pole1[r + 1, s] = 1;
                                            pole1[r + 2, s] = 1;
                                            pole1[r + 1, s + 1] = 1;


                                            lode3[r, s] = 1;
                                            lode3[r + 1, s] = 1;
                                            lode3[r + 2, s] = 1;
                                            lode3[r + 1, s + 1] = 1;
                                            lod3 = true;
                                        }
                                    }
                                }

                                else if (s - 1 < 0) // levy horni roh
                                {
                                    if (pole1[r, s] != 1 && pole1[r + 1, s] != 1 && pole1[r + 2, s] != 1 && pole1[r + 3, s] != 1)  //check radku
                                    {
                                        if (pole1[r, s + 1] != 1 && pole1[r + 1, s + 1] != 1 && pole1[r + 2, s + 1] != 1 && pole1[r + 1, s + 2] != 1 && pole1[r, s + 2] != 1 && pole1[r + 2, s + 2] != 1 && pole1[r + 3, s + 1] != 1) //check sloupcu
                                        {
                                            pole1[r, s] = 1;
                                            pole1[r + 1, s] = 1;
                                            pole1[r + 2, s] = 1;
                                            pole1[r + 1, s + 1] = 1;


                                            lode3[r, s] = 1;
                                            lode3[r + 1, s] = 1;
                                            lode3[r + 2, s] = 1;
                                            lode3[r + 1, s + 1] = 1;
                                            lod3 = true;
                                        }
                                    }
                                }

                                else //horni
                                {
                                    if (pole1[r, s] != 1 && pole1[r + 1, s] != 1 && pole1[r + 2, s] != 1 && pole1[r + 3, s] != 1)  //check radku
                                    {
                                        if (pole1[r, s + 1] != 1 && pole1[r + 1, s + 1] != 1 && pole1[r + 2, s + 1] != 1 && pole1[r + 3, s + 1] != 1 && pole1[r + 1, s + 2] != 1 && pole1[r, s - 1] != 1 && pole1[r + 1, s - 1] != 1 && pole1[r + 2, s - 1] != 1 && pole1[r + 3, s - 1] != 1 && pole1[r, s + 2] != 1 && pole1[r + 2, s + 2] != 1) //check sloupcu
                                        {
                                            pole1[r, s] = 1;
                                            pole1[r + 1, s] = 1;
                                            pole1[r + 2, s] = 1;
                                            pole1[r + 1, s + 1] = 1;


                                            lode3[r, s] = 1;
                                            lode3[r + 1, s] = 1;
                                            lode3[r + 2, s] = 1;
                                            lode3[r + 1, s + 1] = 1;
                                            lod3 = true;
                                        }
                                    }
                                }

                            }
                            else if (r + 3 > 9)
                            {
                                if (s + 2 > 9) //pravy dolni roh
                                {
                                    if (pole1[r, s] != 1 && pole1[r + 1, s] != 1 && pole1[r + 2, s] != 1 && pole1[r - 1, s] != 1)  //check radku
                                    {
                                        if (pole1[r, s - 1] != 1 && pole1[r + 1, s - 1] != 1 && pole1[r + 2, s - 1] != 1 && pole1[r, s + 1] != 1 && pole1[r + 1, s + 1] != 1 && pole1[r + 2, s + 1] != 1) //check sloupcu
                                        {
                                            pole1[r, s] = 1;
                                            pole1[r + 1, s] = 1;
                                            pole1[r + 2, s] = 1;
                                            pole1[r + 1, s + 1] = 1;

                                            lode3[r, s] = 1;
                                            lode3[r + 1, s] = 1;
                                            lode3[r + 2, s] = 1;
                                            lode3[r + 1, s + 1] = 1;
                                            lod3 = true;
                                        }
                                    }
                                }
                                else if (s - 1 < 0) //levy dolni roh
                                {
                                    if (pole1[r, s] != 1 && pole1[r + 1, s] != 1 && pole1[r + 2, s] != 1 && pole1[r - 1, s] != 1)  //check radku
                                    {
                                        if (pole1[r, s + 1] != 1 && pole1[r + 1, s + 1] != 1 && pole1[r + 2, s + 1] != 1 && pole1[r + 1, s + 2] != 1 && pole1[r + 2, s + 2] != 1 && pole1[r, s + 2] != 1) //check sloupcu
                                        {
                                            pole1[r, s] = 1;
                                            pole1[r + 1, s] = 1;
                                            pole1[r + 2, s] = 1;
                                            pole1[r + 1, s + 1] = 1;


                                            lode3[r, s] = 1;
                                            lode3[r + 1, s] = 1;
                                            lode3[r + 2, s] = 1;
                                            lode3[r + 1, s + 1] = 1;
                                            lod3 = true;
                                        }
                                    }
                                }
                                else //dolni
                                {
                                    if (pole1[r, s] != 1 && pole1[r + 1, s] != 1 && pole1[r + 2, s] != 1 && pole1[r - 1, s] != 1)  //check radku
                                    {
                                        if (pole1[r, s + 1] != 1 && pole1[r + 1, s + 1] != 1 && pole1[r + 2, s + 1] != 1 && pole1[r, s - 1] != 1 && pole1[r + 1, s - 1] != 1 && pole1[r + 2, s - 1] != 1 && pole1[r + 1, s + 2] != 1 && pole1[r - 1, s - 1] != 1 && pole1[r - 1, s + 1] != 1 && pole1[r, s + 2] != 1 && pole1[r + 2, s + 2] != 1) //check sloupcu
                                        {
                                            pole1[r, s] = 1;
                                            pole1[r + 1, s] = 1;
                                            pole1[r + 2, s] = 1;
                                            pole1[r + 1, s + 1] = 1;


                                            lode3[r, s] = 1;
                                            lode3[r + 1, s] = 1;
                                            lode3[r + 2, s] = 1;
                                            lode3[r + 1, s + 1] = 1;
                                            lod3 = true;
                                        }
                                    }
                                }
                            }
                            else if (r - 1 >= 0 && r + 3 <= 9 && s - 1 < 0) //levy
                            {
                                if (pole1[r, s] != 1 && pole1[r + 1, s] != 1 && pole1[r + 2, s] != 1 && pole1[r + 3, s] != 1 && pole1[r - 1, s] != 1)
                                {
                                    if (pole1[r, s + 1] != 1 && pole1[r + 1, s + 1] != 1 && pole1[r + 2, s + 1] != 1 && pole1[r - 1, s + 1] != 1 && pole1[r + 3, s + 1] != 1)
                                    {
                                        if (pole1[r, s + 2] != 1 && pole1[r + 1, s + 2] != 1 && pole1[r + 2, s + 2] != 1)
                                        {
                                            pole1[r, s] = 1;
                                            pole1[r + 1, s] = 1;
                                            pole1[r + 2, s] = 1;
                                            pole1[r + 1, s + 1] = 1;


                                            lode3[r, s] = 1;
                                            lode3[r + 1, s] = 1;
                                            lode3[r + 2, s] = 1;
                                            lode3[r + 1, s + 1] = 1;
                                            lod3 = true;
                                        }
                                    }
                                }
                            }

                            else if (r - 1 >= 0 && r + 3 <= 9 && s + 2 > 9) //pravy
                            {
                                if (pole1[r, s] != 1 && pole1[r + 1, s] != 1 && pole1[r + 2, s] != 1 && pole1[r + 3, s] != 1 && pole1[r - 1, s] != 1)
                                {
                                    if (pole1[r, s - 1] != 1 && pole1[r + 1, s - 1] != 1 && pole1[r + 2, s - 1] != 1 && pole1[r - 1, s - 1] != 1 && pole1[r + 3, s - 1] != 1)
                                    {
                                        if (pole1[r, s + 1] != 1 && pole1[r + 1, s + 1] != 1 && pole1[r + 2, s + 1] != 1 && pole1[r - 1, s + 1] != 1 && pole1[r + 3, s + 1] != 1)
                                        {
                                            pole1[r, s] = 1;
                                            pole1[r + 1, s] = 1;
                                            pole1[r + 2, s] = 1;
                                            pole1[r + 1, s + 1] = 1;


                                            lode3[r, s] = 1;
                                            lode3[r + 1, s] = 1;
                                            lode3[r + 2, s] = 1;
                                            lode3[r + 1, s + 1] = 1;
                                            lod3 = true;
                                        }
                                    }
                                }
                            }

                            else if (r - 1 > 0 && r + 3 <= 9 && s - 1 >= 0 && s + 2 <= 9)
                            {
                                if (pole1[r, s] != 1 && pole1[r + 1, s] != 1 && pole1[r + 2, s] != 1 && pole1[r + 3, s] != 1 && pole1[r - 1, s] != 1)  //check radku
                                {
                                    if (pole1[r, s + 1] != 1 && pole1[r + 1, s + 1] != 1 && pole1[r + 2, s + 1] != 1 && pole1[r + 3, s + 1] != 1 && pole1[r - 1, s + 1] != 1 && pole1[r, s - 1] != 1 && pole1[r + 1, s - 1] != 1 && pole1[r + 2, s - 1] != 1 && pole1[r + 3, s - 1] != 1 && pole1[r - 1, s + 1] != 1 && pole1[r + 1, s + 2] != 1 && pole1[r, s + 2] != 1 && pole1[r + 2, s + 2] != 1) //check sloupcu
                                    {
                                        pole1[r, s] = 1;
                                        pole1[r + 1, s] = 1;
                                        pole1[r + 2, s] = 1;
                                        pole1[r + 1, s + 1] = 1;


                                        lode3[r, s] = 1;
                                        lode3[r + 1, s] = 1;
                                        lode3[r + 2, s] = 1;
                                        lode3[r + 1, s + 1] = 1;
                                        lod3 = true;
                                    }
                                }
                            }
                        }

                    }


                }
            }

            while (!lod2)
            {
                for (int r = 0; r < pole1.GetLength(0); r++)
                {
                    for (int s = 0; s < pole1.GetLength(1); s++)
                    {


                        if (rndm.Next(1, 76) == 50 && r + 4 <= pole1.GetLength(0) && !lod2)
                        {
                            if (r - 1 < 0)
                            {
                                if (s + 1 > 9) //pravy horni roh
                                {
                                    if (pole1[r, s] != 1 && pole1[r + 1, s] != 1 && pole1[r + 2, s] != 1 && pole1[r + 3, s] != 1 && pole1[r + 4, s] != 1)  //check radku
                                    {
                                        if (pole1[r, s - 1] != 1 && pole1[r + 1, s - 1] != 1 && pole1[r + 2, s - 1] != 1 && pole1[r + 3, s - 1] != 1 && pole1[r + 4, s - 1] != 1) //check sloupcu
                                        {
                                            pole1[r, s] = 1;
                                            pole1[r + 1, s] = 1;
                                            pole1[r + 2, s] = 1;
                                            pole1[r + 3, s] = 1;


                                            lode2[r, s] = 1;
                                            lode2[r + 1, s] = 1;
                                            lode2[r + 2, s] = 1;
                                            lode2[r + 3, s] = 1;
                                            lod2 = true;
                                        }
                                    }
                                }

                                else if (s - 1 < 0) // levy horni roh
                                {
                                    if (pole1[r, s] != 1 && pole1[r + 1, s] != 1 && pole1[r + 2, s] != 1 && pole1[r + 3, s] != 1 && pole1[r + 4, s] != 1)  //check radku
                                    {
                                        if (pole1[r, s + 1] != 1 && pole1[r + 1, s + 1] != 1 && pole1[r + 2, s + 1] != 1 && pole1[r + 3, s + 1] != 1 && pole1[r + 4, s + 1] != 1) //check sloupcu
                                        {
                                            pole1[r, s] = 1;
                                            pole1[r + 1, s] = 1;
                                            pole1[r + 2, s] = 1;
                                            pole1[r + 3, s] = 1;


                                            lode2[r, s] = 1;
                                            lode2[r + 1, s] = 1;
                                            lode2[r + 2, s] = 1;
                                            lode2[r + 3, s] = 1;
                                            lod2 = true;
                                        }
                                    }
                                }

                                else //horni
                                {
                                    if (pole1[r, s] != 1 && pole1[r + 1, s] != 1 && pole1[r + 2, s] != 1 && pole1[r + 3, s] != 1 && pole1[r + 4, s] != 1)  //check radku
                                    {
                                        if (pole1[r, s + 1] != 1 && pole1[r + 1, s + 1] != 1 && pole1[r + 2, s + 1] != 1 && pole1[r + 3, s + 1] != 1 && pole1[r + 4, s + 1] != 1 && pole1[r, s - 1] != 1 && pole1[r + 1, s - 1] != 1 && pole1[r + 2, s - 1] != 1 && pole1[r + 3, s - 1] != 1 && pole1[r + 4, s - 1] != 1) //check sloupcu
                                        {
                                            pole1[r, s] = 1;
                                            pole1[r + 1, s] = 1;
                                            pole1[r + 2, s] = 1;
                                            pole1[r + 3, s] = 1;


                                            lode2[r, s] = 1;
                                            lode2[r + 1, s] = 1;
                                            lode2[r + 2, s] = 1;
                                            lode2[r + 3, s] = 1;
                                            lod2 = true;
                                        }
                                    }
                                }

                            }
                            else if (r + 4 > 9)
                            {
                                if (s + 1 > 9) //pravy dolni roh
                                {
                                    if (pole1[r, s] != 1 && pole1[r + 1, s] != 1 && pole1[r + 2, s] != 1 && pole1[r + 3, s] != 1 && pole1[r - 1, s] != 1)  //check radku
                                    {
                                        if (pole1[r, s - 1] != 1 && pole1[r + 1, s - 1] != 1 && pole1[r + 2, s - 1] != 1 && pole1[r + 3, s - 1] != 1 && pole1[r - 1, s - 1] != 1) //check sloupcu
                                        {
                                            pole1[r, s] = 1;
                                            pole1[r + 1, s] = 1;
                                            pole1[r + 2, s] = 1;
                                            pole1[r + 3, s] = 1;


                                            lode2[r, s] = 1;
                                            lode2[r + 1, s] = 1;
                                            lode2[r + 2, s] = 1;
                                            lode2[r + 3, s] = 1;
                                            lod2 = true;
                                        }
                                    }
                                }
                                else if (s - 1 < 0) //levy dolni roh
                                {
                                    if (pole1[r, s] != 1 && pole1[r + 1, s] != 1 && pole1[r + 2, s] != 1 && pole1[r + 3, s] != 1 && pole1[r - 1, s] != 1)  //check radku
                                    {
                                        if (pole1[r, s + 1] != 1 && pole1[r + 1, s + 1] != 1 && pole1[r + 2, s + 1] != 1 && pole1[r + 3, s + 1] != 1 && pole1[r - 1, s + 1] != 1) //check sloupcu
                                        {
                                            pole1[r, s] = 1;
                                            pole1[r + 1, s] = 1;
                                            pole1[r + 2, s] = 1;
                                            pole1[r + 3, s] = 1;


                                            lode2[r, s] = 1;
                                            lode2[r + 1, s] = 1;
                                            lode2[r + 2, s] = 1;
                                            lode2[r + 3, s] = 1;
                                            lod2 = true;
                                        }
                                    }
                                }
                                else //dolni
                                {
                                    if (pole1[r, s] != 1 && pole1[r + 1, s] != 1 && pole1[r + 2, s] != 1 && pole1[r + 3, s] != 1 && pole1[r - 1, s] != 1)  //check radku
                                    {
                                        if (pole1[r, s + 1] != 1 && pole1[r + 1, s + 1] != 1 && pole1[r + 2, s + 1] != 1 && pole1[r + 3, s + 1] != 1 && pole1[r - 1, s + 1] != 1 && pole1[r, s - 1] != 1 && pole1[r + 1, s - 1] != 1 && pole1[r + 2, s - 1] != 1 && pole1[r + 3, s - 1] != 1 && pole1[r - 1, s - 1] != 1) //check sloupcu
                                        {
                                            pole1[r, s] = 1;
                                            pole1[r + 1, s] = 1;
                                            pole1[r + 2, s] = 1;
                                            pole1[r + 3, s] = 1;


                                            lode2[r, s] = 1;
                                            lode2[r + 1, s] = 1;
                                            lode2[r + 2, s] = 1;
                                            lode2[r + 3, s] = 1;
                                            lod2 = true;
                                        }
                                    }
                                }
                            }

                            else if (r - 1 >= 0 && r + 4 <= 9 && s - 1 < 0) //levy
                            {
                                if (pole1[r, s] != 1 && pole1[r + 1, s] != 1 && pole1[r + 2, s] != 1 && pole1[r + 3, s] != 1 && pole1[r - 1, s] != 1 && pole1[r+ 4, s] != 1)
                                {
                                    if (pole1[r, s + 1] != 1 && pole1[r + 1, s + 1] != 1 && pole1[r + 2, s + 1] != 1 && pole1[r - 1, s + 1] != 1 && pole1[r + 3, s + 1] != 1 && pole1[r + 4, s+ 1] != 1)
                                    {
                                        pole1[r, s] = 1;
                                        pole1[r + 1, s] = 1;
                                        pole1[r + 2, s] = 1;
                                        pole1[r + 3, s] = 1;


                                        lode2[r, s] = 1;
                                        lode2[r + 1, s] = 1;
                                        lode2[r + 2, s] = 1;
                                        lode2[r + 3, s] = 1;
                                        lod2 = true;
                                    }
                                }
                            }

                            else if (r - 1 >= 0 && r + 4 <= 9 && s + 1 > 9) //pravy
                            {
                                if (pole1[r, s] != 1 && pole1[r + 1, s] != 1 && pole1[r + 2, s] != 1 && pole1[r + 3, s] != 1 && pole1[r - 1, s] != 1 && pole1[r + 4, s] != 1)
                                {
                                    if (pole1[r, s - 1] != 1 && pole1[r + 1, s - 1] != 1 && pole1[r + 2, s - 1] != 1 && pole1[r - 1, s - 1] != 1 && pole1[r + 3, s - 1] != 1 && pole1[r + 4, s - 1] !=1)
                                    {
                                        pole1[r, s] = 1;
                                        pole1[r + 1, s] = 1;
                                        pole1[r + 2, s] = 1;
                                        pole1[r + 3, s] = 1;


                                        lode2[r, s] = 1;
                                        lode2[r + 1, s] = 1;
                                        lode2[r + 2, s] = 1;
                                        lode2[r + 3, s] = 1;
                                        lod2 = true;
                                    }
                                }
                            }
                            else if (r - 1 >= 0 && r + 4 <= 9 && s - 1 >= 0 && s + 1 <= 9)
                            {
                                if (pole1[r, s] != 1 && pole1[r + 1, s] != 1 && pole1[r + 2, s] != 1 && pole1[r + 3, s] != 1 && pole1[r + 4, s] != 1 && pole1[r - 1, s] != 1)  //check radku
                                {
                                    if (pole1[r, s + 1] != 1 && pole1[r + 1, s + 1] != 1 && pole1[r + 2, s + 1] != 1 && pole1[r + 3, s + 1] != 1 && pole1[r + 4, s + 1] != 1 && pole1[r - 1, s + 1] != 1 && pole1[r, s - 1] != 1 && pole1[r + 1, s - 1] != 1 && pole1[r + 2, s - 1] != 1 && pole1[r + 3, s - 1] != 1 && pole1[r + 4, s - 1] != 1 && pole1[r - 1, s + 1] != 1) //check sloupcu
                                    {
                                        pole1[r, s] = 1;
                                        pole1[r + 1, s] = 1;
                                        pole1[r + 2, s] = 1;
                                        pole1[r + 3, s] = 1;


                                        lode2[r, s] = 1;
                                        lode2[r + 1, s] = 1;
                                        lode2[r + 2, s] = 1;
                                        lode2[r + 3, s] = 1;
                                        lod2 = true;
                                    }
                                }
                            }
                        }

                    }


                }
            }

            

        }

        static bool Check(int[,] pole1) //konec hry
        {
            bool gaming = true;
            bool once = true;
            for (int r = 0; r < pole1.GetLength(0); r++)
            {
                for (int s = 0; s < pole1.GetLength(1); s++)
                {

                    if (pole1[r, s] == 1)
                    {
                        gaming = true;
                        once = false;
                    }
                    else if (once)
                    {
                        gaming = false;
                    }


                }
            }

            return gaming;
        }

        static void CheckLode(int[,] pole, int[,] lode1, int[,] lode2, int[,] lode3) //💀 
        {
            bool zije1 = false;
            bool zije2 = false;
            bool zije3 = false;
            
            for (int r = 0; r < lode1.GetLength(0); r++)
            {
                for (int s = 0; s < lode1.GetLength(1); s++)
                {
                    if (lode1[r, s] == 1)
                    {
                        zije1 = true;
                    }
                    if (lode2[r, s] == 1)
                    {
                        zije2 = true;
                    }
                    if (lode3[r, s] == 1)
                    {
                        zije3 = true;
                    }
                }
            }

            for (int r = 0; r < lode1.GetLength(0); r++)
            {
                for (int s = 0; s < lode1.GetLength(1); s++)
                {
                    if (!zije1)
                    {
                        if (lode1[r,s] == 2)
                        {
                            pole[r, s] = 3;
                        }
                    }
                    if (!zije2)
                    {
                        if (lode2[r, s] == 2)
                        {
                            pole[r, s] = 3;
                        }
                    }
                    if (!zije3)
                    {
                        if (lode3[r, s] == 2)
                        {
                            pole[r, s] = 3;
                        }
                    }
                }
            }

        }
        static bool cheating = false;
        
        

    }
}