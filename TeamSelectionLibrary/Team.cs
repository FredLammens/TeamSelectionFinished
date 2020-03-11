
using System;
using System.Collections.Generic;
using System.Linq;

namespace TeamSelectionLibrary
{
    public class Team
    {
        #region properties
        public string Naam { get; private set; }
        public HashSet<Speler> Spelers { get; private set; }
        private List<Speler> selectie = new List<Speler>();
        private HashSet<string> mogelijkeSelecties;
        private string currentSelection;
        #endregion
        #region methoden
        public Team(string naam, HashSet<Speler> spelers)
        {
            (this.Naam, this.Spelers) = (naam, spelers);
            mogelijkeSelecties = new HashSet<string>() { "best", "rotatie", "standaard" };
        }
        public void Selectie(string selectieMethode, string formatie = "4-4-2")
        {
            if (mogelijkeSelecties.Contains(selectieMethode))
            {
                currentSelection = selectieMethode;
                switch (selectieMethode)
                {
                    case "best":
                        Best(formatie);
                        break;
                    case "rotatie":
                        Rotatie(formatie);
                        break;
                    case "standaard":
                        Standaard(formatie);
                        break;
                }
                currentSelection = "";
            }
            else
            {
                throw new SelectieException();
            }

        }
        #region SelectieTypes
        private void Best(string formatie)
        {
            List<int> formatieLijst = Parser.ParameterChecker(formatie);
            var sortSpelers = Spelers.Where(s => s.CurrentStatus == Speler.Status.kanSpelen).OrderByDescending(s => s.Rating); // lijst van alle spelers sorteren op aantalwedgesp
            var goalKeeper = sortSpelers.Where(s => s.GetType() == typeof(GoalKeeper));
            var defenders = sortSpelers.Where(s => s.GetType() == typeof(Defender));
            var middle = sortSpelers.Where(s => s.GetType() == typeof(MidFielder));
            var attackers = sortSpelers.Where(s => s.GetType() == typeof(Forward));
            selectie.Add(goalKeeper.ElementAt(0));
            for (int i = 0; i < formatieLijst[0]; i++)
            {
                selectie.Add(defenders.ElementAt(i));
            }
            for (int i = 0; i < formatieLijst[1]; i++)
            {
                selectie.Add(middle.ElementAt(i));
            }
            for (int i = 0; i < formatieLijst[2]; i++)
            {
                selectie.Add(attackers.ElementAt(i));
            }
            selectie.OrderBy(s => s.Rating).Last().IsCaptain = true;
            Print(selectie);
        }
        private void Rotatie(string formatie)
        {
            List<int> formatieLijst = Parser.ParameterChecker(formatie);
            var sortSpelers = Spelers.Where(s => s.CurrentStatus == Speler.Status.kanSpelen).OrderBy(s => s.AantalWedGesp); // lijst van alle spelers sorteren op aantalwedgesp
            var goalKeeper = sortSpelers.Where(s => s.GetType() == typeof(GoalKeeper));
            var defenders = sortSpelers.Where(s => s.GetType() == typeof(Defender));
            var middle = sortSpelers.Where(s => s.GetType() == typeof(MidFielder));
            var attackers = sortSpelers.Where(s => s.GetType() == typeof(Forward));
            selectie.Add(goalKeeper.ElementAt(0));
            for (int i = 0; i < formatieLijst[0]; i++)
            {
                selectie.Add(defenders.ElementAt(i));
            }
            for (int i = 0; i < formatieLijst[1]; i++)
            {
                selectie.Add(middle.ElementAt(i));
            }
            for (int i = 0; i < formatieLijst[2]; i++)
            {
                selectie.Add(attackers.ElementAt(i));
            }
            selectie.OrderBy(s => s.AantalWedGesp).Last().IsCaptain = true;
            Print(selectie);
        }
        private void Standaard(string formatie)
        {
            List<int> formatieLijst = Parser.ParameterChecker(formatie);
            var sortSpelers = Spelers.Where(s => s.CurrentStatus == Speler.Status.kanSpelen).OrderByDescending(s => s.AantalWedGesp); // lijst van alle spelers sorteren op aantalwedgesp
            var goalKeeper = sortSpelers.Where(s => s.GetType() == typeof(GoalKeeper));
            var defenders = sortSpelers.Where(s => s.GetType() == typeof(Defender));
            var middle = sortSpelers.Where(s => s.GetType() == typeof(MidFielder));
            var attackers = sortSpelers.Where(s => s.GetType() == typeof(Forward));
            selectie.Add(goalKeeper.ElementAt(0));
            for (int i = 0; i < formatieLijst[0]; i++)
            {
                selectie.Add(defenders.ElementAt(i));
            }
            for (int i = 0; i < formatieLijst[1]; i++)
            {
                selectie.Add(middle.ElementAt(i));
            }
            for (int i = 0; i < formatieLijst[2]; i++)
            {
                selectie.Add(attackers.ElementAt(i));
            }
            selectie.OrderBy(s => s.AantalWedGesp).Last().IsCaptain = true;
            Print(selectie);
        }
        #endregion
        #region spelersWijzigen
        public void AddSpeler(Speler speler)
        {
            if (!Spelers.Contains(speler))
                Spelers.Add(speler);
        }
        public void RemoveSpeler(Speler speler)
        {
            if (!Spelers.Contains(speler))
                Spelers.Remove(speler);
        }
        #endregion
        private void Print(List<Speler> selectie)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"-----------------------------------Selection for {Naam} ---- Formation {currentSelection}-------------------------------------------");
            foreach (Speler speler in selectie)
            {
                if (speler.CurrentStatus == Speler.Status.kanSpelen)
                {
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    if (speler.IsCaptain == true)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        speler.IsCaptain = false;
                    }
                    Console.WriteLine(speler);
                }
                else
                {
                    throw new UnavailablePlayerException();
                }
            }
            selectie.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.White;
        }
        #endregion
    }
}
