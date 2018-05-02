using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading;

// NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
public class Service : IService
{

    public string GetData(int value)
	{
		return string.Format("You entered: {0}", value);
	}

	public CompositeType GetDataUsingDataContract(CompositeType composite)
	{
		if (composite == null)
		{
			throw new ArgumentNullException("composite");
		}
		if (composite.BoolValue)
		{
			composite.StringValue += "Suffix";
		}
		return composite;
	}


    private ArrayList getNegativeNumberTab(String number)
    {
        return new ArrayList();
        /*
        ArrayList res = new ArrayList();
        res.Add("#Negativo");
        res.Add("Los números negativos expresan pérdidas, deudas, disminuciones, etc. en relaciones con los números naturales.");
        res.Add("#Número traducido a texto cardinal.");
        res.Add("moins " + getCardinalNumber(number.Substring(1))[0].ToString());
        return res;
        */
    }

    public ArrayList getTabs(string number)
    {
        if (number.Equals("")) return new ArrayList();
        String unformattedNumber = number;
        Boolean isNegative = false;
        String nonDecimal = "";
        String decimalPart = "";
        String divider = "";
        //int exit = TratamientoInicial.InitialTratement(ref unformattedNumber, ref minusSign, ref nonDecimal, ref decimalPart, ref divider, ref isNegative);
        int exit = TratamientoInicialRegEx.tratamientoInicialRegEx(ref unformattedNumber, ref isNegative, ref nonDecimal, ref decimalPart, ref divider);

        Fraction fractionNumberTranslation= new Fraction();
        Cardinal cardinalNumberTranslation= new Cardinal();
        Ordinal ordinalNumberTranslation= new Ordinal();
        Negative negativeNumberTranslation= new Negative();
        Decimal decimalNumberTranslation = new Decimal();
        Multiplicative multiplicativeNumberTranslation = new Multiplicative();
        Birth_Count birthCountNumberTranslation = new Birth_Count();

        ArrayList result = new ArrayList();

        //result.Add("Numero: " + unformattedNumber.Replace(" ", ""));
        //result.Add("Menos: " + isNegative);
        //result.Add("Parte entera: " + nonDecimal);
        //result.Add("Parte decimal: " + decimalPart);
        //result.Add("Divisor: " + divider);
        //result.Add(exit);
        //return result;

        if (!isNegative)
        {
            ArrayList threadList = new ArrayList();
            if (!decimalPart.Equals("") || !divider.Equals(""))
            {
                if (!divider.Equals(""))
                {
                    threadList.Add(new Thread(() => result.Add(fractionNumberTranslation.getFractionTab(nonDecimal, divider))));
                    String unformattedAux = (float.Parse(nonDecimal) / float.Parse(divider)).ToString();
                    Boolean minus = false;
                    String nonDecimalAux = "";
                    String decimalPartAux = "";
                    String dividerAux = "";
                    int decimalTabFromFraction = TratamientoInicialRegEx.tratamientoInicialRegEx(ref unformattedAux, ref minus, ref nonDecimalAux, ref decimalPartAux, ref dividerAux);
                    /*
                    result.Add("Numero: " + unformattedAux.Replace(" ", ""));
                    result.Add("Menos: " + minus);
                    result.Add("Parte entera: " + nonDecimalAux);
                    result.Add("Parte decimal: " + decimalPartAux);
                    result.Add("Divisor: " + divider);
                    result.Add(exit);
                    return result;
                    */
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
