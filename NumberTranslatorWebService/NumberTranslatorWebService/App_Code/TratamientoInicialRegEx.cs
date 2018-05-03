using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

/// <summary>
/// Descripción breve de Class1
/// </summary>
public class TratamientoInicialRegEx
{
    public TratamientoInicialRegEx(){}
    public static int tratamientoInicialRegEx(ref String numeroText, ref Boolean signoMenos, ref String cadParteEntera, ref String cadParteDecimal, ref String cadDivisor)
    {
        String cadAux, cadAux1, expRistra = null;
        String Digitos = "0123456789";
        bool tieneCero = false, signoMas = false, blancosMal = false, tieneBlancos = false;
        int cantidadPuntos = 0, cantidadComas = 0;
        char especial = ' ';
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
            if ((cantidadComas > 0) || (cantidadPuntos > 0))
            {
                if (numeroText.IndexOf('.') > -1) especial = '.';
                else if (numeroText.IndexOf(',') > -1) especial = ',';
            }

            cadAux = numeroText.Trim().Replace('.', ',');

            if (cadAux[0].Equals('-'))
            {
                signoMenos = true;
                cadAux = cadAux.Substring(1);
            }

            //exponencial (con la e/E)
            
            regex = Regex.Match(cadAux.Replace(" ", ""), @"([-+]?(\d*\,?\d+))[eE](([-+])?(\d*\.?\d+))");
            if (regex.Success)
            {
                string preExp = regex.Groups[2].Value;
                string postExp = regex.Groups[5].Value;
                if (int.Parse(postExp) > 120) return 1; //exponente demasiado grande.

                double expNumber = double.Parse(preExp);
                System.Diagnostics.Debug.WriteLine(expNumber);
                if (regex.Groups[4].Value == "-")
                {

                    expNumber = expNumber / Math.Pow(10, double.Parse(postExp.Replace(".", "")));

                    string expNumberString = decimal.Parse(expNumber.ToString(), NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint).ToString().Replace(",", ".");

                    cadParteEntera = expNumberString.Substring(0, expNumberString.IndexOf("."));
                    cadParteDecimal = expNumberString.Substring(expNumberString.IndexOf(".") + 1);
                    return 0;
                }
                else
                {
                    expNumber = expNumber * Math.Pow(10, double.Parse(postExp.Replace(".", "")));
                    cadParteEntera = expNumber.ToString();
                    return 0;
                }

            }
            else
            {
                System.Diagnostics.Debug.WriteLine("No es un exponencial: " + cadAux);

            }
            
            //fracciones
            regex = Regex.Match(cadAux.Replace(" ", ""), @"(\d+)/(\d*)");
            if (regex.Success)
            {
                cadParteEntera = regex.Groups[1].Value;
                cadDivisor = regex.Groups[2].Value;
                return 0; //número correcto
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("No es una fracción: " + cadAux);
            }
            //if (cadParteEntera.Length == 0 || cadDivisor.Length == 0) return 3; //número mal formado.

            //decimales y enteros
            regex = Regex.Match(cadAux, @"(\d*[.|,])(\d+)");
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
            /*
            else
                cadParteEntera = "No ha entrado";
            if (cadParteEntera.Length == 0 || cadParteDecimal.Length == 0) return 3; //número mal formado.
            */
            cadParteEntera = cadAux;
        }
        catch(Exception e)
        {
            return 3;
        }
        return 0;
    }
}