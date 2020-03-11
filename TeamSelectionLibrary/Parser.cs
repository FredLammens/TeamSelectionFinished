
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TeamSelectionLibrary
{
    public class Parser
    {
        /// <summary>
        /// Split file in een lijst van string arrays met een teken als delimeter
        /// </summary>
        /// <param name="fileToReadPath">pad waar bestand zich bevindt</param>
        /// <param name="teken">delimeter</param>
        /// <returns></returns>
        public static List<String[]> FileSplitter(string fileToReadPath, char teken)
        {
            List<String[]> splittedLines = new List<String[]>();
            using (FileStream fs = File.Open(fileToReadPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (StreamReader sr = new StreamReader(fs))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    splittedLines.Add(s.Split(teken));
                }
            }
            return splittedLines;
        }
        /// <summary>
        /// parameters checken voor bij formatie vb . 4-4-2 is geldig 2-2-2 is niet geldig
        /// </summary>
        /// <param name="formatie"> vb . 4-4-2</param>
        /// <returns></returns>
        public static List<int> ParameterChecker(string formatie)
        {
            string[] formatieSplitted = formatie.Split('-');//TC
            int aantalDefenders;
            int aantalMid;
            int aantalAttack;
            if (formatieSplitted.Length != 3)
            {
                if (formatieSplitted.Length < 3)
                    throw new TeWeinigParameterException();
                else
                    throw new TeveelParameterException();
            }
            else
            {
                if (!int.TryParse(formatieSplitted[0], out aantalDefenders) & !int.TryParse(formatieSplitted[1], out aantalMid) & !int.TryParse(formatieSplitted[2], out aantalAttack))//TC geen && anders voert 2e int.tryparse niet
                    throw new Exception("Is geen cijfer");
                else if ((aantalMid + aantalDefenders + aantalAttack) > 10)
                {
                    throw new Exception("totaal aantal spelers != 10");
                }
            }
            return new List<int>() { aantalAttack, aantalDefenders, aantalMid };
        }
        /// <summary>
        /// Uit een file een lijst van spelerobjecten maken
        /// </summary>
        /// <param name="fileToReadPath">pad waar bestand zich bevindt</param>
        /// <param name="delim">teken dat de verschillende attributen scheid in de file</param>
        /// <returns></returns>
        public static HashSet<Speler> SpelersMakerFile(string fileToReadPath, char delim)
        {
            List<string[]> lines = Parser.FileSplitter(fileToReadPath, delim);
            HashSet<Speler> spelers = new HashSet<Speler>();
            foreach (string[] line in lines)
            {
                int laatste = line.Length - 1;
                int voorlaatste = laatste - 1;
                string speler = line[0];
                string naam = line[1];
                ushort rugNummer;
                ushort rating;
                int aantalWedGesp;
                List<Speler.PositieNamen> mogelijkePosities;
                #region parsen van waarden
                mogelijkePosities = MogelijkePositiesParser(line);
                if (!ushort.TryParse(line[2], out rugNummer))
                    throw new UshortParseException();
                if (!ushort.TryParse(line[voorlaatste], out rating))
                    throw new UshortParseException();
                if (!int.TryParse(line[laatste], out aantalWedGesp))
                    throw new IntOmzetException();
                #endregion
                spelers.Add(TypeSpelerMaken(speler, naam, rugNummer, rating, aantalWedGesp, mogelijkePosities));
            }
            return spelers;
        }
        #region Hulpmethoden voor spelersmakerFile
        public static Speler TypeSpelerMaken(string speler, string naam, ushort rugNummer, ushort rating, int aantalWedGesp, List<Speler.PositieNamen> mogelijkePosities)
        {
            if (speler == "GoalKeeper")
                return new GoalKeeper(naam, rugNummer, rating, aantalWedGesp, mogelijkePosities);
            if (speler == "Defender")
                return new Defender(naam, rugNummer, rating, aantalWedGesp, mogelijkePosities);
            if (speler == "Forward")
                return new Forward(naam, rugNummer, rating, aantalWedGesp, mogelijkePosities);
            if (speler == "MidFielder")
                return new MidFielder(naam, rugNummer, rating, aantalWedGesp, mogelijkePosities);
            else
                throw new SpelerNaamOnbestaand();
        }
        public static List<Speler.PositieNamen> MogelijkePositiesParser(string[] line)
        {
            //int eindlengte = line.Length - 2;
            //line[3..eindlengte]; 
            //kan enkel bij c# 8.0$
            //list getRange ?
            //Span

            int aantalMogelijkePosities = line.Length - 5; // -5 want er zijn 5 variabelen die wel gedefinieerd zijn.
            string[] mogelijkePositiesFile = line.Skip(3).Take(aantalMogelijkePosities).ToArray();
            List<Speler.PositieNamen> geparsed = new List<Speler.PositieNamen>();
            foreach (string positie in mogelijkePositiesFile)
            {
                if (Enum.TryParse(positie, out Speler.PositieNamen result))
                    geparsed.Add(result);
                else
                    throw new MogelijkePositieException();
            }
            return geparsed;
        }
        #endregion
    }
}
