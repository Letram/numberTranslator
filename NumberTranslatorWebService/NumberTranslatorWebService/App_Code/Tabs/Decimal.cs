using System;
using System.Collections;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Class1
/// </summary>
public class Decimal
{
    Cardinal cardinal;
    Ordinal ordinal;
    public Decimal()
    {
        cardinal = new Cardinal();
        ordinal = new Ordinal();
    }

    public ArrayList getDecimalTab(String nonDecimalPart, String decimalPart, Boolean isNegative = false)
    {
        ArrayList decimalTab = new ArrayList();

        bool negativeCardinal = false;
        bool negativeDecimal = false;
        bool isDecimal = false;

        if (nonDecimalPart != "")
        {
            negativeCardinal = nonDecimalPart[0] == '-' ? true : false;
            if (nonDecimalPart == "0") isDecimal = true;
        }

        if (decimalPart != "")
            negativeDecimal = decimalPart[0] == '-'? true : false;

        ArrayList decimalPartTranslated = new ArrayList();
        ArrayList nonDecimalPartTranslated = new ArrayList();

        if((negativeCardinal && negativeDecimal) || (!negativeCardinal && !negativeDecimal))
        {
            nonDecimalPartTranslated = cardinal.getCardinalNumber(nonDecimalPart);
            decimalPartTranslated = cardinal.getCardinalNumber(decimalPart);
        }else if(negativeDecimal || negativeCardinal){
            nonDecimalPartTranslated = cardinal.getCardinalNumber(nonDecimalPart, true);
            decimalPartTranslated = cardinal.getCardinalNumber(decimalPart);
        }
        for (int i = 0; i < decimalPartTranslated.Count; i++)
        {
            switch (decimalPart.Length)
            {
                case 1:
                    decimalPartTranslated[i] = decimalPartTranslated[i] + pluralize(" dixième", decimalPart);
                    break;
                case 2:
                    decimalPartTranslated[i] = decimalPartTranslated[i] + pluralize(" centième", decimalPart);
                    break;
                default:
                    string preffix = "";
                    switch (Scales.GetMask(decimalPart.Length))
                    {
                        case 0:
                            break;
                        case 1:
                            preffix = "cent-";
                            break;
                        case 2:
                            preffix = "dix-";
                            break;
                    }
                    decimalPartTranslated[i] = decimalPartTranslated[i] + " " + preffix + pluralize(ordinal.ordinalTreatment(Scales.getShortScale()[decimalPart.Length / 3], decimalPart), decimalPart);
                    break;
            }
        }

        decimalTab.Add(Resources.Resource.decimalTitle);
        decimalTab.Add(Resources.Resource.decimalDescription);
        decimalTab.Add(Resources.Resource.decimalNumberDescription);
        if (isNegative) nonDecimalPartTranslated[0] = "moins " + nonDecimalPartTranslated[0].ToString().Trim();
        if (decimalPartTranslated.Count > 0)
        {
            if (isDecimal)
            {
                decimalTab.Add(decimalPartTranslated[0].ToString().Trim());
                decimalTab.Add("@" + decimalPartTranslated[0].ToString().Trim());
            } else {
                decimalTab.Add(nonDecimalPartTranslated[0].ToString().Trim() + " virgule " + decimalPartTranslated[0].ToString().Trim());
                decimalTab.Add("@" + nonDecimalPartTranslated[0].ToString().Trim() + " virgule " + decimalPartTranslated[0].ToString().Trim());
            }
        }
        else
        {
            decimalTab.Add(nonDecimalPartTranslated[0].ToString().Trim());
            decimalTab.Add("@" + nonDecimalPartTranslated[0].ToString().Trim());
        }
        decimalTab.Add(Resources.Resource.value);
        if(isNegative) decimalTab.Add("-" + nonDecimalPart + "." + decimalPart);
        else decimalTab.Add(nonDecimalPart + "." + decimalPart);
        if (nonDecimalPartTranslated.Count > 1 && decimalPartTranslated.Count > 1)
        {
            decimalTab.Add(Resources.Resource.other);
            for (int i = 0; i < nonDecimalPartTranslated.Count; i++)
            {
                for(int j = 0; j < decimalPartTranslated.Count; j++)
                {
                    if (i == j && j == 0) continue;
                    decimalTab.Add(nonDecimalPartTranslated[i].ToString().Trim() + " virgule " + decimalPartTranslated[j].ToString().Trim());
                }
            }
        }

        return decimalTab;
    }

    private string pluralize(string v, string decimalPart)
    {
        if (decimalPart == "1") return v;
        return v + "s";
    }
}