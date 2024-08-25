using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;
using System.Data;
using TreeUtility;
/// <summary>
/// Summary description for Category
/// </summary>
public class Category 
{
	

    private int _id;
    private string _name;
    private Category _parent;
    private List<Category> _children;

    public int Id
    {
        get { return _id; }
        set { _id = value; }
    }

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public Category Parent
    {
        get { return _parent; }
        set { _parent = value; }
    }

    public List<Category> Children
    {
        get { return _children; }
        set { _children = value; }
    }

   

}