﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Number
/// </summary>
public class Scales
{
    private static String[] bigNumbersLongScale ={
        "",
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
        "duodécilliard",
        "trédécillon",
        "trédécilliard",
        "quattuordécillion",
        "quattuordécilliard",
        "quindécillion",
        "quindécilliard",
        "sexdécillion",
        "sexdécilliard",
        "septendécillion",
        "septendécilliard",
        "octodécillion",
        "octodécilliard",
        "nonidécillion",
        "nonidécilliard",
        "vigintillon"
    };
    private static String[] bigNumbersShortScale ={
        "",
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
        "tredecillon",
        "quattuordecillion",
        "quindecillion",
        "sexdecillion",
        "septendecillion",
        "octodecillion",
        "novemdecillion",
        "vigintillion",
        "unvigintillion",
        "duovigintillion",
        "trevigintillion",
        "quattuorvigintillion",
        "quinvigintillion",
        "sexvigintillion",
        "septenvigintillion",
        "octovigintillion",
        "novemvigintillion",
        "trigintillion",
        "untrigintillion",
        "duotrigintillion",
        "tretrigintillion",
        "quattuortrigintillion",
        "quintrigintillion",
        "sextrigintillion",
        "septentrigintillion",
        "octotrigintillion",
        "novemtrigintillion"
    };

    private static String[] multiplicativeNumbers =
    {
        "Not available in this form",
        "Single",
        "Double",
        "Triple",
        "Quadruple",
        "Quintuple",
        "Sextuple",
        "Septuple",
        "Octuple",
        "Nonuple",
        "Décuple"
    };

    private static String[] birthNumbers =
{
        "Not available in this form",
        "Singleton",
        "Twin",
        "Triplet",
        "Quadruplet",
        "Quintuplet",
        "Sextuplet",
        "Septuplet",
        "Octuplet",
        "Nonuplet",
        "Decuplet"
    };
    public static String[] getLargeScale()
    {
        return bigNumbersLongScale;
    }

    public static String[] getShortScale()
    {
        return bigNumbersShortScale;
    }

    public static String[] getMultiplicativeNumbers()
    {
        return multiplicativeNumbers;
    }

    public static String[] getBirthNumbers()
    {
        return birthNumbers;
    }
    public static int GetMask(int length)
    {
        int op = 3 - (length % 3);
        switch (op % 3)
        {
            case 1:
                return 1;
            case 2:
                return 2;
            case 3:
                return 0;
        }
        return 0;
    }
}