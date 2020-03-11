
using System.Collections.Generic;
namespace TeamSelectionLibrary
{
    public class Speler
    {
        #region properties
        public bool IsCaptain { get; set; } = false;
        public string Naam { get; private set; }
        public ushort Rugnummer { get; set; }
        public enum PositieNamen
        {
            CentralDefence,
            RightBack,
            LeftBack,
            DefenceMidfield,
            RightMidfield,
            LeftMidfield,
            AttackingMidfield,
            CentralForward,
            LeftWinger,
            RightWinger,
            GoalKeeper
        }
        public List<PositieNamen> ToegelatenPosities { get; private set; }
        public List<PositieNamen> MogelijkePosities { get; private set; }
        public ushort Rating { get; set; }
        public int AantalWedGesp { get; set; } = 0;
        public enum Status
        {
            geblesseerd,
            kanSpelen,
        }
        public Status CurrentStatus { get; set; } = Status.kanSpelen;
        #endregion
        #region constructor
        public Speler(string naam, ushort rugNummer, ushort rating, int aantalWedGesp, List<PositieNamen> mogelijkePosities)
        {
            (Naam, Rugnummer, Rating, AantalWedGesp, MogelijkePosities) = (naam, rugNummer, rating, aantalWedGesp, mogelijkePosities);
            ToegelatenPosities = new List<PositieNamen>();
            MogelijkePosities = new List<PositieNamen>();
        }
        #endregion
        public override string ToString()
        {
            string posities = "";
            foreach (PositieNamen positieNaam in MogelijkePosities)
            {
                posities += positieNaam.ToString() + ", ";
            }
            posities = posities.Remove(posities.Length - 2);

            return $"Naam: {Naam}, Rugnummer: {Rugnummer}, Rating: {Rating}, Posities: [{posities}],Aantal wedstrijden gespeeld: {AantalWedGesp}";
        }

        public override bool Equals(object obj)
        {
            return obj is Speler speler &&
                   Naam == speler.Naam &&
                   Rugnummer == speler.Rugnummer &&
                   Rating == speler.Rating;
        }

        public override int GetHashCode()
        {
            var hashCode = 2004589807;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Naam);
            hashCode = hashCode * -1521134295 + Rugnummer.GetHashCode();
            hashCode = hashCode * -1521134295 + Rating.GetHashCode();
            return hashCode;
        }
    }
    class GoalKeeper : Speler
    {
        public GoalKeeper(string naam, ushort rugNummer, ushort rating, int aantalWedGesp, List<PositieNamen> mogelijkePosities) : base(naam, rugNummer, rating, aantalWedGesp, mogelijkePosities)
        {
            ToegelatenPosities.Add(PositieNamen.GoalKeeper);
            foreach (PositieNamen positie in mogelijkePosities)
            {
                if (mogelijkePosities.Contains(positie))
                {
                    base.MogelijkePosities.Add(positie);
                }
                else
                    throw new WrongPositionPlayerException();
            }

        }
        public override string ToString()
        {
            return this.GetType().Name + " - " + base.ToString();
        }
    }
    class Forward : Speler
    {
        public Forward(string naam, ushort rugNummer, ushort rating, int aantalWedGesp, List<PositieNamen> mogelijkePosities) : base(naam, rugNummer, rating, aantalWedGesp, mogelijkePosities)
        {
            ToegelatenPosities.Add(PositieNamen.CentralForward);
            ToegelatenPosities.Add(PositieNamen.LeftWinger);
            ToegelatenPosities.Add(PositieNamen.RightWinger);

            foreach (PositieNamen positie in mogelijkePosities)
            {
                if (mogelijkePosities.Contains(positie))
                {
                    base.MogelijkePosities.Add(positie);
                }
                else
                    throw new WrongPositionPlayerException();
            }
        }
        public override string ToString()
        {
            return this.GetType().Name + " - " + base.ToString();
        }
    }
    class MidFielder : Speler
    {
        public MidFielder(string naam, ushort rugNummer, ushort rating, int aantalWedGesp, List<PositieNamen> mogelijkePosities) : base(naam, rugNummer, rating, aantalWedGesp, mogelijkePosities)
        {
            ToegelatenPosities.Add(PositieNamen.DefenceMidfield);
            ToegelatenPosities.Add(PositieNamen.RightMidfield);
            ToegelatenPosities.Add(PositieNamen.LeftMidfield);
            ToegelatenPosities.Add(PositieNamen.AttackingMidfield);

            foreach (PositieNamen positie in mogelijkePosities)
            {
                if (mogelijkePosities.Contains(positie))
                {
                    base.MogelijkePosities.Add(positie);
                }
                else
                    throw new WrongPositionPlayerException();
            }
        }
        public override string ToString()
        {
            return this.GetType().Name + " - " + base.ToString();
        }
    }
    class Defender : Speler
    {
        public Defender(string naam, ushort rugNummer, ushort rating, int aantalWedGesp, List<PositieNamen> mogelijkePosities) : base(naam, rugNummer, rating, aantalWedGesp, mogelijkePosities)
        {
            ToegelatenPosities.Add(PositieNamen.CentralDefence);
            ToegelatenPosities.Add(PositieNamen.LeftBack);
            ToegelatenPosities.Add(PositieNamen.RightBack);

            foreach (PositieNamen positie in mogelijkePosities)
            {
                if (mogelijkePosities.Contains(positie))
                {
                    base.MogelijkePosities.Add(positie);
                }
                else
                    throw new WrongPositionPlayerException();
            }
        }
        public override string ToString()
        {
            return this.GetType().Name + " - " + base.ToString();
        }
    }
}
