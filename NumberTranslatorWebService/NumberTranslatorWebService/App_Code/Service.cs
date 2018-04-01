using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

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
        Number resultNumber = Number.getInstance();

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
        if (longScaleNumber.Equals(" "))
        {
            resultNumber.setCardinalLong("zèro");
            resultNumber.setCardinalShort("zèro");
        }
        else
        {
            resultNumber.setCardinalLong(longScaleNumber.Trim());
            resultNumber.setCardinalShort(shortScaleNumber.Trim());
        }
        cardinalNumberArrayList.Add(longScaleNumber);
        cardinalNumberArrayList.Add(shortScaleNumber);
        
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

    private ArrayList getOrdinalNumber(string number)
    {
        String ordinalNumber = getCardinalNumber(number)[0].ToString();
        ArrayList ordinalNumberArrayList = new ArrayList();
        //special cases such as first, zero and those numbers ending with 5 (cinq -> cinqu + ième) or 9 (neuf -> neuv + ième) or when the numbers ends with -s

        //one
        if (ordinalNumber.Equals("un")){
            ordinalNumberArrayList.Add("prèmier");
            ordinalNumberArrayList.Add("prèmiere");
            return ordinalNumberArrayList;
        }

        //zero
        if (ordinalNumber.Equals("zèro")){
            return ordinalNumberArrayList;
        }

        if (ordinalNumber.Equals("deux"))
        {
            ordinalNumberArrayList.Add("deuxième");
            ordinalNumberArrayList.Add("second");
            return ordinalNumberArrayList;
        }
        //five and nine
        if(ordinalNumber[ordinalNumber.Length - 1].Equals('q'))
        {
            ordinalNumber = ordinalNumber + "u";
        }
        else if(ordinalNumber[ordinalNumber.Length - 1].Equals('f'))
        {
            ordinalNumber = ordinalNumber.Substring(0, ordinalNumber.Length - 1);
            ordinalNumber = ordinalNumber + "v";
        }
        String ending = "ième";

        //regular cases
        if (ordinalNumber[ordinalNumber.Length - 1].Equals('e') || ordinalNumber[ordinalNumber.Length - 1].Equals('s'))
            ordinalNumber = ordinalNumber.Substring(0, ordinalNumber.Length - 1);
        else if (ordinalNumber.Substring(ordinalNumber.Length - 2).Equals("s "))
            ordinalNumber = ordinalNumber.Substring(0, ordinalNumber.Length - 2);
        ordinalNumber = ordinalNumber + ending;
        ordinalNumberArrayList.Add(ordinalNumber);
        return ordinalNumberArrayList;
    }

    public ArrayList getOrdinalNumberTab(string number)
    {
        ArrayList ordinalNumberConverted = getOrdinalNumber(number);
        ArrayList ordinalTab = new ArrayList();
        ordinalTab.Add("#Ordinal");
        ordinalTab.Add("Los números ordinales expresan orden o sucesión e indican el lugar que ocupa el elemento en una serie ordenada.");
        ordinalTab.Add("#Número traducido a texto ordinal");
        ordinalTab.Add(ordinalNumberConverted[0].ToString());
        if(ordinalNumberConverted.Count > 1)
        {
            ordinalTab.Add("#Otras versiones:");
            for (int i = 1; i < ordinalNumberConverted.Count; i++)
            {
                ordinalTab.Add(ordinalNumberConverted[i].ToString());
            }
        }
        return ordinalTab;
    }

    private ArrayList getFractionaryNumber(string number)
    {
        if (number.Length == 1)
            return justOne(Convert.ToInt32(number));
        String res = getOrdinalNumber(number)[0].ToString();
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
                res.Add("un " + getOrdinalNumber(v.ToString())[0].ToString());
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
        ArrayList result = new ArrayList();
        if (!number[0].ToString().Equals("-"))
        {
            result.Add(getCardinalNumberTab(number));
            result.Add(getOrdinalNumberTab(number));
            result.Add(getFractionaryNumberTab(number));
        }
        else result.Add(getNegativeNumberTab(number));
        return result;
    }
}
