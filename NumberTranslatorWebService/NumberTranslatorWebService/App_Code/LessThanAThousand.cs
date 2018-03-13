﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Descripción breve de LessThanAThousand
/// </summary>
public class LessThanAThousand
{
    int number;
    int cent;
    int reposer;

    public LessThanAThousand()
    {
    }

    public LessThanAThousand(String numberString)
    {
        this.number = Convert.ToInt32(numberString);
        this.cent = number / 100;
        this.reposer = number % 100;
    }

    public String Translate()
    {
        switch (cent)
        {
            case 0:
                return new LessThanAHundred(reposer).Translate();
            case 1:
                return "cent " + new LessThanAHundred(reposer).Translate();
            default:
                if (reposer > 0)
                    return new LessThanAHundred(cent).Translate() + " cent " + new LessThanAHundred(reposer).Translate();
                else
                    return new LessThanAHundred(cent).Translate() + " cents ";
        }
    }
}