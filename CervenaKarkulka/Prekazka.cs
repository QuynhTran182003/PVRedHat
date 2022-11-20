using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CervenaKarkulka
{
    public enum DruhPrekazky
    {
        POTOK,
        VYHLIDKA,
        VLK,
        BLUDNYKOREN,
        LOUKA,
        PASEKHRIBKU,
        // pouze jednou
        DUMKARKULKY,
        CHALOUPEK
    }

    public class Prekazka
    {
        private int x;
        private int y;
        private DruhPrekazky nazev;
        
        //set get
        public int X
        {
            get { return x; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("");
                }
                x = value;
            }
        }
        public int Y
        {
            get { return y; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("");
                }
                y = value;
            }
        }
        public DruhPrekazky Nazev { get { return this.nazev; } }

        //odkaz na enum
        public void SetPrekazka(int random)
        {
            switch (random)
            {
                case 0:
                    this.nazev = DruhPrekazky.POTOK;
                    break;
                case 1:
                    this.nazev = DruhPrekazky.VYHLIDKA;
                    break;
                case 2:
                    this.nazev = DruhPrekazky.VLK;
                    break;
                case 3:
                    this.nazev = DruhPrekazky.BLUDNYKOREN;
                    break;
                case 4:
                    this.nazev = DruhPrekazky.LOUKA;
                    break;
                case 5:
                    this.nazev = DruhPrekazky.PASEKHRIBKU;
                    break;
                case 6:
                    this.nazev = DruhPrekazky.DUMKARKULKY;
                    break;
                case 7:
                    this.nazev = DruhPrekazky.CHALOUPEK;
                    break;
                default:
                    this.nazev = DruhPrekazky.VYHLIDKA;
                    break;
            }
        }

        //konstruktor
        public Prekazka(int x, int y, int random)
        {
            this.X = x;
            this.Y = y;
            SetPrekazka(random);
        }

        public override string? ToString()
        {
            return this.Nazev.ToString();
        }
    }

}
