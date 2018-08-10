using System;
using System.Windows.Forms;
using System.Resources;
using System.Reflection;

static class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new Form1());
    }
}

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        
    }
}