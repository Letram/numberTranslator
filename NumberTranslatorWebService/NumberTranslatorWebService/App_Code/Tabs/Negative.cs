using System;
using System.Collections;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
        ArrayList taskList = new ArrayList();

        

        if (divider.Equals("") && decimalPart.Equals(""))
        {
            taskList.Add(new Task(() => negativeTabs.Add(cardinal.getCardinalTab(nonDecimal, true))));
        }
        else if(decimalPart.Equals("") && !divider.Equals(""))
        {
            taskList.Add(new Task(() => negativeTabs.Add(fraction.getFractionTab(nonDecimal, divider, true))));
            String unformattedAux = (double.Parse(nonDecimal) / double.Parse(divider)).ToString();
            Boolean minus = false;
            String nonDecimalAux = "";
            String decimalPartAux = "";
            String dividerAux = "";
            int decimalTabFromFraction = TratamientoInicialRegEx.tratamientoInicialRegEx(ref unformattedAux, ref minus, ref nonDecimalAux, ref decimalPartAux, ref dividerAux);
            taskList.Add(new Task(() => negativeTabs.Add(decimalTab.getDecimalTab(nonDecimalAux, decimalPartAux, true))));
        }
        else
        {
            taskList.Add(new Task(() => negativeTabs.Add(decimalTab.getDecimalTab(nonDecimal, decimalPart, true))));
        }

        foreach (Task task in taskList) task.Start();
        Task[] taskArr = (Task[])taskList.ToArray(typeof(Task));
        Task.WaitAll(taskArr);

        return negativeTabs;
    }
}