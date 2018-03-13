using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Number
/// </summary>
public class Number
{
    private String cardinal = "";
    private String ordinal = "";
    private static Number instance = null;
    public Number(){}
    public static Number getInstance()
    {
        if (instance == null) instance = new Number();
        return instance;
    }

    public void setCardinal(String value)
    {
        this.cardinal = value;
    }
    public void setOrdinal(String value)
    {
        this.ordinal = value;
    }

    public String getCardinal()
    {
        return this.cardinal;
    }
    public String getOrdinal()
    {
        return this.ordinal;
    }
}