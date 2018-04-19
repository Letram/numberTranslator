using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Descripción breve de Class1
/// </summary>
public class Cardinal
{
    String[] bigNumbersLongScale = Scales.getLargeScale();
    String[] bigNumbersShortScale = Scales.getShortScale();
    public Cardinal(){}

    public ArrayList getCardinalNumber(string number, Boolean isNegative = false){
        ArrayList cardinalNumberArrayList = new ArrayList();
        StringBuilder numericValue = new StringBuilder(number);
        string aux = new string('0', Scales.GetMask(numericValue.Length));
        StringBuilder mask = new StringBuilder(aux);
        StringBuilder parsedNumber = mask.Append(numericValue);
        string longScaleNumber = "";
        string shortScaleNumber = "";
        string negative = "";
        if (isNegative) negative = "moins ";
        if(number.Length == 1 && number[0].Equals("0"))
        {
            cardinalNumberArrayList.Add("zéro");
            return cardinalNumberArrayList;
        }
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
        cardinalNumberArrayList.Add(negative + longScaleNumber);
        cardinalNumberArrayList.Add(negative + shortScaleNumber);

        return cardinalNumberArrayList;
    }



    public ArrayList getCardinalTab(String number, Boolean isNegative = false)
    {
        ArrayList cardinalNumberConverted = getCardinalNumber(number, isNegative);
        ArrayList cardinalTab = new ArrayList();
        cardinalTab.Add("#Cardinal");
        cardinalTab.Add("Los números cardinales expresan cantidad en relación con los números naturales.");
        cardinalTab.Add("#Número traducido a texto cardinal");
        cardinalTab.Add(cardinalNumberConverted[0].ToString());
        if (cardinalNumberConverted.Count > 1)
        {
            cardinalTab.Add("#Otras versiones:");
            for (int i = 1; i < cardinalNumberConverted.Count; i++)
            {
                cardinalTab.Add(cardinalNumberConverted[i].ToString());
            }
        }
        return cardinalTab;
    }
}