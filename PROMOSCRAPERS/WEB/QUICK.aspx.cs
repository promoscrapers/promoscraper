using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class QUICK : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
         
    }

    protected void btnbb_Click(object sender, EventArgs e)
    {
        string txt = txtbb.Text;
        string[] lst = txt.Split(new Char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        string[] lst2;

        tbloutput.Rows.Clear();

        TableRow tr = new TableRow();
        TableCell td = new TableCell();

        for (int i = 0; i < lst.Length; i++)
        {
            lst2 = lst[i].Split(new Char[] { '$' }, StringSplitOptions.RemoveEmptyEntries);

            td.Text = lst2[0].Trim();

            tr.Cells.Add(td);
            td = new TableCell();

            td.Text = "$"+ (decimal.Parse(lst2[1].Trim()) + 4).ToString();

            tr.Cells.Add(td);
            td = new TableCell();

            td.Text = "$" + (decimal.Parse(lst2[2].Trim()) + 4).ToString();

            tr.Cells.Add(td);
            td = new TableCell();


            td.Text = "$" + (decimal.Parse(lst2[3].Trim()) + 4).ToString();

            tr.Cells.Add(td);
            td = new TableCell();


            td.Text = "$" + (decimal.Parse(lst2[4].Trim()) + 4).ToString();

            tr.Cells.Add(td);
            td = new TableCell();


            td.Text = "$" + (decimal.Parse(lst2[5].Trim()) + 4).ToString();

            tr.Cells.Add(td);
            td = new TableCell();
            tbloutput.Rows.Add(tr);
            tr = new TableRow();

        }
    }
}