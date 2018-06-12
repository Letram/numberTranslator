using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;

/// <summary>
/// Descripción breve de Class1
/// </summary>
public class Cardinal
{
    String[] bigNumbersLongScale;
    String[] bigNumbersShortScale;
    public Cardinal(){
        bigNumbersLongScale = Scales.getLargeScale();
        bigNumbersShortScale = Scales.getShortScale();
    }

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
        if (isNegative) negative = "moins ";
        if(Regex.Match(number, @"\b(0+)\b").Success)
        {
            cardinalNumberArrayList.Add("zéro");
            return cardinalNumberArrayList;
        }
        //we parse the first group apart so we can add some more control to our translation process

        //tanto en LongScale(default) como ShortScale
        longScaleNumber = new LessThanAThousand(parsedNumber.ToString(parsedNumber.Length - 3, 3)).Translate(true);
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
                        longScaleNumber = bigNumbersLongScale[bigNumberIndex] + "-" + longScaleNumber;
                        shortScaleNumber = bigNumbersShortScale[bigNumberIndex] + "-" + shortScaleNumber;
                        if (bigNumberIndex >= 3)
                        {
                            hasShortScale = true;
                        }
                    }
                    else
                    {
                        longScaleNumber = translatedGroup + "-" + bigNumbersLongScale[bigNumberIndex] + "-" + longScaleNumber;
                        shortScaleNumber = translatedGroup + "-" + bigNumbersShortScale[bigNumberIndex] + "-" + shortScaleNumber;
                        if (bigNumberIndex >= 3)
                        {
                            hasShortScale = true;
                        }
                    }
                }
                else
                {
                    if (!translatedGroup.Equals("un"))
                    {
                        longScaleNumber = translatedGroup + "-" + bigNumbersLongScale[bigNumberIndex] + "s-" + longScaleNumber;
                        shortScaleNumber = translatedGroup + "-" + bigNumbersShortScale[bigNumberIndex] + "s-" + shortScaleNumber;
                        if (bigNumberIndex >= 3)
                        {
                            hasShortScale = true;
                        }
                    }

                    else
                    {
                        longScaleNumber = translatedGroup + "-" + bigNumbersLongScale[bigNumberIndex] + "-" + longScaleNumber;
                        shortScaleNumber = translatedGroup + "-" + bigNumbersShortScale[bigNumberIndex] + "-" + shortScaleNumber;
                        if (bigNumberIndex >= 3)
                        {
                            hasShortScale = true;
                        }
                    }
                }
            }
            bigNumberIndex++;
        }
        bigNumberIndex = 0;
        if (longScaleNumber[longScaleNumber.Length - 1] == '-') longScaleNumber = longScaleNumber.Substring(0, longScaleNumber.Length - 1);
        cardinalNumberArrayList.Add(negative + longScaleNumber);

        if (hasShortScale)
        {
            if (shortScaleNumber[shortScaleNumber.Length - 1] == '-') shortScaleNumber = shortScaleNumber.Substring(0, shortScaleNumber.Length - 1);
            cardinalNumberArrayList.Add(negative + shortScaleNumber);
        }

        return cardinalNumberArrayList;
    }



    public ArrayList getCardinalTab(String number, Boolean isNegative = false)
    {
        ArrayList cardinalNumberConverted = getCardinalNumber(number, isNegative);
        ArrayList cardinalTab = new ArrayList();
        cardinalTab.Add(Resources.Resource.cardinal);
        cardinalTab.Add(Resources.Resource.cardinalDescription);
        cardinalTab.Add(Resources.Resource.numberCardinalDescription);
        cardinalTab.Add(cardinalNumberConverted[0].ToString());
        cardinalTab.Add("@" + cardinalNumberConverted[0].ToString().Replace('-', ' '));
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