using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Number
/// </summary>
public class Number
{
    private String cardinalLongScale = "";
    private String cardinalShortScale = "";
    private String ordinal = "";
    private static Number instance = null;
    public Number(){}
    public static Number getInstance()
    {
        if (instance == null) instance = new Number();
        return instance;
    }

    public void setCardinalLong(String value)
    {
        this.cardinalLongScale = value;
    }
    public void setCardinalShort(String value)
    {
        this.cardinalShortScale = value;
    }
    public void setOrdinal(String value)
    {
        this.ordinal = value;
    }

    public String getCardinalLong()
    {
        return this.cardinalLongScale;
    }
    public String getCardinalShort()
    {
        return this.cardinalShortScale;
    }
    public String getOrdinal()
    {
        return this.ordinal;
    }
}