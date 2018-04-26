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
    public Decimal()
    {
        cardinal = new Cardinal();
    }

    public ArrayList getDecimalTab(String nonDecimal, String decimalPart)
    {
        ArrayList decimalTab = new ArrayList();
        Boolean negativeCardinal = nonDecimal[0] == '-'? true : false;
        Boolean negativeOrdinal = decimalPart[0] == '-'? true : false;
        ArrayList decimalPartTranslated = new ArrayList();
        ArrayList nonDecimalPartTranslated = new ArrayList();

        if((negativeCardinal && negativeOrdinal) || (!negativeCardinal && !negativeOrdinal))
        {
            nonDecimalPartTranslated = cardinal.getCardinalNumber(nonDecimal);
            decimalPartTranslated = cardinal.getCardinalNumber(decimalPart);
        }else if(negativeOrdinal || negativeCardinal)
        {
            nonDecimalPartTranslated = cardinal.getCardinalNumber(nonDecimal, true);
            decimalPartTranslated = cardinal.getCardinalNumber(decimalPart);
        }


        decimalTab.Add("#Decimal");
        decimalTab.Add("Los números decimales expresan una cantidad en relación con la serie de los números naturales más una fracción de una unidad separada por una coma o un punto.");
        decimalTab.Add("#Número traducido a texto decimal");
        decimalTab.Add(nonDecimalPartTranslated[0].ToString().Trim() + " virgule " + decimalPartTranslated[0].ToString().Trim());
        decimalTab.Add("&Valor numérico: ");
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
}