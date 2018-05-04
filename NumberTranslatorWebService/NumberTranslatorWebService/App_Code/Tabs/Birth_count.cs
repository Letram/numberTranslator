using System;
using System.Collections;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Class1
/// </summary>
public class Birth_Count
{    public Birth_Count()
    { }

    private ArrayList getBirthCountNumber(String number)
    {
        double aux = double.Parse(number);
        ArrayList res = new ArrayList();
        if (aux < 10)
        {
            res.Add(Scales.getMultiplicativeNumbers()[int.Parse(aux.ToString())]);
        }
        return res;
    }

    public ArrayList getBirthCountTab(String number)
    {
        ArrayList birthCountNumber = getBirthCountNumber(number);
        ArrayList birthCountNumberTab = new ArrayList();
        if (birthCountNumber.Count < 1)
            return birthCountNumberTab;
        birthCountNumberTab.Add(Resources.Resource.birth);
        birthCountNumberTab.Add(Resources.Resource.birthDescripion);
        birthCountNumberTab.Add(Resources.Resource.birthNumberDescription);
        birthCountNumberTab.Add(birthCountNumber[0].ToString());
        if (birthCountNumber.Count > 1)
        {
            birthCountNumberTab.Add(Resources.Resource.other);
            for (int i = 1; i < birthCountNumber.Count; i++)
            {
                birthCountNumberTab.Add(birthCountNumber[i].ToString());
            }
        }
        return birthCountNumberTab;
    }
}