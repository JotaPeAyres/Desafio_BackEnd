using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MotorcycleRental.Bussiness.Models.Validations
{
    public class LicensePlateValidation
    {
        public static bool ValidatePlate(string plate)
        {
            return IsNationalPlate(plate) || IsMercosulPlate(plate);
        }

        private static bool IsNationalPlate(string value)
        {
            Regex nationalPlateRegex = new Regex("[a-zA-Z]{3}[0-9]{4}");
            return nationalPlateRegex.IsMatch(value);
        }

        private static bool IsMercosulPlate(string value)
        {
            Regex mercosulPlateRegex = new Regex("[a-zA-Z]{3}[0-9]{1}[a-zA-Z]{1}[0-9]{2}");
            return mercosulPlateRegex.IsMatch(value);
        }

    }

    public class DocumentValidation
    {
        public static bool ValidateCnpj(string document)
        {
            int[] multiplier1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplier2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int sum;
            int rest;
            string digit;
            string tempCnpj;
            document = document.Trim();
            document = document.Replace(".", "").Replace("-", "").Replace("/", "");
            if (document.Length != 14)
                return false;
            tempCnpj = document.Substring(0, 12);
            sum = 0;
            for (int i = 0; i < 12; i++)
                sum += int.Parse(tempCnpj[i].ToString()) * multiplier1[i];
            rest = (sum % 11);
            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;
            digit = rest.ToString();
            tempCnpj = tempCnpj + digit;
            sum = 0;
            for (int i = 0; i < 13; i++)
                sum += int.Parse(tempCnpj[i].ToString()) * multiplier2[i];
            rest = (sum % 11);
            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;
            digit = digit + rest.ToString();
            return document.EndsWith(digit);
        }
    }
}
