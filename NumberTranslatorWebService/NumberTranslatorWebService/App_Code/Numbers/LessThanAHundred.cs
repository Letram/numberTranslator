using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class LessThanAHundred
{
    private static String[] tensArr = {
    "",
    "",
    "vingt",
    "trente",
    "quarante",
    "cinquante",
    "soixante",
    "soixante",
    "quatre-vingt",
    "quatre-vingt"
  };

    private static String[] unitsArr = {
    "",
    "un",
    "deux",
    "trois",
    "quatre",
    "cinq",
    "six",
    "sept",
    "huit",
    "neuf",
    "dix",
    "onze",
    "douze",
    "treize",
    "quatorze",
    "quinze",
    "seize",
    "dix-sept",
    "dix-huit",
    "dix-neuf"
  };

    int number;
    public LessThanAHundred(int numberDesired)
    {
        this.number = numberDesired;
    }

    public String Translate()
    {
        int tens = number / 10;
        int units = number % 10;
        String result = "";

        switch (tens)
        {
            case 1:
            case 7:
            case 9:
                units = units + 10;
                break;
            default:
                break;
        }

        // separadores "-", "et" o "" 
        String separator = "";
        if (tens > 1)
        {
            separator = "-";
        }
        // casos particulares
        switch (units)
        {
            case 0:
                separator = "";
                break;
            case 1:
                if (tens == 8)
                {
                    separator = "-";
                }
                else
                {
                    separator = "-et-";
                }
                break;
            case 11:
                if (tens == 7)
                {
                    separator = "-et-";
                }
                break;
            default:
                break;
        }

        // dizaines en lettres
        switch (tens)
        {
            case 0:
                result = unitsArr[units];
                break;
            case 8:
                if (units == 0)
                {
                    result = tensArr[tens];
                }
                else
                {
                    result = tensArr[tens]
                                            + separator + unitsArr[units];
                }
                break;
            default:
                result = tensArr[tens]
                                        + separator + unitsArr[units];
                break;
        }
        return result;
    }
}