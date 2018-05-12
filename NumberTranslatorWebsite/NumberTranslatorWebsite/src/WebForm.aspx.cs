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
        //Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("fr");
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
        String language = Thread.CurrentThread.CurrentUICulture.Name;
        ArrayList serviceTabs = translator.getTabs(number.Text, language);
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
        if (tabObject.Count < 1) return;
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
                        currentContainer = tabPane;
                        HtmlGenericControl greyContainer = new HtmlGenericControl("div");
                        currentContainer.Controls.Add(greyContainer);

                        HtmlGenericControl greyContainerTitle = new HtmlGenericControl("div");
                        greyContainerTitle.Attributes["class"] += " greyContainerTitle";
                        greyContainer.Controls.Add(greyContainerTitle);

                        HtmlGenericControl greyTitleSpan = new HtmlGenericControl("span");
                        greyTitleSpan.InnerHtml = text.Substring(1);
                        greyContainerTitle.Controls.Add(greyTitleSpan);

                        HtmlGenericControl greyContainerBody = new HtmlGenericControl("div");
                        greyContainerBody.Attributes["style"] = "padding:10px;";
                        greyContainer.Controls.Add(greyContainerBody);
                        currentContainer = greyContainerBody;
                        break;
                    case '&':
                        if (text[1].Equals('&'))
                        {
                            HtmlGenericControl blueContainer = new HtmlGenericControl("div");
                            blueContainer.Attributes["class"] += "blueContainer";
                            currentContainer.Controls.Add(blueContainer);

                            HtmlGenericControl blueContainerTitle = new HtmlGenericControl("div");
                            blueContainerTitle.Attributes["class"] += "blueContainerTitle";
                            blueContainer.Controls.Add(blueContainerTitle);

                            HtmlGenericControl blueContainerTitleIcon = new HtmlGenericControl("span");
                            blueContainerTitleIcon.Attributes["id"] = "blueContainerIcon";
                            blueContainerTitleIcon.InnerText = "+";
                            blueContainerTitle.Controls.Add(blueContainerTitleIcon);

                            HtmlGenericControl blueTitleSpan = new HtmlGenericControl("span");
                            blueTitleSpan.InnerHtml = text.Substring(2);
                            blueContainerTitle.Controls.Add(blueTitleSpan);

                            HtmlGenericControl blueContainerBody = new HtmlGenericControl("div");
                            blueContainerBody.Attributes["id"] = "blueContainer";
                            blueContainerBody.Attributes["class"] += "hideOnStart";
                            blueContainer.Controls.Add(blueContainerBody);
                            currentContainer = blueContainerBody;
                        }
                        else
                        {
                            HtmlGenericControl purpleContainer = new HtmlGenericControl("div");
                            purpleContainer.Attributes["class"] += "purpleContainer";
                            currentContainer.Controls.Add(purpleContainer);

                            HtmlGenericControl purpleContainerTitle = new HtmlGenericControl("div");
                            purpleContainerTitle.Attributes["class"] += "purpleContainerTitle";
                            purpleContainer.Controls.Add(purpleContainerTitle);

                            HtmlGenericControl purpleContainerTitleIcon = new HtmlGenericControl("span");
                            purpleContainerTitleIcon.Attributes["id"] = "purpleContainerIcon";
                            purpleContainerTitleIcon.InnerText = "+";
                            purpleContainerTitle.Controls.Add(purpleContainerTitleIcon);

                            HtmlGenericControl purpleTitleSpan = new HtmlGenericControl("span");
                            purpleTitleSpan.InnerHtml = text.Substring(1);
                            purpleContainerTitle.Controls.Add(purpleTitleSpan);

                            HtmlGenericControl purpleContainerBody = new HtmlGenericControl("div");
                            purpleContainerBody.Attributes["id"] = "purpleContainer";
                            purpleContainerBody.Attributes["class"] += "hideOnStart";
                            purpleContainer.Controls.Add(purpleContainerBody);
                            currentContainer = purpleContainerBody;
                        }
                        break;
                    case '@':
                        HtmlGenericControl button = new HtmlGenericControl("button");
                        button.Attributes["class"] += "btn btn-primary btn-lg btn-block my-2";
                        button.Attributes["onclick"] = "responsiveVoice.speak(\'" + text.Substring(1) + "\', \'French Female\');";
                        button.InnerText = "Click on me to hear how it sounds!";
                        currentContainer.Controls.Add(button);
                        break;

                    default:
                        HtmlGenericControl p = new HtmlGenericControl("p");
                        p.Attributes["class"] += "text-justify";
                        p.InnerHtml = text;
                        currentContainer.Controls.Add(p);
                        break;
                }
            }
        }
    }
}