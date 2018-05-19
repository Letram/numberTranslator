using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Numerics;
/// <summary>
/// Descripción breve de Class1
/// </summary>
public class TratamientoInicialRegEx
{
    public TratamientoInicialRegEx(){}
    public static int tratamientoInicialRegEx(ref String numeroText, ref Boolean signoMenos, ref String cadParteEntera, ref String cadParteDecimal, ref String cadDivisor)
    {
        String cadAux;
        int cantidadPuntos = 0, cantidadComas = 0;
        Match regex;
        try
            //partimos de la suposición de que nos lo mandan bien
        {
            cadAux = numeroText;
            
            //tratamiento inicial para quitar valores a no tener en cuenta y preparar la string
            //Se eliminan el $ y el € y los espacios al inicio y al final
            numeroText = numeroText.Replace("$", "");
            numeroText = numeroText.Replace("€", "");
            numeroText = numeroText.Replace('\'', ',');
            numeroText = numeroText.Trim();

            // Tratamiento del formato con puntos y comas para los miles y los millones.
            for (int i = 0; i < numeroText.Length; i++)
            {
                if (numeroText[i] == '.') cantidadPuntos++;
                if (numeroText[i] == ',') cantidadComas++;
            }
            if (((cantidadPuntos == 1) && (numeroText.EndsWith("."))) ||
                ((cantidadComas == 1) && (numeroText.EndsWith(",")))) numeroText += "0";

            if (((cantidadPuntos > 1) && (cantidadComas == 0)) || ((cantidadPuntos == 0) && (cantidadComas > 1))) numeroText = numeroText.Replace('.', ' ').Replace(',', ' ');
            if (((cantidadPuntos == 1) && (cantidadComas > 0)) || ((cantidadPuntos > 0) && (cantidadComas == 1)))
            {
                if (numeroText.LastIndexOf('.') > numeroText.LastIndexOf(',')) numeroText = numeroText.Replace(',', ' ');
                else numeroText = numeroText.Replace('.', ' ');
            }

            cadAux = numeroText.Trim().Replace('.', ',');

            if (cadAux[0].Equals('-'))
            {
                signoMenos = true;
                cadAux = cadAux.Substring(1);
            }
            //en el caso de que hayan letras de por medio que no estén contempladas
            regex = Regex.Match(cadAux, @"[a-df-zA-DF-Z]+");
            if (regex.Success) return 3; //número que no está bien escrito

            //exponencial (con la e/E)
            cadAux = new Regex("\\s+").Replace(cadAux, "");
            regex = Regex.Match(cadAux.Replace(" ", ""), @"([-+]?(\d*\,?\d+))[eE](([-+])?(\d{1,120}))");
            if (regex.Success)
            {
                string preExp = regex.Groups[2].Value;
                int decimalDigits = 0;
                if(preExp.IndexOf(",") != -1)
                    decimalDigits = Convert.ToInt16(preExp.Substring(preExp.IndexOf(",")+1).Length);
                string postExp = regex.Groups[5].Value;
                if (int.Parse(postExp) > 120) return 1; //exponente demasiado grande.

                double expNumber = double.Parse(preExp);
                System.Diagnostics.Debug.WriteLine(expNumber);
                if (regex.Groups[4].Value == "-")
                {
                    expNumber = expNumber / Math.Pow(10, double.Parse(postExp.Replace(".", "")));
                    string expNumberString = expNumber.ToString("N" + (Convert.ToInt16(postExp.Replace(".", "")) + decimalDigits));
                    cadParteEntera = expNumberString.Substring(0, expNumberString.IndexOf(","));
                    cadParteDecimal = expNumberString.Substring(expNumberString.IndexOf(",") + 1);
                    return 0;
                }
                else
                {
                    expNumber = expNumber * Math.Pow(10, double.Parse(postExp.Replace(".", ""), NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent));
                    cadParteEntera = expNumber.ToString("N0").Replace(".", "");
                    return 0;
                }

            }
            else
            {
                System.Diagnostics.Debug.WriteLine("No es un exponencial: " + cadAux);
            }
            
            //fracciones
            regex = Regex.Match(cadAux.Replace(" ", ""), @"\b(\d{1,120})/([-+])?(\d{1,120})\b");
            if (regex.Success)
            {
                cadParteEntera = regex.Groups[1].Value;
                if (regex.Groups[2].Value == "-")
                {
                    signoMenos = !signoMenos;
                };
                cadDivisor = regex.Groups[3].Value;
                return 0; //número correcto
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("No es una fracción: " + cadAux);
            }

            //decimales y enteros
            regex = Regex.Match(cadAux, @"\b(\d{1,120}[.|,])(\d{1,120})\b");
            if (regex.Success)
            {
                cadParteEntera = regex.Groups[1].Value == "," ? "0" : regex.Groups[1].Value.Remove(regex.Groups[1].Value.Length-1);
                cadParteDecimal = regex.Groups[2].Value == "" ? "0" : regex.Groups[2].Value;
                return 0; //número correcto
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("No es un decimal: " + cadAux);
            }

            regex = Regex.Match(cadAux, @"^(\d{1,120})$");
            if (regex.Success)
            {
                cadParteEntera = regex.Groups[1].Value;
                return 0;
            }
            /*
            else
                cadParteEntera = "No ha entrado";
            if (cadParteEntera.Length == 0 || cadParteDecimal.Length == 0) return 3; //número mal formado.
            */
        }
        catch(Exception e)
        {
            return 3;
        }
        return 2;
    }
}