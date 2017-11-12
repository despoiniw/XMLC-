using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            DataSet1.CategoriesDataTable dtc = new DataSet1.CategoriesDataTable();
            DataSet1.CategoriesRow dr = dtc.NewCategoriesRow();
            dr["Name"] = "Shampoo";
            dtc.Rows.Add(dr);

            Console.WriteLine(dtc.Rows[0]["Id"] + " " + dtc.Rows[0]["Name"]);

            Console.ReadKey();


        }
    }
}
