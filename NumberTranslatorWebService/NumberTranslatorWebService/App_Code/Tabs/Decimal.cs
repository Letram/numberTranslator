﻿using System;
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

    public ArrayList getDecimalTab(String nonDecimal, String decimalPart)
    {
        ArrayList decimalTab = new ArrayList();

        bool negativeCardinal = false;
        bool negativeDecimal = false;
        bool isDecimal = false;
        if (nonDecimal != "")
        {
            negativeCardinal = nonDecimal[0] == '-' ? true : false;
            if ((negativeCardinal && (nonDecimal.Substring(1) == "0")) || !negativeCardinal && (nonDecimal == "0")) isDecimal = true;
        }

        if(decimalPart != "")
            negativeDecimal = decimalPart[0] == '-'? true : false;

        ArrayList decimalPartTranslated = new ArrayList();
        ArrayList nonDecimalPartTranslated = new ArrayList();

        if((negativeCardinal && negativeDecimal) || (!negativeCardinal && !negativeDecimal))
        {
            nonDecimalPartTranslated = cardinal.getCardinalNumber(nonDecimal);
            decimalPartTranslated = cardinal.getCardinalNumber(decimalPart);
        }else if(negativeDecimal || negativeCardinal){
            nonDecimalPartTranslated = cardinal.getCardinalNumber(nonDecimal, true);
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
                    decimalPartTranslated[i] = decimalPartTranslated[i] + " " + pluralize(ordinal.ordinalTreatment(Scales.getShortScale()[decimalPart.Length / 3], decimalPart), decimalPart);
                    System.Diagnostics.Debug.WriteLine("=> " + decimalPartTranslated[i].ToString());
                    break;
            }
        }

        decimalTab.Add("#Decimal");
        decimalTab.Add("Los números decimales expresan una cantidad en relación con la serie de los números naturales más una fracción de una unidad separada por una coma o un punto.");
        decimalTab.Add("#Número traducido a texto decimal");
        if (decimalPartTranslated.Count > 0)
            decimalTab.Add(nonDecimalPartTranslated[0].ToString().Trim() + " virgule " + decimalPartTranslated[0].ToString().Trim());
        else
            decimalTab.Add(nonDecimalPartTranslated[0].ToString().Trim());
        decimalTab.Add("#Valor numérico: ");
        decimalTab.Add(nonDecimal + "." + decimalPart);
        if (nonDecimalPartTranslated.Count > 1 && decimalPartTranslated.Count > 1)
        {
            decimalTab.Add("&&Otras versiones:");
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