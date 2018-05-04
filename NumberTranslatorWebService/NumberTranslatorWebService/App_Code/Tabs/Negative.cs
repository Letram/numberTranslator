using System;
using System.Collections;
using System.Linq;
using System.Threading;
using System.Web;

/// <summary>
/// Descripción breve de Class1
/// </summary>
public class Negative
{
    private Cardinal cardinal;
    private Fraction fraction;
    private Decimal decimalTab;
    public Negative(){
        cardinal = new Cardinal();
        fraction = new Fraction();
        decimalTab = new Decimal();
    }

    public ArrayList getNegativeTabs(String nonDecimal, String decimalPart, String divider)
    {
        ArrayList negativeTabs = new ArrayList();
        ArrayList threadList = new ArrayList();

        

        if (!divider.Equals(""))
        {
            threadList.Add(new Thread(() => negativeTabs.Add(cardinal.getCardinalTab(nonDecimal, true))));
        }
        else
        {
            threadList.Add(new Thread(() => negativeTabs.Add(fraction.getFractionTab(nonDecimal, divider, true))));
            String unformattedAux = (double.Parse(nonDecimal) / double.Parse(divider)).ToString();
            Boolean minus = false;
            String nonDecimalAux = "";
            String decimalPartAux = "";
            String dividerAux = "";
            int decimalTabFromFraction = TratamientoInicialRegEx.tratamientoInicialRegEx(ref unformattedAux, ref minus, ref nonDecimalAux, ref decimalPartAux, ref dividerAux);
            threadList.Add(new Thread(() => negativeTabs.Add(decimalTab.getDecimalTab(nonDecimalAux, decimalPartAux, true))));
        }


        foreach (Thread thread in threadList) thread.Start();
        foreach (Thread thread in threadList) thread.Join();

        return negativeTabs;
    }
}