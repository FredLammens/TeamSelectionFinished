using TeamSelectionLibrary;
using System;
namespace UI
{
    class Controller
    {
        public static void TeamSelectionStart() 
        {
            Console.WriteLine("----------TeamSelection----------");
            Console.WriteLine("Gelieve de naam van uw team in te geven :");
            string teamNaam = Console.ReadLine();
            Console.WriteLine("Gelieve de path van de file met alle spelergegevens in te geven a.u.b");
            string path = Console.ReadLine();
            var spelers = Parser.SpelersMakerFile(path, ',');
            Team team = new Team(teamNaam, spelers);
            SelectieMenu(team);
        }
        public static void SelectieMenu(Team team) 
        {
            Console.WriteLine("Typ de formatie in die u wilt gebruiken : indien niets ingevuld wordt het het standaard formatie van 4-4-2 gebruikt.");
            string formatie = Console.ReadLine();
            Console.WriteLine("Welke van de volgende opties wilt u zien :(typ het nummer in)" +
                "\n1)Standaard formatie" +
                "\n2)Beste formatie" +
                "\n3)Rotatie formatie");
            while (!int.TryParse(Console.ReadLine(), out int optie)) 
            {
                if (optie == 1)
                    team.Selectie("standaard", formatie);
                else if (optie == 2)
                    team.Selectie("best", formatie);
                else
                    team.Selectie("rotatie", formatie);
            }

        }
    }
}
