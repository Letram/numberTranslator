using System;
using System.Collections;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
            ArrayList taskList = new ArrayList();
            if (!decimalPart.Equals("") || !divider.Equals(""))
            {
                if (!divider.Equals(""))
                {
                    taskList.Add(new Task(() => result.Add(fractionNumberTranslation.getFractionTab(nonDecimal, divider))));
                    String unformattedAux = (double.Parse(nonDecimal) / double.Parse(divider)).ToString();
                    Boolean minus = false;
                    String nonDecimalAux = "";
                    String decimalPartAux = "";
                    String dividerAux = "";
                    int decimalTabFromFraction = TratamientoInicialRegEx.tratamientoInicialRegEx(ref unformattedAux, ref minus, ref nonDecimalAux, ref decimalPartAux, ref dividerAux);
                    taskList.Add(new Task(() => result.Add(decimalNumberTranslation.getDecimalTab(nonDecimalAux, decimalPartAux))));
                }
                else
                    taskList.Add(new Task(() => result.Add(decimalNumberTranslation.getDecimalTab(nonDecimal, decimalPart))));
            }
            else
            {
                taskList.Add(new Task(() => result.Add(cardinalNumberTranslation.getCardinalTab(nonDecimal))));
                taskList.Add(new Task(() => result.Add(ordinalNumberTranslation.getOrdinalNumberTab(nonDecimal))));
                taskList.Add(new Task(() => result.Add(fractionNumberTranslation.getFractionTab(nonDecimal, divider))));
                taskList.Add(new Task(() => result.Add(multiplicativeNumberTranslation.getMultiplicativeTab(nonDecimal))));
                taskList.Add(new Task(() => result.Add(birthCountNumberTranslation.getBirthCountTab(nonDecimal))));
            }

            foreach(Task task in taskList)
            {
                task.Start();
            }
            Task[] taskArr= (Task[])taskList.ToArray(typeof(Task));
            Task.WaitAll(taskArr);
        }

        else return negativeNumberTranslation.getNegativeTabs(nonDecimal, decimalPart, divider);

        return result;
    }
}