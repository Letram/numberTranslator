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
    private HtmlGenericControl currentContainer;
    private HtmlGenericControl tabs_list;
    private HtmlGenericControl tabsContent;
    protected void Page_Load(object sender, EventArgs e)
    {
        Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("fr-FR");
    }

    protected void convertButton_Click(object sender, EventArgs e)
    {

        //tabs
        tabs_list = new HtmlGenericControl("ul");
        tabs_list.Attributes["class"] = "nav nav-tabs";
        tabs_list.Attributes.Add("role", "tablist");
        tabs_panel.Controls.Add(tabs_list);

        //tabs-content
        tabsContent = new HtmlGenericControl("div");
        tabsContent.Attributes["class"] = "tab-content";
        tabs_panel.Controls.Add(tabsContent);

        ServiceReference1.ServiceClient translator = new ServiceReference1.ServiceClient();

        ArrayList serviceTabs = translator.getTabs(number.Text);

        Boolean firstSet = false;
        for(int i = 0; i < serviceTabs.Count; i++)
        {
            if (serviceTabs[i].GetType() == typeof(ArrayList))
                arrayListTreatment(serviceTabs[i], firstSet);
            if (!firstSet) firstSet = true;

        }

    }

    private void arrayListTreatment(object obj,Boolean firstSet)
    {
        ArrayList tabObject = obj as ArrayList;
        String nameOfTheTab = tabObject[0].ToString().Substring(1);
        HtmlGenericControl tab = new HtmlGenericControl("li");
        tab.Attributes["class"] = "nav-item";
        tabs_list.Controls.Add(tab);

        HtmlGenericControl linkToTabPane = new HtmlGenericControl("a");
        linkToTabPane.Attributes["class"] = "nav-link";
        if (!firstSet)
            linkToTabPane.Attributes["class"] += " active";
        linkToTabPane.Attributes.Add("data-toggle", "tab");
        linkToTabPane.Attributes.Add("href", "#" + nameOfTheTab);
        linkToTabPane.Attributes.Add("role", "tab");
        linkToTabPane.InnerText = nameOfTheTab;
        tab.Controls.Add(linkToTabPane);

        HtmlGenericControl tabPane = new HtmlGenericControl("div");
        tabPane.Attributes["class"] = "tab-pane";
        if (!firstSet)
        {
            tabPane.Attributes["class"] += " active";
        }
        tabPane.Attributes.Add("role", "tabpanel");
        tabPane.Attributes["id"] = nameOfTheTab;
        tabsContent.Controls.Add(tabPane);

        currentContainer = tabPane;

        for (int i = 1; i < tabObject.Count; i++)
        {
            String text = tabObject[i].ToString();
            if (!text.Equals(""))
            {
                switch (text[0])
                {
                    case '#':
                        HtmlGenericControl greyContainer = new HtmlGenericControl("div");
                        currentContainer.Controls.Add(greyContainer);

                        HtmlGenericControl greyContainerTitle = new HtmlGenericControl("div");
                        greyContainerTitle.Attributes["style"] = "background-color:#d6d6d6;";
                        greyContainer.Controls.Add(greyContainerTitle);

                        HtmlGenericControl greyContainerTitleIcon = new HtmlGenericControl("span");
                        greyContainerTitleIcon.InnerText = "+";
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
}