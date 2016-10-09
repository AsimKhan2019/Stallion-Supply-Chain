using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using StallionSuppyChain.Admin;

namespace StallionSuppyChain
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new StallionSuppyChain.Products.ProductPlannerMain());
            //Application.Run(new StallionSuppyChain.Products.ProductMaster());
            //Application.Run(new StallionSuppyChain.SCMLogIN());
            //Application.Run(new StallionSuppyChain.Reports.PrintBarcode());
        }
    }
}
