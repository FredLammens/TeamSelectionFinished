using System;

namespace TeamSelectionLibrary
{
    #region ControllerExceptionHandler
    public class ControllerExceptionHandler : Exception
    {
        public ControllerExceptionHandler()
        {
            Console.WriteLine("Er is iets misgelopen bij de controller");
        }
    }
    public class FileSplitterException : ControllerExceptionHandler
    {
        public FileSplitterException()
        {
            Console.WriteLine("Iets is misgelopen met de path van het bestand.");
        }
    }
    #endregion
    #region TeamExceptionHandler
    public class TeamExceptionHandler : Exception
    {
        public TeamExceptionHandler()
        {
            Console.WriteLine("Er is iets fout gelopen bij team");
        }
    }
    public class SelectieException : TeamExceptionHandler
    {
        public SelectieException()
        {
            Console.WriteLine("selectie bestaat niet . contacteer de programmeur van dienst .");
        }
    }
    public class PlayerExceptionHandler : TeamExceptionHandler
    {
        public PlayerExceptionHandler()
        {
            Console.WriteLine("Er is iets fout gelopen bij de speler");
        }
    }
    public class UnavailablePlayerException : PlayerExceptionHandler
    {
        public UnavailablePlayerException()
        {
            Console.WriteLine("Speler is niet beschikbaar.");
        }
    }
    public class WrongPositionPlayerException : PlayerExceptionHandler
    {
        public WrongPositionPlayerException()
        {
            Console.WriteLine("Speler kan niet op deze positie spelen.");
        }
    }
    public class FormatieException : PlayerExceptionHandler
    {
        public FormatieException()
        {
            Console.WriteLine("Er is iets fout gelopen bij het aanmaken van de formatie");
        }
    }
    public class TeveelParameterException : FormatException
    {
        public TeveelParameterException()
        {
            Console.WriteLine("Er zijn teveel parameters gegeven .Het moeten er 3 zijn.");
        }
    }
    public class TeWeinigParameterException : FormatException
    {
        public TeWeinigParameterException()
        {
            Console.WriteLine("Er zijn te weinig parameters gegeven . Het moeten er 3 zijn.");
        }
    }
    #endregion
    #region parserException
    public class ParserExceptionHandler : Exception
    {
        public ParserExceptionHandler()
        {
            Console.WriteLine("Er is iets fout gelopen bij het parsen van bestand.");
        }
    }
    public class SpelerNaamOnbestaand : ParserExceptionHandler
    {
        public SpelerNaamOnbestaand()
        {
            Console.WriteLine("Spelertype bestaat niet gelieve het bestand te controleren. of programmeur van dienst te contacteren");
        }
    }
    public class MogelijkePositieException : ParserExceptionHandler
    {
        public MogelijkePositieException()
        {
            Console.WriteLine("Er is iets fout gelopen bij het inlezen van de mogelijke posities.");
        }
    }
    public class UshortParseException : ParserExceptionHandler
    {
        public UshortParseException()
        {
            Console.WriteLine("Er is iets foutgelopen bij het omzetten naar een ushort.");
        }
    }
    public class IntOmzetException : ParserExceptionHandler
    {
        public IntOmzetException()
        {
            Console.WriteLine("Er is iet foutgelopen bij het omzetten naar een int.");
        }
    }
    #endregion
}
