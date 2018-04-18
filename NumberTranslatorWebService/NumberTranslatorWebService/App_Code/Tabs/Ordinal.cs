using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Descripción breve de Class1
/// </summary>
public class Ordinal
{
    String[] bigNumbersLongScale = Scales.getLargeScale();
    String[] bigNumbersShortScale = Scales.getShortScale();
    Boolean treated = false;
    public Ordinal() { }

    public ArrayList getOrdinalNumber(string number){
        ArrayList ordinalNumberArrayList = new ArrayList();
        StringBuilder numericValue = new StringBuilder(number);
        string aux = new string('0', Scales.GetMask(numericValue.Length));
        StringBuilder mask = new StringBuilder(aux);
        StringBuilder parsedNumber = mask.Append(numericValue);
        String ordinalNumberTranslated = "";
        //traducimos los primeros tres números

        ordinalNumberTranslated = new LessThanAThousand(parsedNumber.ToString(parsedNumber.Length - 3, 3)).Translate();

        if (number.Length == 1){
            if (ordinalNumberTranslated.Equals("un"))
            {
                ordinalNumberArrayList.Add("prèmier");
                ordinalNumberArrayList.Add("prèmiere");
                return ordinalNumberArrayList;
            }


            if (ordinalNumberTranslated.Equals("deux"))
            {
                ordinalNumberArrayList.Add("deuxième");
                ordinalNumberArrayList.Add("second");
                return ordinalNumberArrayList;
            }
        }
        
        ordinalNumberTranslated = ordinalTreatment(ordinalNumberTranslated.Trim(), parsedNumber.ToString(parsedNumber.Length - 3, 3));
        //traducimos el resto
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
                        ordinalNumberTranslated = bigNumbersLongScale[bigNumberIndex] + " " + ordinalNumberTranslated;
                    }
                    else
                    {
                        ordinalNumberTranslated = translatedGroup + " " + bigNumbersLongScale[bigNumberIndex] + " " + ordinalNumberTranslated;
                    }
                }
                else
                {
                    if (!translatedGroup.Equals("un"))
                    {
                        ordinalNumberTranslated = translatedGroup + " " + bigNumbersLongScale[bigNumberIndex] + "s " + ordinalNumberTranslated;
                    }

                    else
                    {
                        ordinalNumberTranslated = translatedGroup + " " + bigNumbersLongScale[bigNumberIndex] + " " + ordinalNumberTranslated;
                    }
                }
            }
            bigNumberIndex++;
        }
        bigNumberIndex = 0;
        ordinalNumberTranslated = ordinalNumberTranslated.Trim();
        if (!treated) ordinalNumberTranslated = treatRegularCases(ordinalNumberTranslated, parsedNumber.ToString(parsedNumber.Length - 3, 3));
        ordinalNumberArrayList.Add(ordinalNumberTranslated);
        return ordinalNumberArrayList;
    }

    private string ordinalTreatment(string ordinalNumber, string number)
    {
        if (number.Equals("000")) return "";
        treated = true;
        if (ordinalNumber[ordinalNumber.Length - 1].Equals('q'))
        {
            ordinalNumber = ordinalNumber + "u";
        }
        else if (ordinalNumber[ordinalNumber.Length - 1].Equals('f'))
        {
            ordinalNumber = ordinalNumber.Substring(0, ordinalNumber.Length - 1);
            ordinalNumber = ordinalNumber + "v";
        }
        String ending = "ième";

        //regular cases
        return treatRegularCases(ordinalNumber, number);
    }

    private string treatRegularCases(string ordinalNumber, string number)
    {
        String ending = "ième";
        if (ordinalNumber[ordinalNumber.Length - 1].Equals('e') ||
           (ordinalNumber[ordinalNumber.Length - 1].Equals('s') && !number[number.Length - 1].Equals('3')))
        {
            ordinalNumber = ordinalNumber.Substring(0, ordinalNumber.Length - 1);
        }
        return ordinalNumber + ending;
    }

    public ArrayList getOrdinalNumberTab(string number)
    {
        ArrayList ordinalNumberConverted = getOrdinalNumber(number);
        ArrayList ordinalTab = new ArrayList();
        ordinalTab.Add("#Ordinal");
        ordinalTab.Add("Los números ordinales expresan orden o sucesión e indican el lugar que ocupa el elemento en una serie ordenada.");
        ordinalTab.Add("#Número traducido a texto ordinal");
        ordinalTab.Add(ordinalNumberConverted[0].ToString());
        if (ordinalNumberConverted.Count > 1)
        {
            ordinalTab.Add("#Otras versiones:");
            for (int i = 1; i < ordinalNumberConverted.Count; i++)
            {
                ordinalTab.Add(ordinalNumberConverted[i].ToString());
            }
        }
        return ordinalTab;
    }
}