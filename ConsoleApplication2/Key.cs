using Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    public class Key : IKey
    {
        public int ReadKey(string text, string description)
        {
            int key;
            do
            {
                Console.WriteLine(string.Concat(text, description, ":"));
                key = ValidateKey();
            } while (key == -1);
            return key;
        }
        #region private methods
        private int ValidateKey()
        {
            int number;
            try
            {
                number = int.Parse(Console.ReadLine());
                if(number < Constants.MinMeasure)
                {
                    Console.WriteLine(string.Concat("Ingrese un Numero mayor o igual a ", Constants.MinMeasure,"!"));
                    number = -1;
                }
            }
            catch (System.Exception)
            {
                Console.WriteLine("Ingrese un Nùmero!");
                number = -1;
            }
            return number;
        }
        #endregion
    }
}
