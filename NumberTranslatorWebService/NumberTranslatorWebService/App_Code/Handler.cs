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
                errorArray.Add(Resources.Resource.error1);
                break;
            case 2:
                errorArray.Add(Resources.Resource.error2);
                break;
            case 3:
                errorArray.Add(Resources.Resource.error3);
                break;
            case 4:
                errorArray.Add(Resources.Resource.error4);
                break;
        }
        return errorArray;
    }
}