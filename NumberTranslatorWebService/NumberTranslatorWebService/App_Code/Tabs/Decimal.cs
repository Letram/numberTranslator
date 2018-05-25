using System;
using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

/// <summary>
/// Descripción breve de Class1
/// </summary>
public class Decimal
{
    Cardinal cardinal;
    public Decimal()
    {
        cardinal = new Cardinal();
    }

    public ArrayList getDecimalTab(String nonDecimalPart, String decimalPart, Boolean isNegative = false)
    {

        ArrayList res = new ArrayList();
        if (decimalPart == "") return res;
        ArrayList nonDecimalTranslation = cardinal.getCardinalNumber(nonDecimalPart, isNegative);
        ArrayList decimalPartTranlation = cardinal.getCardinalNumber(decimalPart);

        String beforeComma = nonDecimalTranslation[0].ToString();
        String afterComma = decimalPartTranlation[0].ToString();

        String decimalNumber = "";
        Match regex = Regex.Match(decimalPart, @"\b(0+)\b");
        if (regex.Success)
        {
            decimalNumber = beforeComma;
        }
        else
        {
            regex = Regex.Match(decimalPart, @"\b(0+)(\d)+\b");
            if (regex.Success)
            {
                decimalNumber = beforeComma + "-virgule-";
                for (int i = 0; i < regex.Groups[1].Value.Length; i++)
                {
                    decimalNumber += "zéro-";
                }
                decimalNumber += afterComma;
            }
            else
            {
                decimalNumber = beforeComma + "-virgule-" + afterComma;
            }
        }

        res.Add(Resources.Resource.decimalTitle);
        res.Add(Resources.Resource.decimalDescription);
        res.Add(Resources.Resource.decimalNumberDescription);
        res.Add(decimalNumber);
        res.Add("@" + decimalNumber.Replace('-', ' '));

        if (nonDecimalTranslation.Count > 1 && decimalPartTranlation.Count > 1)
        {
            res.Add(Resources.Resource.other);
            for (int i = 0; i < nonDecimalTranslation.Count; i++)
            {
                for (int j = 0; j < decimalPartTranlation.Count; j++)
                {
                    if (i == j && j == 0) continue;
                    res.Add(nonDecimalTranslation[i].ToString().Trim() + "-virgule-" + decimalPartTranlation[j].ToString().Trim());
                }
            }
        }
        return res;
    }

    private string pluralize(string v, string decimalPart)
    {
        if (decimalPart == "1") return v;
        return v + "s";
    }
}