using System;
using System.Collections;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Class1
/// </summary>
public class Multiplicative
{
    Cardinal cardinal;
    public Multiplicative()
    {
        cardinal = new Cardinal();
    }

    private ArrayList getMultiplicativeNumber(String number)
    {
        double aux = double.Parse(number);
        ArrayList res = new ArrayList();
        String numberTranslated = cardinal.getCardinalNumber(number)[0].ToString();
        if(aux == 1)
        {
            numberTranslated += "e";
        }
        numberTranslated += " fois";

        res.Add(numberTranslated);
        if(aux < 10)
        {
            res.Add(Scales.getMultiplicativeNumbers()[int.Parse(aux.ToString())]);
        }
        return res;
    }

    public ArrayList getMultiplicativeTab(String number)
    {
        double aux = double.Parse(number);
        if (aux == 0) return new ArrayList();
        ArrayList multiplicativeNumber = getMultiplicativeNumber(number);
        ArrayList multiplicativeTab = new ArrayList();
        multiplicativeTab.Add(Resources.Resource.multiplicative);
        multiplicativeTab.Add(Resources.Resource.multiplicativeDescription);
        multiplicativeTab.Add(Resources.Resource.numberMultiplicativeDescription);
        multiplicativeTab.Add(multiplicativeNumber[0].ToString());
        if (multiplicativeNumber.Count > 1)
        {
            multiplicativeTab.Add(Resources.Resource.other);
            for (int i = 1; i < multiplicativeNumber.Count; i++)
            {
                multiplicativeTab.Add(multiplicativeNumber[i].ToString());
            }
        }
        return multiplicativeTab;
    }
}