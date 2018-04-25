using System;
using System.Collections;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Class1
/// </summary>
public class Fraction
{
    String [] bigNumbersShortScale = Scales.getShortScale();
    String[] bigNumbersLargeScale = Scales.getLargeScale();
    Cardinal cardinal;
    Ordinal ordinal;

    public Fraction(){
        cardinal = new Cardinal();
        ordinal = new Ordinal();
    }

    public ArrayList getFractionTab(String numerator, String denominator, Boolean isNegative = false)
    {
        ArrayList fractionaryNumberConverted = getFractionaryNumber(numerator, denominator, isNegative);
        ArrayList fractionaryTab = new ArrayList();
        fractionaryTab.Add("#Fraccionario");
        fractionaryTab.Add("Los números fraccionarios expresan división de un todo en partes y designan las fracciones iguales en que se ha dividido la unidad.");
        fractionaryTab.Add("#Número traducido a texto fraccional");
        fractionaryTab.Add(fractionaryNumberConverted[0].ToString());
        if (fractionaryNumberConverted.Count > 1)
        {
            fractionaryTab.Add("&&Otras versiones:");
            for (int i = 1; i < fractionaryNumberConverted.Count; i++)
            {
                fractionaryTab.Add(fractionaryNumberConverted[i].ToString());
            }
        }
        fractionaryTab.Add(isNegative);
        return fractionaryTab;
    }

    private ArrayList getFractionaryNumber(string numerator, string denominator, Boolean isNegative = false)
    {
        ArrayList fractionaryNumber = new ArrayList();
        if (numerator.Equals("0"))
        {
            fractionaryNumber.Add(cardinal.getCardinalNumber(numerator)[0].ToString().Trim());
            return fractionaryNumber;
        }
        if (denominator.Equals("0"))
        {
            fractionaryNumber.Add("Infini");
            fractionaryNumber.Add("Indéterminé");
            return fractionaryNumber;
        }
        if (numerator.Equals("1") && !denominator.Equals(""))
        {
            return justOne(denominator, isNegative);
        }
        if (denominator.Equals(""))
        {
            return justOne(numerator, isNegative);
        }
        if (numerator.Equals(""))
        {
            return justOne(denominator, isNegative);
        }
        
        String fraction = "";
        if(!numerator.Equals("") && !denominator.Equals(""))
            fraction = cardinal.getCardinalNumber(numerator, isNegative)[0].ToString().Trim() + " " + ordinal.getOrdinalNumber(denominator)[0].ToString().Trim();
        fractionaryNumber.Add(pluralize(fraction.Trim()));
        return fractionaryNumber;
    }

    private object pluralize(string v)
    {
        if (!v[v.Length - 1].Equals("s")) v = v + "s";
        return v;
    }

    private ArrayList justOne(String v, Boolean isNegative = false)
    {
        ArrayList res = new ArrayList();
        res.Add(isNegative);
        switch (v)
        {
            case "1":
                res.Add("une unité");
                res.Add("l'unité");
                break;
            case "2":
                res.Add("un demi");
                res.Add("une demie");
                break;
            case "3":
                res.Add("un tiers");
                break;
            case "4":
                res.Add("un quart");
                break;
            default:
                res.Add("un " + ordinal.getOrdinalNumber(v)[0].ToString().Trim());
                break;
        }
        if (isNegative)
        {
            for (int i = 0; i < res.Count; i++)
            {
                res[i] = "<b>moins</b> " + res[i].ToString().Trim();
            }
        }

        return res;
    }
}