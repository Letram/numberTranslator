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
        String minusSign = "";
        String nonDecimal = "";
        String decimalPart = "";
        String divider = "";
        int exit = TratamientoInicial.InitialTratement(ref unformattedNumber, ref minusSign, ref nonDecimal, ref decimalPart, ref divider);

        Fraction fractionNumberTranslation= new Fraction();
        Cardinal cardinalNumberTranslation= new Cardinal();
        Ordinal ordinalNumberTranslation= new Ordinal();
        Negative negativeNumberTranslation= new Negative();
        Decimal decimalNumberTranslation = new Decimal();

        ArrayList result = new ArrayList();

        //result.Add("Numero: " + unformattedNumber.Replace(" ", ""));
        //result.Add("Menos: " + minusSign);
        //result.Add("Parte entera: " + nonDecimal);
        //result.Add("Parte decimal: " + decimalPart);
        //result.Add("Divisor: " + divider);
        //result.Add(exit);
        //return result;

        if (minusSign.Equals(""))
        {
            Thread cardinalThread = new Thread(() => result.Add(cardinalNumberTranslation.getCardinalTab(nonDecimal)));
            Thread ordinalThread = new Thread(() => result.Add(ordinalNumberTranslation.getOrdinalNumberTab(nonDecimal)));
            Thread fractionThread = new Thread(() => result.Add(fractionNumberTranslation.getFractionTab(nonDecimal, divider)));
            Thread decimalThread = new Thread(()=>result.Add(decimalNumberTranslation.getDecimalTab(nonDecimal, decimalPart)));



            if (!decimalPart.Equals(""))
            {
                decimalThread.Start();
                decimalThread.Join();
            }
            else
            {
                cardinalThread.Start();
                ordinalThread.Start();
                fractionThread.Start();

                cardinalThread.Join();
                ordinalThread.Join();
                fractionThread.Join();
            }


        }
        else return negativeNumberTranslation.getNegativeTabs(nonDecimal, decimalPart, divider);
        
        return result;
        
    }
}
