using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de TratamientoInicial
/// </summary>
public class TratamientoInicial
{
    public TratamientoInicial(){}
    public static int InitialTratement(ref String numeroText, ref String signoMenos, ref String cadParteEntera, ref String cadParteDecimal, ref String cadDivisor)
    {
        String cadAux, cadAux1, expRistra = null;
        String Digitos = "0123456789";
        bool tieneCero = false, signoMas = false, blancosMal = false, tieneBlancos = false;
        int cantidadPuntos = 0, cantidadComas = 0;
        char especial = ' ';

        try
        {
            //Se eliminan el $ y el €
            numeroText = numeroText.Replace("$", "");
            numeroText = numeroText.Replace("€", "");
            numeroText = numeroText.Replace('\'', ',');
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
            if ((cadAux[0] == ',') && (cadAux.Length > 1)) cadAux = cadAux.Insert(0, "0");
            if ((cadAux.IndexOf('e') > -1) || (cadAux.IndexOf('E') > -1))
            {
                int ind = cadAux.IndexOf('e');
                if (ind == -1) ind = cadAux.IndexOf('E');
                expRistra = cadAux.Substring(ind + 1).Replace(" ", "");
                cadAux = cadAux.Substring(0, ind).Trim();
                numeroText = cadAux + "E" + expRistra;
                if (signoMenos != null) numeroText = '-' + numeroText;
            }
            if ((cadAux.IndexOf(',') != -1) && (cadAux.Length > 1))
            {
                cadParteEntera = cadAux.Substring(0, cadAux.IndexOf(',')).Trim();
                cadParteDecimal = cadAux.Substring(cadAux.IndexOf(',') + 1).Trim();
                numeroText = cadParteEntera + especial + cadParteDecimal;
                if (expRistra != null) numeroText = numeroText + "E" + expRistra;
                if (signoMenos != null) numeroText = '-' + numeroText;
            }
            else
            {
                if (cadAux.IndexOf('/') == -1) cadParteEntera = cadAux;
                else
                {
                    if (expRistra == null)
                    {
                        cadParteEntera = cadAux.Substring(0, cadAux.IndexOf('/')).Trim();
                        cadDivisor = cadAux.Substring(cadAux.IndexOf('/') + 1).Trim();
                        numeroText = cadParteEntera + "/" + cadDivisor;
                        if (signoMenos != null) numeroText = '-' + numeroText;
                    }
                    if ((cadParteEntera.Length == 0) || (cadDivisor.Length == 0)) return 3; // El número es incorrecto.
                }
            }
            if (cadParteEntera != null)
            {
                if (cadParteEntera.Length > 126) return 1; //El número es demasiado grande.
                if ((cadParteEntera.Length > 1) && (cadParteEntera[0] == '-'))
                {
                    cadParteEntera = cadParteEntera.Substring(1);
                    signoMenos = "menos ";
                }
                if ((cadParteEntera.Length > 1) && (cadParteEntera[0] == '+'))
                {
                    signoMas = true;
                    cadParteEntera = cadParteEntera.Substring(1);
                }
                cadParteEntera = cadParteEntera.TrimStart();

                if ((cadParteEntera.Length != 0) && (cadParteEntera[0] == '0'))
                {
                    tieneCero = true;
                    while ((cadParteEntera.Length > 1) && (cadParteEntera[0] == '0')) cadParteEntera = cadParteEntera.TrimStart('0').TrimStart();
                    if (cadParteEntera.Length == 0) cadParteEntera = "0";
                    numeroText = cadParteEntera;
                    if ((especial == '.') || (especial == ',')) numeroText = numeroText + especial + cadParteDecimal;
                    if (cadDivisor != null) numeroText = numeroText + '/' + cadDivisor;
                    if (expRistra != null) numeroText = numeroText + 'E' + expRistra;
                    if ((signoMenos != null) && (Digitos.IndexOf(numeroText[0]) > -1)) numeroText = '-' + numeroText;
                }

                // Tratamiento del formato con blancos para los miles y los millones.
                cadAux = cadParteEntera;
                cadAux1 = cadParteEntera;
                while (cadAux.LastIndexOf(' ') > -1)
                {
                    tieneBlancos = true;
                    int distancia = cadAux.Length - cadAux.LastIndexOf(' ');
                    if (distancia == 4) cadParteEntera = cadParteEntera.Substring(0, cadParteEntera.LastIndexOf(' ')) + cadParteEntera.Substring(cadParteEntera.LastIndexOf(' ') + 1);
                    else
                    {
                        blancosMal = true;
                        break; //return 3; //El número es incorrecto.
                    }
                    cadAux = cadAux.Substring(0, cadAux.LastIndexOf(' '));
                }

                // Comprueba si es un número romano
                /*
                bool esRomano = false;
                
                if ((expRistra == null) && (signoMenos == null) && (!signoMas) &&
                    (cadParteDecimal == null) && (cadParteEntera.Length > 0))
                {
                    esRomano = true;
                    String numRomano = cadParteEntera.ToUpper().Replace(" ", "");
                    for (int i = 0; i < numRomano.Length; i++)
                    {
                        if (romanosBis.IndexOf(numRomano[i]) == -1)
                        {
                            esRomano = false;
                            break; //El número es incorrecto.
                        }
                    }
                    if (esRomano)
                    {
                        if (numeroText.IndexOf(' ') > -1)
                        {
                            numeroText = numeroText.Replace(" ", "");
                            cadParteEntera = numRomano;
                        }
                        return trataRomanoAEntero(ref cadParteEntera);
                    }
                }
                */

                // Si los blancos en los miles y millones están mal colocados se arreglan.
                if (/*(!esRomano) && */((blancosMal) || ((cadAux.Length > 3) && (tieneBlancos)) || ((tieneBlancos == false) && (cadAux.Length > 4))))
                {
                    cadParteEntera = "";
                    numeroText = "";
                    int dist = 0;
                    for (int i = cadAux1.Length - 1; i >= 0; i--)
                    {
                        if (cadAux1[i] != ' ')
                        {
                            if (dist++ % 3 == 0) numeroText = " " + numeroText;
                            numeroText = cadAux1[i] + numeroText;
                            cadParteEntera = cadAux1[i] + cadParteEntera;
                        }
                    }
                    numeroText = numeroText.Trim();
                    if ((especial == '.') || (especial == ',')) numeroText = numeroText + especial + cadParteDecimal;
                    if (cadDivisor != null) numeroText = numeroText + '/' + cadDivisor;
                    if (expRistra != null) numeroText = numeroText + 'E' + expRistra;
                    if (signoMenos != null) numeroText = '-' + numeroText;
                }

                for (int i = 0; i < cadParteEntera.Length; i++)
                {
                    if (Digitos.IndexOf(cadParteEntera[i]) == -1) return 3; //El número es incorrecto.
                }
                if ((cadParteEntera.Length == 0) && (tieneCero))
                {
                    cadParteEntera = "0";
                    signoMenos = null;
                }
            }
            if (cadParteDecimal != null)
            {
                if (cadParteDecimal[cadParteDecimal.Length - 1] == '0')
                {
                    while ((cadParteDecimal.Length > 0) && (cadParteDecimal[cadParteDecimal.Length - 1] == '0')) cadParteDecimal = cadParteDecimal.TrimEnd('0').TrimEnd();
                    numeroText = numeroText.Substring(0, numeroText.IndexOf(especial));
                    if (cadParteDecimal.Length > 0) numeroText = numeroText + especial + cadParteDecimal;
                    if (expRistra != null) numeroText = numeroText + 'E' + expRistra;
                }

                // Tratamiento del formato con blancos en grupos de tres para los decimales.
                cadAux = cadParteDecimal;
                cadAux1 = cadParteDecimal;
                blancosMal = false;
                tieneBlancos = false;
                while (cadAux.IndexOf(' ') > -1)
                {
                    tieneBlancos = true;
                    int distancia = cadAux.IndexOf(' ');
                    if (distancia == 3) cadParteDecimal = cadParteDecimal.Substring(0, cadParteDecimal.IndexOf(' ')) + cadParteDecimal.Substring(cadParteDecimal.IndexOf(' ') + 1);
                    else
                    {
                        blancosMal = true; //return 3; //El número es incorrecto.
                        break;
                    }
                    cadAux = cadAux.Substring(cadAux.IndexOf(' ') + 1);
                }
                // Si los blancos en los miles y millones están mal colocados se arreglan.
                if ((blancosMal) || ((cadAux.Length > 3) && (tieneBlancos)) || ((tieneBlancos == false) && (cadAux.Length > 4)))
                {
                    cadParteDecimal = "";
                    cadAux = "";
                    int dist = 0;
                    for (int i = 0; i < cadAux1.Length; i++)
                    {
                        if (cadAux1[i] != ' ')
                        {
                            if (dist++ % 3 == 0) cadAux += " ";
                            cadAux += cadAux1[i];
                            cadParteDecimal += cadAux1[i];
                        }
                    }
                    cadAux = cadAux.Trim();
                    numeroText = numeroText.Substring(0, numeroText.IndexOf(especial) + 1) + cadAux;
                    if (expRistra != null) numeroText = numeroText + 'E' + expRistra;
                    if ((signoMenos != null) && (Digitos.IndexOf(numeroText[0]) > -1)) numeroText = '-' + numeroText;
                }

                if (cadParteDecimal.Length > 125) return 2; ; //La parte decimal es demasiado grande.
                for (int i = 0; i < cadParteDecimal.Length; i++)
                {
                    if (Digitos.IndexOf(cadParteDecimal[i]) == -1) return 3; //El número es incorrecto.
                }
            }

            //Tratamiento del divisor
            if (cadDivisor != null)
            {
                if (cadDivisor[0] == '-')
                {
                    if (signoMenos != null) signoMenos = null;
                    else signoMenos = "menos ";
                    cadDivisor = cadDivisor.Substring(1).TrimStart();
                }
                else if (cadDivisor[0] == '+') cadDivisor = cadDivisor.Substring(1);

                tieneCero = false;
                if (cadDivisor[0] == '0')
                {
                    tieneCero = true;
                    while ((cadDivisor.Length > 1) && (cadDivisor[0] == '0')) cadDivisor = cadDivisor.TrimStart('0').TrimStart();
                    if (cadDivisor.Length == 0) cadDivisor = "0";
                    numeroText = numeroText.Substring(0, numeroText.IndexOf('/') + 1) + cadDivisor;
                    if ((signoMenos != null) && (Digitos.IndexOf(numeroText[0]) > -1)) numeroText = '-' + numeroText;
                }

                // Tratamiento del formato con blancos para los miles y los millones.
                cadAux = cadDivisor;
                cadAux1 = cadDivisor;
                blancosMal = false;
                tieneBlancos = false;
                while (cadAux.LastIndexOf(' ') > -1)
                {
                    tieneBlancos = true;
                    int distancia = cadAux.Length - cadAux.LastIndexOf(' ');
                    if (distancia == 4) cadDivisor = cadDivisor.Substring(0, cadDivisor.LastIndexOf(' ')) + cadDivisor.Substring(cadDivisor.LastIndexOf(' ') + 1);
                    else
                    {
                        blancosMal = true;
                        break; //return 3; //El número es incorrecto.
                    }
                    cadAux = cadAux.Substring(0, cadAux.LastIndexOf(' '));
                }

                // Si los blancos en los miles y millones están mal colocados se arreglan.
                if ((blancosMal) || ((cadAux.Length > 3) && (tieneBlancos)) || ((tieneBlancos == false) && (cadAux.Length > 4)))
                {
                    cadDivisor = "";
                    cadAux = "";
                    int dist = 0;
                    for (int i = cadAux1.Length - 1; i >= 0; i--)
                    {
                        if (cadAux1[i] != ' ')
                        {
                            if (dist++ % 3 == 0) cadAux = " " + cadAux;
                            cadAux = cadAux1[i] + cadAux;
                            cadDivisor = cadAux1[i] + cadDivisor;
                        }
                    }
                    numeroText = numeroText.Substring(0, numeroText.IndexOf('/') + 1) + cadAux;
                    if (signoMenos != null) numeroText = '-' + numeroText;
                }

                if (cadDivisor[0] == '0') tieneCero = true;
                cadDivisor = cadDivisor.TrimStart('0');
                if ((cadDivisor.Length == 0) && (tieneCero))
                {
                    cadDivisor = "0";
                    signoMenos = null;
                }

                if (cadDivisor.Length > 125) return 4; ; // El Divisor es demasiado grande.
                for (int i = 0; i < cadDivisor.Length; i++)
                {
                    if (Digitos.IndexOf(cadDivisor[i]) == -1) return 3; //El número es incorrecto.
                }
            }
            // Tratamiento de la notación científica 5,93483475E+23
            if (expRistra != null)
            {
                tieneCero = false;
                if (expRistra.IndexOf('0') > -1) tieneCero = true;

                int exp = 0;
                if (expRistra[0] == '-')
                {
                    expRistra = expRistra.Substring(1);
                    expRistra = expRistra.TrimStart('0');
                    if ((expRistra == null) && (tieneCero)) expRistra = "0";
                    if (expRistra.Length > 3) return 5; // El exponente es demasiado grande
                    for (int i = 0; i < expRistra.Length; i++)
                    {
                        if (Digitos.IndexOf(expRistra[i]) == -1) return 3; //El número es incorrecto.
                    }
                    exp = System.Convert.ToInt32(expRistra);
                    if (cadParteEntera != "0")
                    {
                        if (cadParteEntera.Length <= exp)
                        {
                            cadParteDecimal = cadParteEntera + cadParteDecimal;
                            exp -= cadParteEntera.Length;
                            cadParteEntera = "0";
                            if (exp > 0) cadParteDecimal = cadParteDecimal.PadLeft(cadParteDecimal.Length + exp, '0');
                            exp = 0;
                        }
                        else
                        {
                            cadParteDecimal = cadParteEntera.Substring(cadParteEntera.Length - exp) + cadParteDecimal;
                            cadParteEntera = cadParteEntera.Substring(0, cadParteEntera.Length - exp);
                            exp = 0;
                        }
                    }
                    else
                    {
                        if (cadParteDecimal != null) cadParteDecimal = cadParteDecimal.PadLeft(cadParteDecimal.Length + exp, '0');
                    }
                }
                else
                {
                    if (expRistra[0] == '+') expRistra = expRistra.Substring(1);
                    expRistra = expRistra.TrimStart('0');
                    if ((expRistra == null) && (tieneCero)) expRistra = "0";
                    if (expRistra.Length > 3) return 5; // El exponente es demasiado grande
                    for (int i = 0; i < expRistra.Length; i++)
                    {
                        if (Digitos.IndexOf(expRistra[i]) == -1) return 3; //El número es incorrecto.
                    }
                    exp = System.Convert.ToInt32(expRistra);
                    if (cadParteDecimal != null)
                    {
                        if (cadParteDecimal.Length <= exp)
                        {
                            cadParteEntera += cadParteDecimal;
                            exp -= cadParteDecimal.Length;
                            cadParteDecimal = null;
                            if (exp > 0) cadParteEntera = cadParteEntera.PadRight(cadParteEntera.Length + exp, '0');
                            exp = 0;
                        }
                        else
                        {
                            cadParteEntera += cadParteDecimal.Substring(0, exp);
                            cadParteDecimal = cadParteDecimal.Substring(exp);
                            exp = 0;
                        }
                    }
                    else
                    {
                        if (cadParteEntera != "0") cadParteEntera = cadParteEntera.PadRight(cadParteEntera.Length + exp, '0');
                    }
                }
                cadParteEntera = cadParteEntera.TrimStart('0');
                if (cadParteEntera == "") cadParteEntera = "0";
                if (cadParteDecimal != null) cadParteDecimal = cadParteDecimal.TrimEnd('0');
                if (cadParteEntera.Length > 126) return 5; //El exponente es demasiado grande.
                if ((cadParteDecimal != null) && (cadParteDecimal.Length > 125)) return 5; ; //El exponente es demasiado grande.
            }
        }
        catch (Exception)
        {
            return 3;
        }

        return 0;
    }

}