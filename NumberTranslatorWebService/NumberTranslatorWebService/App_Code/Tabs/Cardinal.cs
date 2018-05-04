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
        if (number == "") return cardinalNumberArrayList;
        StringBuilder numericValue = new StringBuilder(number);
        string aux = new string('0', Scales.GetMask(numericValue.Length));
        StringBuilder mask = new StringBuilder(aux);
        StringBuilder parsedNumber = mask.Append(numericValue);
        string longScaleNumber = "";
        string shortScaleNumber = "";
        string negative = "";
        if (isNegative) negative = "<b>moins</b> ";
        if(number.Length == 1 && number.Equals("0"))
        {
            cardinalNumberArrayList.Add("zéro");
            return cardinalNumberArrayList;
        }
        //we parse the first group apart so we can add some more control to our translation process

        //tanto en LongScale(default) como ShortScale
        longScaleNumber = new LessThanAThousand(parsedNumber.ToString(parsedNumber.Length - 3, 3)).Translate();
        shortScaleNumber = longScaleNumber;
        Boolean hasShortScale = false;
        int bigNumberIndex = 1;
        for (int i = parsedNumber.Length - 6; i >= 0; i -= 3)
        {
            string translatedGroup = new LessThanAThousand(parsedNumber.ToString(i, 3)).Translate();
            if (!translatedGroup.Equals(""))
            {
                if (bigNumberIndex == 1)
                {
                    if (translatedGroup.Equals("un"))
                    {
                        longScaleNumber = bigNumbersLongScale[bigNumberIndex] + " " + longScaleNumber;
                        if(bigNumberIndex >= 2)
                        {
                            shortScaleNumber = bigNumbersShortScale[bigNumberIndex] + " " + shortScaleNumber;
                            hasShortScale = true;
                        }
                    }
                    else
                    {
                        longScaleNumber = translatedGroup + " " + bigNumbersLongScale[bigNumberIndex] + " " + longScaleNumber;
                        if (bigNumberIndex >= 2)
                        {
                            hasShortScale = true;
                            shortScaleNumber = translatedGroup + " " + bigNumbersShortScale[bigNumberIndex] + " " + shortScaleNumber;
                        }
                    }
                }
                else
                {
                    if (!translatedGroup.Equals("un"))
                    {
                        longScaleNumber = translatedGroup + " " + bigNumbersLongScale[bigNumberIndex] + "s " + longScaleNumber;
                        if (bigNumberIndex >= 2)
                        {
                            hasShortScale = true;
                            shortScaleNumber = translatedGroup + " " + bigNumbersShortScale[bigNumberIndex] + "s " + shortScaleNumber;
                        }
                    }

                    else
                    {
                        longScaleNumber = translatedGroup + " " + bigNumbersLongScale[bigNumberIndex] + " " + longScaleNumber;
                        if (bigNumberIndex >= 2)
                        {
                            hasShortScale = true;
                            shortScaleNumber = translatedGroup + " " + bigNumbersShortScale[bigNumberIndex] + " " + shortScaleNumber;
                        }
                    }
                }
            }
            bigNumberIndex++;
        }
        bigNumberIndex = 0;
        cardinalNumberArrayList.Add(negative + longScaleNumber);
        if(hasShortScale)
            cardinalNumberArrayList.Add(negative + shortScaleNumber);

        return cardinalNumberArrayList;
    }



    public ArrayList getCardinalTab(String number, Boolean isNegative = false)
    {
        ArrayList cardinalNumberConverted = getCardinalNumber(number, isNegative);
        ArrayList cardinalTab = new ArrayList();
        cardinalTab.Add(Resources.Resource.cardinal);
        cardinalTab.Add(Resources.Resource.cardinalDescription);
        cardinalTab.Add(Resources.Resource.numberDescription);
        cardinalTab.Add(cardinalNumberConverted[0].ToString());
        if (cardinalNumberConverted.Count > 1)
        {
            cardinalTab.Add(Resources.Resource.other);
            for (int i = 1; i < cardinalNumberConverted.Count; i++)
            {
                cardinalTab.Add(cardinalNumberConverted[i].ToString());
            }
        }
        return cardinalTab;
    }
}