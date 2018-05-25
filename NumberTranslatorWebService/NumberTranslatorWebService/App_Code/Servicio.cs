using System;
using System.Collections;
using System.Linq;
using System.Threading;
using System.Web;

/// <summary>
/// Descripción breve de Class1
/// </summary>
public class Servicio
{
    private Fraction fractionNumberTranslation;
    private Cardinal cardinalNumberTranslation;
    private Ordinal ordinalNumberTranslation;
    private Negative negativeNumberTranslation;
    private Decimal decimalNumberTranslation;
    private Multiplicative multiplicativeNumberTranslation;
    private Birth_Count birthCountNumberTranslation;
    public Servicio(){}
    public ArrayList getTabs(String number)
    {
        Handler errorHandler = Handler.getInstance();
        ArrayList result = new ArrayList();
        if (number.Equals("")) return new ArrayList();
        String unformattedNumber = number;
        Boolean isNegative = false;
        String nonDecimal = "";
        String decimalPart = "";
        String divider = "";
        int exit = TratamientoInicialRegEx.tratamientoInicialRegEx(ref unformattedNumber, ref isNegative, ref nonDecimal, ref decimalPart, ref divider);
        if (exit != 0)
        {
            result.Add(errorHandler.errorHandler(exit));
            return result;
        }
        fractionNumberTranslation = new Fraction();
        cardinalNumberTranslation = new Cardinal();
        ordinalNumberTranslation = new Ordinal();
        negativeNumberTranslation = new Negative();
        decimalNumberTranslation = new Decimal();
        multiplicativeNumberTranslation = new Multiplicative();
        birthCountNumberTranslation = new Birth_Count();


        if (!isNegative)
        {
            ArrayList threadList = new ArrayList();
            if (!decimalPart.Equals("") || !divider.Equals(""))
            {
                if (!divider.Equals(""))
                {
                    threadList.Add(new Thread(() => result.Add(fractionNumberTranslation.getFractionTab(nonDecimal, divider))));
                    String unformattedAux = (double.Parse(nonDecimal) / double.Parse(divider)).ToString();
                    Boolean minus = false;
                    String nonDecimalAux = "";
                    String decimalPartAux = "";
                    String dividerAux = "";
                    int decimalTabFromFraction = TratamientoInicialRegEx.tratamientoInicialRegEx(ref unformattedAux, ref minus, ref nonDecimalAux, ref decimalPartAux, ref dividerAux);
                    threadList.Add(new Thread(() => result.Add(decimalNumberTranslation.getDecimalTab(nonDecimalAux, decimalPartAux))));
                }
                else threadList.Add(new Thread(() => result.Add(decimalNumberTranslation.getDecimalTab(nonDecimal, decimalPart))));
            }
            else
            {
                threadList.Add(new Thread(() => result.Add(cardinalNumberTranslation.getCardinalTab(nonDecimal))));
                threadList.Add(new Thread(() => result.Add(ordinalNumberTranslation.getOrdinalNumberTab(nonDecimal))));
                threadList.Add(new Thread(() => result.Add(fractionNumberTranslation.getFractionTab(nonDecimal, divider))));
                threadList.Add(new Thread(() => result.Add(multiplicativeNumberTranslation.getMultiplicativeTab(nonDecimal))));
                threadList.Add(new Thread(() => result.Add(birthCountNumberTranslation.getBirthCountTab(nonDecimal))));
            }
            foreach (Thread thread in threadList)
            {
                thread.Start();
            }
            foreach (Thread thread in threadList)
            {
                thread.Join();
            }
        }

        else return negativeNumberTranslation.getNegativeTabs(nonDecimal, decimalPart, divider);

        return result;
    }
}