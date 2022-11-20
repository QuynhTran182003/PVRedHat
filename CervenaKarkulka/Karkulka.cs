namespace CervenaKarkulka
{
    public class Karkulka
    {
        private int x;
        private int y;
        private int pocetDarku;

        //pocatecni setup
        private bool vyhrala = false;
        private bool prohrala = false; //gameover
        private bool sbiralaKvetiny = false;
        private bool sbiralaHouby = false;

        //get set
        public int X { get { return x; } set { x = value; } }
        public int Y { get { return y; } set { y = value; } }
        public int PocetDarku
        {
            get { return pocetDarku; }
            set
            {
                if (value < 0 || value > 2)
                {
                    throw new ArgumentException("Pocet darku nesmi byt mensi nez 0 aneb vetsi nez 2");
                }
                pocetDarku = value;
            }
        }
        public bool Vyhrala { get { return vyhrala; } set { vyhrala = value; } }
        public bool Prohrala { get { return prohrala; } set { prohrala = value; } }

        //konstruktor
        public Karkulka(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.pocetDarku = 2;
        }
        
        //pohyb
        public void MoveUp()
        {
            if (this.Y > 0)
            {
                this.Y -= 1;
                Console.WriteLine("Karkulka moved up");
            }
        }
        public void MoveDown(int vyska)
        {
            if (this.Y < vyska - 1)
            {
                this.Y += 1;
                Console.WriteLine("Karkulka moved down");
            }
        }
        public void MoveLeft()
        {
            if (this.X > 0)
            {
                this.X -= 1;
                Console.WriteLine("Karkulka moved left");
            }
        }
        public void MoveRight(int sirka)
        {
            if (this.X < sirka - 1)
            {
                this.X += 1;
                Console.WriteLine("Karkulka moved right");
            }
        }

        //dalsi metody
        public void DarovatDarek()
        {
            if(pocetDarku == 0)
            {
                Console.WriteLine("Nezbyva Karkulce nic k darovani");
                prohrala = true;
            }
            else
            {
                pocetDarku--;
            }
        }

        public void SbiratDarek(DruhPrekazky druhEnum)
        {
            if (this.pocetDarku < 2)
            {
                if (druhEnum == DruhPrekazky.LOUKA && !sbiralaKvetiny)
                {
                    pocetDarku++;
                    sbiralaKvetiny = true;
                    Console.WriteLine("karkulka nasbirala kvetiny");
                }
                else if (druhEnum == DruhPrekazky.PASEKHRIBKU && !sbiralaHouby)
                {
                    pocetDarku++;
                    sbiralaHouby = true;
                    Console.WriteLine("Karkulka nasbirala houby");
                }
                else
                {
                    if (sbiralaHouby)
                    {
                        Console.WriteLine("Nemuzes uz nasbirat houby, protoze jsi ji jednou nasbirala");
                    } else if (sbiralaKvetiny)
                    {
                        Console.WriteLine("Nemuzes uz nasbirat kvetiny, protoze jsi je jednou nasbirala");
                    } 
                }
            }
            else
            {
                Console.WriteLine("uz se nevejde zadny darek do kosiku");

            }
        }

        public void PotkatVlka()
        {
            if((!sbiralaHouby || !sbiralaKvetiny)&&this.pocetDarku<2)
            {
                Console.WriteLine("Potkala jsi vlka , ale musis jeste sbirat darek pro babicku");
                this.prohrala = true;
                Console.WriteLine("Prohral/a ses");
            }
        }

        public void KarkulkaProhrala()
        {
            this.Prohrala = true;
        }
        public void KarkulkaVyhrala()
        {
            this.Vyhrala = true;
        }
    }
}
