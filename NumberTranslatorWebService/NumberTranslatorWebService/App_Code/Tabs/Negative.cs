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
    public Negative(){
        cardinal = new Cardinal();
        fraction = new Fraction();
    }

    public ArrayList getNegativeTabs(String nonDecimal, String decimalPart, String divider)
    {
        ArrayList negativeTabs = new ArrayList();
        Thread cardinalTab = new Thread(() => negativeTabs.Add(cardinal.getCardinalTab(nonDecimal, true)));
        Thread fractionTab = new Thread(() => negativeTabs.Add(fraction.getFractionTab(nonDecimal, divider, true)));

        
        cardinalTab.Start();
        cardinalTab.Join();
        if (!divider.Equals(""))
        {
            fractionTab.Start();
            fractionTab.Join();
        }


        return negativeTabs;
    }
}