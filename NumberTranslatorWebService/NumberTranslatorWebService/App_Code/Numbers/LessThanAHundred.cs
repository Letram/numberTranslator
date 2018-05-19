using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class LessThanAHundred
{
    private static String[] dizaines = {
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

    private static String[] unitès = {
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
        int dizaine = number / 10;
        int unitè = number % 10;
        String result = "";

        switch (dizaine)
        {
            case 1:
            case 7:
            case 9:
                unitè = unitè + 10;
                break;
            default:
                break;
        }

        // separadores "-", "et" o "" 
        String separator = "";
        if (dizaine > 1)
        {
            separator = "-";
        }
        // casos particulares
        switch (unitè)
        {
            case 0:
                separator = "";
                break;
            case 1:
                if (dizaine == 8)
                {
                    separator = "-";
                }
                else
                {
                    separator = "-et-";
                }
                break;
            case 11:
                if (dizaine == 7)
                {
                    separator = "-et-";
                }
                break;
            default:
                break;
        }

        // dizaines en lettres
        switch (dizaine)
        {
            case 0:
                result = unitès[unitè];
                break;
            case 8:
                if (unitè == 0)
                {
                    result = dizaines[dizaine];
                }
                else
                {
                    result = dizaines[dizaine]
                                            + separator + unitès[unitè];
                }
                break;
            default:
                result = dizaines[dizaine]
                                        + separator + unitès[unitè];
                break;
        }
        return result;
    }
}