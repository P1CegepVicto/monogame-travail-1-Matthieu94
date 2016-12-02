using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Question5
{
    class Program
    {
        static void Main(string[] args)
        {
            bool[] cases = new bool[104];
            string touche = "0";
            int position = 0;
            int casesFausses = 0;
            int nombreAleatoire;
            int nombreEssais = 0;
            Random de = new Random();

            for (int i = 0; i < cases.Length; i++)
            {
                nombreAleatoire = de.Next(0, 2);

                if (nombreAleatoire == 0)
                    cases[i] = false;
                else
                    cases[i] = true;
            }

            cases[0] = true;
            cases[99] = true;
            cases[100] = false;
            cases[101] = false;
            cases[102] = false;
            cases[103] = false;

            while (touche.ToUpper() != "Q")
            {
                Console.WriteLine("Position: " + (position + 1));
                Console.WriteLine("Nombre d'essais: " + nombreEssais);

                if (position == 99)
                {
                    Console.WriteLine("Vous avez réussi le niveau!");
                    Console.WriteLine("Entrez la touche Q pour quitter le jeu.");
                }

                for (int i = position; i <= position + 4; i++)
                {
                    if (cases[i] == false)
                        casesFausses++;
                }

                if (casesFausses == 4 && position != 99)
                    Console.WriteLine("Ce niveau est impossible à compléter.");

                casesFausses = 0;

                touche = Console.ReadLine();

                if (touche.ToUpper() == "A")
                {
                    position -= 3;
                    nombreEssais++;

                    if (position < 0)
                        position += 3;
                    else if (cases[position - 3] == false)
                        position += 3;
                }
                else if (touche.ToUpper() == "S")
                {
                    position -= 2;
                    nombreEssais++;

                    if (position < 0)
                        position += 2;
                    else if (cases[position - 2] == false)
                        position += 2;
                }
                else if (touche.ToUpper() == "D")
                {
                    position -= 1;
                    nombreEssais++;

                    if (position < 0)
                        position += 1;
                    else if (cases[position - 1] == false)
                        position += 1;
                }
                else if (touche.ToUpper() == "G")
                {
                    position += 2;
                    nombreEssais++;

                    if (position > 99)
                        position -= 2;
                    else if (cases[position + 2] == false)
                        position -= 2;
                }
                else if (touche.ToUpper() == "H")
                {
                    position += 4;
                    nombreEssais++;

                    if (position > 99)
                        position -= 4;
                    else if (cases[position + 4] == false)
                        position -= 4;
                }
                else if (touche.ToUpper() == "Y")
                    AffichageEntier(cases, position);
                else if (touche.ToUpper() == "P")
                    Affichage10(cases, position);
            }
        }

        static void AffichageEntier(bool[] cases, int position)
        {
            string tableauComplet = "";

            for (int i = 0; i < cases.Length-4; i++)
            {
                if (i == position)
                    tableauComplet += "X ";
                else
                    tableauComplet += (i + 1) + " ";
            }

            Console.WriteLine(tableauComplet);
        }

        static void Affichage10(bool[] cases, int position)
        {
            string tableauDixDix = "";

            for (int i = 10; i >= 1; i--)
            {
                if (position - i >= 0)
                    tableauDixDix += (position - i + 1) + " ";

            }

            tableauDixDix += "X ";

            for (int i = 1; i <= 10; i++)
            {
                if (position + i <= 99)
                    tableauDixDix += (position + i + 1) + " ";
            }

            Console.WriteLine(tableauDixDix);
        }
    }
}
