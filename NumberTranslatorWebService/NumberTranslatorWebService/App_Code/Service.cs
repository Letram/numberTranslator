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

    public ArrayList getTabs(string number, String language)
    {
        Handler errorHandler = Handler.getInstance();
        ArrayList result = new ArrayList();
        if(language.Contains("fr") || language.Contains("es"))
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(language, false);
        }
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
        Fraction fractionNumberTranslation= new Fraction();
        Cardinal cardinalNumberTranslation= new Cardinal();
        Ordinal ordinalNumberTranslation= new Ordinal();
        Negative negativeNumberTranslation= new Negative();
        Decimal decimalNumberTranslation = new Decimal();
        Multiplicative multiplicativeNumberTranslation = new Multiplicative();
        Birth_Count birthCountNumberTranslation = new Birth_Count();


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
