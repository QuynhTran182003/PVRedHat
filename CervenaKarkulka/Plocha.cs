using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CervenaKarkulka
{
    public class Plocha
    {
        private int sirka;
        private int vyska;
        private Prekazka[,] prekazka;
        private Random random = new Random();
        private bool jeDumKarkulky = false;
        private bool jeChaloupka = false;
        private Karkulka karkulka;
        private Prekazka babicka;
        public int Sirka
        {
            get { return sirka; }
            set
            {
                if (value < 4)
                {
                    throw new ArgumentException("Je sirky minimalne 4");
                }
                sirka = value;
            }
        }
        public int Vyska
        {
            get { return vyska; }
            set
            {
                if (value < 4)
                {
                    throw new ArgumentException("Je delky minimalne 4");
                }
                vyska = value;
            }
        }

        public Plocha(int sirka, int vyska)
        {
            this.Sirka = sirka;
            this.Vyska = vyska;
            this.VytvoritPlochu(sirka, vyska);
            this.ZacinatHru();

        }

        public void VytvoritPlochu(int x, int y)
        {
            prekazka = new Prekazka[y,x]; // vytvorit x*y poli v cele plose ... y je pocet row, x je pocet column
            
            //vytvorit nekde nahodne dum karkulky a zaroven karkulku 
            this.VytvoritDumKarkulky();

            //vytvorit chaloupka babicky
            this.VytvoritChaloupkaBabicky();

            //vytvorit nahodne prekazky v plose
            for(int i = 0; i < (int) x*y*80/100; i++)
            {
                this.VytvoritPrekazky();
            }

            Console.WriteLine("Plocha je vytvorena");
        }

        public override string? ToString()
        {
            string mapa = "";
            for(int i = 0; i < vyska; i++)
            {
                for(int j = 0; j < sirka; j++)
                {
                    if(i == karkulka.Y && j == karkulka.X)
                    {
                        mapa += " k ";
                    }
                    else
                    {
                        if (i == babicka.Y && j == babicka.X)
                        {
                            mapa += " b ";
                        }
                        else
                        {
                            mapa += " * ";
                        }

                    }
                }
                mapa += "\n";
            }
            return mapa;
        }

        public void ZacinatHru()
        {
            while (!karkulka.Vyhrala && !karkulka.Prohrala)
            {
                Console.WriteLine(this.ToString());
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.W:
                        karkulka.MoveUp();
                        break;
                    case ConsoleKey.A:
                        karkulka.MoveLeft();
                        break;
                    case ConsoleKey.S:
                        karkulka.MoveDown(this.Vyska);
                        break;
                    case ConsoleKey.D:
                        karkulka.MoveRight(this.Sirka);
                        break;
                }
                this.CheckPozice();
     /*           Console.Clear();*/

            }

            if (karkulka.Vyhrala)
            {
                Console.WriteLine("Huraaa... Vyhrala jsi");
            }
            else
                Console.WriteLine("Neee... Prohrala jsi");
            return;
        }


        public void VytvoritDumKarkulky()
        {
            if (!jeDumKarkulky) //nejprve zjistit, zda nebyla nastavena dum karkulky
            {
                int xp, yp;
                do // vymysli nejake jine pole 
                {
                    xp = random.Next(0, this.Sirka);
                    yp = random.Next(0, this.Vyska);
                    Console.WriteLine(yp);
                } while (prekazka[yp, xp] != null); // jestli v poli byla nastavena jina prekazka
                prekazka[yp, xp] = new Prekazka(yp, xp, 6);
                //Console.WriteLine("Dum je nastaven v poloze [" + xp +", " + yp + "]");
                jeDumKarkulky = true;
                karkulka = new Karkulka(xp, yp);
                //ConsoleWriteLine("Karkulka je nastaven v poloze [" + xp + ", " + yp + "]");
            }
            else
            {
                Console.WriteLine("Dum byl nastaven uz predtim");
            }
            return;
        }

        public void VytvoritChaloupkaBabicky()
        {
            if (!jeChaloupka) //nejprve zjistit, zda nebyla nastavena chaloupka
            {
                int xp, yp;

                do // vymysli nejake jine pole 
                {
                    xp = random.Next(0, this.Sirka);
                    yp = random.Next(0, this.Vyska);
                } while (prekazka[yp, xp] != null); // jestli v poli byla nastavena jina prekazka
                prekazka[yp, xp] = new Prekazka(xp, yp, 7);
                babicka = prekazka[yp, xp];
                jeChaloupka = true;
            }
            return;
        }

        public void VytvoritPrekazky()
        {
            int xp, yp;
            do //vymysli jeste dalsi souradnici, pokud v te souradnici byla nastavena nejaka prekazka
            {
                xp = random.Next(0, sirka);
                yp = random.Next(0, Vyska);
            } while (prekazka[yp, xp] != null);

            int id = random.Next(0, 6);
            prekazka[yp, xp] = new Prekazka(xp, yp, id);
            Console.WriteLine(prekazka[yp,xp].ToString() + " je nastaven v poloze [" + xp + ", " + yp + "]");

        }

        public void CheckPozice()
        {
            int karX = karkulka.X;
            int karY = karkulka.Y;
            if(prekazka[karY, karX] != null)
            {
                switch(prekazka[karY, karX].Nazev)
                {
                    case DruhPrekazky.POTOK:
                        this.HazetOtazku(karX, karY);
                        break;
                    case DruhPrekazky.BLUDNYKOREN:
                        int ranX = random.Next(0, sirka);
                        int ranY = random.Next(0, vyska);
                        karkulka.X = ranX;
                        karkulka.Y = ranY;
                        Console.WriteLine("nahodne ses premistila bludnym korenem");
                        if(babicka.Y == karkulka.Y && babicka.X == karkulka.X)
                        {
                            karkulka.KarkulkaVyhrala();
                        }
                        break;
                    case DruhPrekazky.PASEKHRIBKU:
                        karkulka.SbiratDarek(DruhPrekazky.PASEKHRIBKU);
                        break;
                    case DruhPrekazky.LOUKA:
                        karkulka.SbiratDarek(DruhPrekazky.LOUKA);
                        break;
                    case DruhPrekazky.VYHLIDKA:
                        Console.WriteLine("ses na vyhlidce, tak si uzivej");
                        break;
                    case DruhPrekazky.VLK:
                        karkulka.PotkatVlka();
                        break;
                    case DruhPrekazky.CHALOUPEK:
                        karkulka.KarkulkaVyhrala();
                        break;
                    default:
                        Console.WriteLine("neni znamo");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Nic tady neni");
            }

        }
        public void HazetOtazku(int x, int y)
        {
            //udelam pro zatim prave pouze jednu otazku
            if (prekazka[y, x] != null)
            {
                Console.WriteLine(prekazka[y, x].ToString() + " se ti nevyhnul :) Odpovez na otazku, a pak ti pousti");
                Console.WriteLine("Programátorská konstrukce try…catch se v C# používá: " +
                                    "\nA. na ukončení cyklu " +
                                    "\nB. na odchycení výjimky " +
                                    "\nC. v C# nic takového neexistuje " +
                                    "\nD. pro zavolání konstruktoru");
                
                string spravnaOdpoved = "B";

                string uzivatelOdpoved = Console.ReadLine();
                while(uzivatelOdpoved == null)
                {
                    uzivatelOdpoved = Console.ReadLine();
                    string? TryGetMessage(int id) => "";

                    string msg = TryGetMessage(42)!;  // Possible null assignment.
                }
                if(String.Compare(spravnaOdpoved, uzivatelOdpoved) == 0){
                    Console.WriteLine("Mas pravdu. Jdi dal");
                }
                else
                {
                    if(karkulka.PocetDarku == 0)
                    {
                        Console.WriteLine("Nezbyva ti zadny darek.");
                        karkulka.KarkulkaProhrala();
                    }
                    else
                    {
                        karkulka.PocetDarku--;
                        Console.WriteLine("Bohuzel, nemas pravdu. Musis dat svemu myslivci jeden z darku v kosiku, aby te zachranil");
                        Console.WriteLine("Zbyva ti " + karkulka.PocetDarku + " darek");
                    }
                }
            }
        }
    }
}
