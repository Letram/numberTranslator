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
            cadAux = cadAux + " "; 
            regex = Regex.Match(cadAux, @"((\d+)([,](\d+))?)[eE]([+-]?(\d+)) ");
            if (regex.Success)
            {
                string numberPreExp = regex.Groups[1].Value;
                int exponent = Convert.ToInt16(regex.Groups[5].Value);
                normalize(ref numberPreExp);
                if (exponent == 0)
                {
                    if(numberPreExp.IndexOf(',') != -1)
                    {
                        cadParteEntera = numberPreExp.Substring(0, numberPreExp.IndexOf(','));
                        cadParteDecimal = numberPreExp.Substring(numberPreExp.IndexOf(','));
                    }
                    else
                    {
                        cadParteEntera = numberPreExp;
                    }
                    return 0;
                }
                int decimalLength = 0;
                if(numberPreExp.IndexOf(',') != 1 && numberPreExp.IndexOf(',') != -1) decimalLength = numberPreExp.Substring(numberPreExp.IndexOf(',')+1).Length;
                exponent += decimalLength;
                if (exponent > 120) return 1;
                numberPreExp = numberPreExp.Replace(",", "");
                numberPreExp = numberPreExp.Substring(0, 1) + "," + numberPreExp.Substring(1);
                string formattedNumber = "";
                if(exponent < 0)
                {
                    string aux = "0,";
                    while(exponent < 0)
                    {
                        aux += "0";
                        exponent++;
                    }
                    numberPreExp = numberPreExp.Replace(",", "");
                    formattedNumber = aux + numberPreExp;
                    cadParteEntera = formattedNumber.Substring(0, formattedNumber.IndexOf(','));
                    cadParteDecimal = formattedNumber.Substring(formattedNumber.IndexOf(','));
                    return 0;
                }
                else
                {
                    decimalLength = numberPreExp.Substring(numberPreExp.IndexOf(',') + 1).Length;
                    if(exponent == decimalLength)
                    {
                        cadParteEntera = numberPreExp.Replace(",", "");
                        return 0;
                    }
                    if(exponent > decimalLength)
                    {
                        if(regex.Groups[3].Value.IndexOf(",") != -1)
                        {
                            exponent -= decimalLength;
                        }
                        formattedNumber = numberPreExp.Replace(",", "") + new string('0', exponent);
                        cadParteEntera = formattedNumber;
                        return 0;
                    }
                    numberPreExp = numberPreExp.Replace(",", "");
                    formattedNumber = numberPreExp.Substring(0, exponent) + "," + numberPreExp.Substring(exponent+1);
                    if (formattedNumber.Length > 120) return 4;
                    cadParteEntera = formattedNumber.Substring(0, formattedNumber.IndexOf(','));
                    cadParteDecimal = formattedNumber.Substring(formattedNumber.IndexOf(','));
                    return 0;
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("No es un exponencial: " + cadAux);
            }
            
            //fracciones
            regex = Regex.Match(cadAux.Replace(" ", ""), @"(\d{1,120})/([-+])?(\d{1,120})");
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
            regex = Regex.Match(cadAux, @"(\d{1,120}[.|,])(\d{1,120})");
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
            cadAux = cadAux.Trim();
            regex = Regex.Match(cadAux, @"^(\d{1,120})$");
            if (regex.Success)
            {
                cadParteEntera = regex.Groups[1].Value;
                return 0;
            }
        }
        catch(Exception e)
        {
            return 3;
        }
        return 2;
    }

    private static void normalize(ref string numberPreExp)
    {
        numberPreExp = numberPreExp.TrimStart('0');
        numberPreExp = numberPreExp.TrimEnd('0');
    }
}