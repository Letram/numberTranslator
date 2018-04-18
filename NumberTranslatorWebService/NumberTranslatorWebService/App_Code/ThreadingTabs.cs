using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class ThreadingTabs
{
    private static ArrayList tabs;
    public ThreadingTabs()
    {
        tabs = new ArrayList();
    }

    public static void addTab(ArrayList tab)
    {
        tabs.Add(tab);
    }

    public ArrayList getTabs()
    {
        return tabs;
    }
}