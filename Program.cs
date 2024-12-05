using System;
using System.Windows.Forms;

namespace GraphPlotter
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new GraphForm());
        }
    }
}
