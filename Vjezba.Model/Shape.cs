using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace Vjezba.Model
{
    public class Card
    {
        public string CardNumber { get; set; }
        public int ExpMonth { get; set; }
        public int ExpYear { get; set; }
    }

    public class CardProcessor
    {
        public void ProcessCard(Card card)
        {

        }
    }
    public class CardNumberValidator
    {
        public bool Validate(Card card)
        {
            return card.CardNumber.Length == 16;
        }
    }

    public class CardExpirationValidator
    {
        public bool Validate(Card card)
        {
            return card.ExpMonth >= 1 && card.ExpMonth <= 12 && card.ExpYear >= 2018;
        }
    }





    



    /*public static class ShapeFactory
    {
        public static Shape CreatePolygon(int sides)
        {
            
            return new Polygon { NumberOfSides = sides };
        } 

        public static List<string> SplitFullName(this string fullName)
        {
            try
            {
                string[] names = fullName.Split(' ');
                List<string> result = new List<string>();
                result.Add(names[0]);
                result.Add(string.Join(" ", names.Skip(1)));
                return result;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<string>() { "", "" } ;
            }
            
        }
    }
    public abstract class Shape
    {

        private void DoSomething()

        {
            string fullname = "Ivan Cesar";
            var nameSplit = fullname.SplitFullName();
            
        }

        private string _color;

        private readonly double _x;
        private readonly double _y;

        public Shape(string color)
        {
            Color = color;
        }

        protected Shape(double x, double y)
        {
            _x = x;
            _y = y;
        }

        public string Color { get; set; }


        public virtual void Print()
        {
        }

        public class Polygon : Shape
        {
            public Polygon() : base("Red")
            {
            }

            public int NumberOfSides { get; set; }
            public override void Print()
            {
                Console.WriteLine("Color: {0}, number of sides: {1}", Color, NumberOfSides);
                Console.WriteLine($"Color: {Color}, Number of sies: {NumberOfSides}");
                Console.WriteLine("Color: " + Color + ", Number of sides: " + NumberOfSides);
            }
        }
    }*/
}
