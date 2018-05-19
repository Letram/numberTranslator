using System;
using System.Collections;
using System.Linq;
using System.Web;

public class Handler
{
    private static Handler instance;
    public Handler(){}
    public static Handler getInstance()
    {
        if (instance == null) instance = new Handler();
        return instance;
    }
    public ArrayList errorHandler(int err)
    {
        ArrayList errorArray = new ArrayList();
        errorArray.Add("#Error");
        switch (err)
        {
            case 1:
                errorArray.Add("Exponent is too high. Limit is ~E120");
                break;
            case 2:
                errorArray.Add("Number not supported");
                break;
            case 3:
                errorArray.Add("Number is not written properly.");
                break;
        }
        return errorArray;
    }
}