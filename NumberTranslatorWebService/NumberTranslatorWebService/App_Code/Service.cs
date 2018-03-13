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

    private String[] bigNumbers =
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

    public string getCardinalNumber(string number)
    {
        StringBuilder numericValue = new StringBuilder(number);
        string aux = new string('0', GetMask(numericValue.Length));
        StringBuilder mask = new StringBuilder(aux);
        StringBuilder parsedNumber = mask.Append(numericValue);
        string result = "";
        Number resultNumber = Number.getInstance();

        //we parse the first group apart so we can add some more control to our translation process
        result = new LessThanAThousand(parsedNumber.ToString(parsedNumber.Length - 3, 3)).Translate();
        int bigNumberIndex = 0;
        for (int i = parsedNumber.Length - 6; i >= 0; i -= 3)
        {
            string translatedGroup = new LessThanAThousand(parsedNumber.ToString(i, 3)).Translate();
            if (!translatedGroup.Equals(""))
            {
                if (bigNumberIndex == 0)
                {
                    if (translatedGroup.Equals("un")) result = bigNumbers[bigNumberIndex] + " " + result;
                    else result = translatedGroup + " " + bigNumbers[bigNumberIndex] + " " + result;
                }
                else
                {
                    if (!translatedGroup.Equals("un")) result = translatedGroup + " " + bigNumbers[bigNumberIndex] + "s " + result;
                    else result = translatedGroup + " " + bigNumbers[bigNumberIndex] + " " + result;
                }
            }
            bigNumberIndex++;
        }
        bigNumberIndex = 0;
        if (result.Equals(" ")) resultNumber.setCardinal("zèro");
        else resultNumber.setCardinal(result.Trim());
        return resultNumber.getCardinal();
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
        ArrayList cardinalTab = new ArrayList();
        cardinalTab.Add("#Cardinal");
        cardinalTab.Add("Los números cardinales expresan cantidad en relación con los números naturales.");
        cardinalTab.Add("#Número traducido a texto cardinal");
        cardinalTab.Add(getCardinalNumber(number));
        return cardinalTab;
    }

    public string getOrdinalNumber(string number)
    {
        String ordinalNumber = getCardinalNumber(number);
        //special cases such as first, zero and those numbers ending with 5 (cinq -> cinqu + ième) or 9 (neuf -> neuv + ième)

        //one
        if (ordinalNumber.Equals("un")){
            return "première";
        }

        //zero
        if (ordinalNumber.Equals("zèro")){
            return "Este número no existe en esta forma";
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
        ordinalNumber = ordinalNumber + ending;
        return ordinalNumber;
    }

    public ArrayList getOrdinalNumberTab(string number)
    {
        ArrayList ordinalTab = new ArrayList();
        ordinalTab.Add("#Ordinal");
        ordinalTab.Add("Los números ordinales expresan orden o sucesión e indican el lugar que ocupa el elemento en una serie ordenada.");
        ordinalTab.Add("#Número traducido a texto ordinal");
        ordinalTab.Add(getOrdinalNumber(number));
        return ordinalTab;
    }
}
