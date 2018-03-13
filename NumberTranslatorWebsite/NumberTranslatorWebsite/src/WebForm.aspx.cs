using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Threading;
using System.Globalization;
using System.Collections;

public partial class src_WebForm : System.Web.UI.Page
{
    HtmlGenericControl currentContainer;
    protected void Page_Load(object sender, EventArgs e)
    {
        Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("fr-FR");
    }

    protected void convertButton_Click(object sender, EventArgs e)
    {
        ServiceReference1.ServiceClient translator = new ServiceReference1.ServiceClient();

        String cardinal = translator.getCardinalNumber(number.Text);
        String ordinal = translator.getOrdinalNumber(number.Text);

        ArrayList cardinalTab = translator.getCardinalNumberTab(number.Text);
        ArrayList ordinalTab = translator.getOrdinalNumberTab(number.Text);

        //tabs
        HtmlGenericControl tabs_list = new HtmlGenericControl("ul");
        tabs_list.Attributes["class"] = "nav nav-tabs";
        tabs_list.Attributes.Add("role", "tablist");
        tabs_panel.Controls.Add(tabs_list);

        //tabs-content
        HtmlGenericControl tabsContent = new HtmlGenericControl("div");
        tabsContent.Attributes["class"] = "tab-content";
        tabs_panel.Controls.Add(tabsContent);


        /*
         * El primer '#' genera el nombre del tab y crea el contenedor del tab correspondiente. => el primer elemento del arraylist.
         * Del segundo '#' en adelante generamos un nuevo contenedor dentro del contenedor actual (gris)
         * Cada '&&' genera un contenedor dentro del contenedor root (tabPane) (azul)
         * Toda la información que no sea así, simplemente se añade al contenedor actual como un <p>
         * 
         * */

        //con el primer elemento creamos el tab correspondiente y el contenedor de ese tab
        String nameOfTheTab = cardinalTab[0].ToString().Substring(1);
        HtmlGenericControl tab = new HtmlGenericControl("li");
        tab.Attributes["class"] = "nav-item";
        tabs_list.Controls.Add(tab);

        HtmlGenericControl linkToTabPane = new HtmlGenericControl("a");
        linkToTabPane.Attributes["class"] = "nav-link active";
        linkToTabPane.Attributes.Add("data-toggle", "tab");
        linkToTabPane.Attributes.Add("href", "#" + nameOfTheTab);
        linkToTabPane.Attributes.Add("role", "tab");
        linkToTabPane.InnerText = nameOfTheTab;
        tab.Controls.Add(linkToTabPane);

        HtmlGenericControl tabPane = new HtmlGenericControl("div");
        tabPane.Attributes["class"] = "tab-pane active";
        tabPane.Attributes.Add("role", "tabpanel");
        tabPane.Attributes["id"] = nameOfTheTab;
        tabsContent.Controls.Add(tabPane);

        currentContainer = tabPane;

        for(int i = 1; i < cardinalTab.Count; i++)
        {
            String text = cardinalTab[i].ToString();

            switch (text[0])
            {
                case '#':
                    HtmlGenericControl greyContainer = new HtmlGenericControl("div");
                    currentContainer.Controls.Add(greyContainer);

                    HtmlGenericControl greyContainerTitle = new HtmlGenericControl("div");
                    greyContainerTitle.Attributes["style"] = "background-color:#d6d6d6;";
                    greyContainer.Controls.Add(greyContainerTitle);

                    HtmlGenericControl greyContainerTitleIcon = new HtmlGenericControl("i");
                    greyContainerTitleIcon.Attributes["class"] = "glyphicon glyphicon-plus";
                    greyContainerTitleIcon.Attributes["style"] = "font-size:32px;padding:10px;";
                    greyContainerTitle.Controls.Add(greyContainerTitleIcon);

                    HtmlGenericControl greyTitleSpan = new HtmlGenericControl("span");
                    greyTitleSpan.Attributes["style"] = "font-size:32px;padding:0px;";
                    greyTitleSpan.InnerHtml = text.Substring(1);
                    greyContainerTitle.Controls.Add(greyTitleSpan);

                    HtmlGenericControl greyContainerBody = new HtmlGenericControl("div");
                    greyContainerBody.Attributes["style"] = "padding:10px;";
                    greyContainer.Controls.Add(greyContainerBody);
                    currentContainer = greyContainerBody;
                    break;
                default:
                    HtmlGenericControl p = new HtmlGenericControl("p");
                    p.InnerText = text;
                    currentContainer.Controls.Add(p);
                    break;

            }
        }

        nameOfTheTab = ordinalTab[0].ToString().Substring(1);
        tab = new HtmlGenericControl("li");
        tab.Attributes["class"] = "nav-item";
        tabs_list.Controls.Add(tab);

        linkToTabPane = new HtmlGenericControl("a");
        linkToTabPane.Attributes["class"] = "nav-link";
        linkToTabPane.Attributes.Add("data-toggle", "tab");
        linkToTabPane.Attributes.Add("href", "#" + nameOfTheTab);
        linkToTabPane.Attributes.Add("role", "tab");
        linkToTabPane.InnerText = nameOfTheTab;
        tab.Controls.Add(linkToTabPane);

        tabPane = new HtmlGenericControl("div");
        tabPane.Attributes["class"] = "tab-pane";
        tabPane.Attributes.Add("role", "tabpanel");
        tabPane.Attributes["id"] = nameOfTheTab;
        tabsContent.Controls.Add(tabPane);

        currentContainer = tabPane;

        for (int i = 1; i < ordinalTab.Count; i++)
        {
            String text = ordinalTab[i].ToString();

            switch (text[0])
            {
                case '#':
                    HtmlGenericControl greyContainer = new HtmlGenericControl("div");
                    currentContainer.Controls.Add(greyContainer);

                    HtmlGenericControl greyContainerTitle = new HtmlGenericControl("div");
                    greyContainerTitle.Attributes["style"] = "background-color:#d6d6d6;";
                    greyContainer.Controls.Add(greyContainerTitle);

                    HtmlGenericControl greyContainerTitleIcon = new HtmlGenericControl("i");
                    greyContainerTitleIcon.Attributes["class"] = "glyphicon glyphicon-plus";
                    greyContainerTitleIcon.Attributes["style"] = "font-size:32px;padding:10px;";
                    greyContainerTitle.Controls.Add(greyContainerTitleIcon);

                    HtmlGenericControl greyTitleSpan = new HtmlGenericControl("span");
                    greyTitleSpan.Attributes["style"] = "font-size:32px;padding:0px;";
                    greyTitleSpan.InnerHtml = text.Substring(1);
                    greyContainerTitle.Controls.Add(greyTitleSpan);

                    HtmlGenericControl greyContainerBody = new HtmlGenericControl("div");
                    greyContainerBody.Attributes["style"] = "padding:10px;";
                    greyContainer.Controls.Add(greyContainerBody);
                    currentContainer = greyContainerBody;
                    break;
                default:
                    HtmlGenericControl p = new HtmlGenericControl("p");
                    p.InnerText = text;
                    currentContainer.Controls.Add(p);
                    break;

            }
        }
        }
}