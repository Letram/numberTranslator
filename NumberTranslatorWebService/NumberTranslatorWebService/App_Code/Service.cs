using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading;

// NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
public class Service : IService
{

    public string GetData(int value)
	{
		return string.Format("You entered: {0}", value);
	}

	public CompositeType GetDataUsingDataContract(CompositeType composite)
	{
		if (composite == null)
		{
			throw new ArgumentNullException("composite");
		}
		if (composite.BoolValue)
		{
			composite.StringValue += "Suffix";
		}
		return composite;
	}

    public ArrayList getTabs(string number, String language)
    {
        System.Diagnostics.Debug.WriteLine(language);
        if (language.Contains("fr") || language.Contains("es"))
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(language, false);
        }
        else
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en", false);
        }
        return new Servicio().getTabs(number);
    }
}
