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

    private String[] bigNumbersLongScale =
    {
        "mille",        
        "million",      
        "milliard",     
        "billion",      
        "billiard",     
        "trillion",     
        "trilliard",    
        "quadrillion",  
        "quadrilliard", 
        "quintillion",  
        "quintilliard", 
        "sixtillion",   
        "sixtilliard",  
        "septillion",   
        "septilliard",  
        "octillion",    
        "octilliard",   
        "nonillion",    
        "nonilliard",   
        "décillion",    
        "décilliard",   
        "undécillon",
        "undécillard",
        "duodécillon",
        "duodécillard",
        "trédécillon",
        "quattuordézillon",
        "quattuordécillard",
        "quindécillon",
        "quindécillard",
        "sexdécillon",
        "sexdécillard",
        "septendécillon",
        "septendécilliard",
        "octodécillion",
        "octodécilliard",
        "novemdécillion",
        "novemdécilliard",
        "vigintillon"
    };
    private String[] bigNumbersShortScale =
{
        "mille",
        "million",
        "billion",
        "trillion",
        "quadrillion",
        "quintillion",
        "sextillion",
        "septillion",
        "octillion",
        "nonillion",
        "decillion",
        "undecillion",
        "duodecillion",
        "trédécillon",
        "quatturdézillon",
        "quindécillon",
        "sexdécillon",
        "septendécillon",
        "octodécillon",
        "novemdécillon",
        "vigintillion",
        "unvigintillion",
        "duovigintillion",
        "trévigintillion",
        "quattuorvigintillion",
        "quinvigintillion",
        "sexvigintillion",
        "septenvigintillion",
        "octovigintillion",
        "novemvigintillion",
        "trigintillion",
        "untrigintillion",
        "duotrigintillion",
        "trétrigintillion",
        "quattuortrigintillion",
        "quintrigintillion",
        "sextrigintillion",
        "septentrigintillion",
        "octotrigintillion"
    };
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

    private ArrayList getCardinalNumber(string number)
    {
        ArrayList cardinalNumberArrayList = new ArrayList();
        StringBuilder numericValue = new StringBuilder(number);
        string aux = new string('0', GetMask(numericValue.Length));
        StringBuilder mask = new StringBuilder(aux);
        StringBuilder parsedNumber = mask.Append(numericValue);
        string longScaleNumber = "";
        string shortScaleNumber = "";

        //we parse the first group apart so we can add some more control to our translation process

        //tanto en LongScale(default) como ShortScale
        longScaleNumber = new LessThanAThousand(parsedNumber.ToString(parsedNumber.Length - 3, 3)).Translate();
        shortScaleNumber = longScaleNumber;
        int bigNumberIndex = 0;
        for (int i = parsedNumber.Length - 6; i >= 0; i -= 3)
        {
            string translatedGroup = new LessThanAThousand(parsedNumber.ToString(i, 3)).Translate();
            if (!translatedGroup.Equals(""))
            {
                if (bigNumberIndex == 0)
                {
                    if (translatedGroup.Equals("un"))
                    {
                        longScaleNumber = bigNumbersLongScale[bigNumberIndex] + " " + longScaleNumber;
                        shortScaleNumber = bigNumbersShortScale[bigNumberIndex] + " " + shortScaleNumber;
                    }
                    else
                    {
                        longScaleNumber = translatedGroup + " " + bigNumbersLongScale[bigNumberIndex] + " " + longScaleNumber;
                        shortScaleNumber = translatedGroup + " " + bigNumbersShortScale[bigNumberIndex] + " " + shortScaleNumber;
                    }
                }
                else
                {
                    if (!translatedGroup.Equals("un"))
                    {
                        longScaleNumber = translatedGroup + " " + bigNumbersLongScale[bigNumberIndex] + "s " + longScaleNumber;
                        shortScaleNumber = translatedGroup + " " + bigNumbersShortScale[bigNumberIndex] + "s " + shortScaleNumber;
                    }

                    else
                    {
                        longScaleNumber = translatedGroup + " " + bigNumbersLongScale[bigNumberIndex] + " " + longScaleNumber;
                        shortScaleNumber = translatedGroup + " " + bigNumbersShortScale[bigNumberIndex] + " " + shortScaleNumber;
                    }
                }
            }
            bigNumberIndex++;
        }
        bigNumberIndex = 0;
        cardinalNumberArrayList.Add(longScaleNumber.Trim());
        cardinalNumberArrayList.Add(shortScaleNumber.Trim());
        
        return cardinalNumberArrayList;
    }
    private int GetMask(int length)
    {
        int op = 3 - (length % 3);
        switch (op%3){
            case 1:
                return 1;
            case 2:
                return 2;
            case 3:
                return 0;
        }
        return 0;
    }
    public ArrayList getCardinalNumberTab(string number)
    {
        ArrayList cardinalNumberConverted = getCardinalNumber(number);
        ArrayList cardinalTab = new ArrayList();
        cardinalTab.Add("#Cardinal");
        cardinalTab.Add("Los números cardinales expresan cantidad en relación con los números naturales.");
        cardinalTab.Add("#Número traducido a texto cardinal");
        cardinalTab.Add(cardinalNumberConverted[0].ToString());
        if(cardinalNumberConverted.Count > 1)
        {
            cardinalTab.Add("#Otras versiones:");
            for (int i = 1; i < cardinalNumberConverted.Count; i++)
            {
                cardinalTab.Add(cardinalNumberConverted[i].ToString());
            }
        }
        return cardinalTab;
    }

    private ArrayList getFractionaryNumber(string number)
    {
        if (number.Length == 1)
            return justOne(Convert.ToInt32(number));
        String res = "";
        ArrayList aux = new ArrayList();
        aux.Add(res);
        return aux;
        
    }

    private ArrayList getFractionaryNumberTab(string number)
    {
        ArrayList fractionaryNumberConverted = getFractionaryNumber(number);
        ArrayList fractionaryTab = new ArrayList();
        fractionaryTab.Add("#Fraccionario");
        fractionaryTab.Add("Los números fraccionarios expresan división de un todo en partes y designan las fracciones iguales en que se ha dividido la unidad.");
        fractionaryTab.Add("#Número traducido a texto fraccional");
        fractionaryTab.Add(fractionaryNumberConverted[0].ToString());
        if(fractionaryNumberConverted.Count > 1)
        {
            fractionaryTab.Add("#Otras versiones:");
            for (int i = 1; i < fractionaryNumberConverted.Count; i++)
            {
                fractionaryTab.Add(fractionaryNumberConverted[i].ToString());
            }
        }
        return fractionaryTab;

    }
    private ArrayList justOne(int v)
    {
        ArrayList res = new ArrayList();
        switch (v)
        {
            case 1:
                res.Add("une unité");
                res.Add("l'unité");
                break;
            case 2:
                res.Add("un demi");
                break;
            case 3:
                res.Add("un tiers");
                break;
            case 4:
                res.Add("un quart");
                break;
            default:
                //res.Add("un " + getOrdinalNumber(v.ToString())[0].ToString());
                break;
        }
        return res;
    }
    private ArrayList getNegativeNumberTab(String number)
    {
        ArrayList res = new ArrayList();
        res.Add("#Negativo");
        res.Add("Los números negativos expresan pérdidas, deudas, disminuciones, etc. en relaciones con los números naturales.");
        res.Add("#Número traducido a texto cardinal.");
        res.Add("moins " + getCardinalNumber(number.Substring(1))[0].ToString());
        return res;
    }

    public ArrayList getTabs(string number)
    {
        String unformattedNumber = number;
        String minusSign = "";
        String nonDecimal = "";
        String decimalPart = "";
        String divider = "";
        int exit = TratamientoInicial.InitialTratement(ref unformattedNumber, ref minusSign, ref nonDecimal, ref decimalPart, ref divider);

        Fraction fractionNumberTranslation= new Fraction();
        Cardinal cardinalNumberTranslation= new Cardinal();
        Ordinal ordinalNumberTranslation= new Ordinal();

        ArrayList result = new ArrayList();

        //result.Add("Numero: " + unformattedNumber.Replace(" ", ""));
        //result.Add("Menos: " + minusSign);
        //result.Add("Parte entera: " + nonDecimal);
        //result.Add("Parte decimal: " + decimalPart);
        //result.Add("Divisor: " + divider);
        //result.Add(exit);
        //return result;
        if (!number[0].ToString().Equals("-"))
        {
            result.Add(cardinalNumberTranslation.getCardinalTab(nonDecimal));
            result.Add(ordinalNumberTranslation.getOrdinalNumberTab(nonDecimal));
            result.Add(fractionNumberTranslation.getFractionTab(nonDecimal, divider));
        }
        else result.Add(getNegativeNumberTab(number));
        
        return result;
        
    }
}
