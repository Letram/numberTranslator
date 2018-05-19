using System;
using System.Collections;
using System.Globalization;
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
        if (numerator.Equals("0") && (denominator.Equals(""))) return new ArrayList();
        ArrayList fractionaryNumberConverted = getFractionaryNumber(numerator, denominator, isNegative);
        ArrayList fractionaryTab = new ArrayList();
        fractionaryTab.Add(Resources.Resource.fractional);
        fractionaryTab.Add(Resources.Resource.fractionalDescription);
        fractionaryTab.Add(Resources.Resource.numberFractionalDescription);
        fractionaryTab.Add(fractionaryNumberConverted[0].ToString());
        fractionaryTab.Add("@" + fractionaryNumberConverted[0].ToString().Replace('-', ' '));
        fractionaryTab.Add(Resources.Resource.value);
        if(denominator == "")
        {
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";
            if (isNegative) fractionaryTab.Add("-" + (double.Parse("1") / double.Parse(numerator)).ToString(nfi));
            else fractionaryTab.Add((double.Parse("1") / double.Parse(numerator)).ToString(nfi));
        }
        else
        {
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";
            if(isNegative) fractionaryTab.Add("-"+(double.Parse(numerator) / double.Parse(denominator)).ToString(nfi));
            else fractionaryTab.Add((double.Parse(numerator) / double.Parse(denominator)).ToString(nfi));
        }
        if (fractionaryNumberConverted.Count > 1)
        {

            fractionaryTab.Add(Resources.Resource.other);
            for (int i = 1; i < fractionaryNumberConverted.Count; i++)
            {
                fractionaryTab.Add(fractionaryNumberConverted[i].ToString());

            }
        }
        return fractionaryTab;
    }

    private ArrayList getFractionaryNumber(string numerator, string denominator, Boolean isNegative = false)
    {
        ArrayList fractionaryNumber = new ArrayList();
        if (numerator.Equals("0") && !denominator.Equals(""))
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
            return justOne(numerator, isNegative, true);
        }
        if (numerator.Equals(""))
        {
            return justOne(denominator, isNegative);
        }
        if (denominator.Equals("1"))
        {
            fractionaryNumber.Add(cardinal.getCardinalNumber(numerator, isNegative)[0].ToString().Trim());
            return fractionaryNumber;
        }
        if (numerator.Equals(denominator)) return justOne("1", isNegative);

        String fraction = "";
        if(!numerator.Equals("") && !denominator.Equals(""))
        {
            fraction = cardinal.getCardinalNumber(numerator, isNegative)[0].ToString().Trim() + " " + checkForSpecialDenominator(denominator);
            fractionaryNumber.Add(fraction);
        }
        return fractionaryNumber;
    }

    private string checkForSpecialDenominator(string denominator)
    {
        switch (denominator)
        {
            case "2":
                return "demi";
            case "3":
                return "tiers";
            case "4":
                return "quarts";
            default:
                return pluralize(ordinal.getOrdinalNumber(denominator, true)[0].ToString().Trim());
        }
    }

    private string pluralize(string denominator)
    {
        if (!denominator[denominator.Length-1].Equals('s')) return denominator + "s";
        else return denominator;
    }

    private ArrayList justOne(String v, Boolean isNegative = false, Boolean isDecimal = false)
    {
        ArrayList res = new ArrayList();
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
                String prefix = "";
                if (!isDecimal) prefix = "un-";
                res.Add(prefix + ordinal.getOrdinalNumber(v)[0].ToString().Trim());
                break;
        }
        if (isNegative)
        {
            for (int i = 0; i < res.Count; i++)
            {
                res[i] = "moins " + res[i].ToString().Trim();
            }
        }

        return res;
    }
}