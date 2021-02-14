using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private object l = new object();
        List<Bitmap> fotos = new List<Bitmap>();
        public Form1()
        {
            InitializeComponent();

        }

        private bool rellenaFotos()
        {
            DirectoryInfo di;

            di = new DirectoryInfo(textBox1.Text);

            if (di.Exists)
            {
                foreach(FileInfo f in di.EnumerateFiles())
                {
                    if(f.Extension == ".jpg" || f.Extension == ".png" || f.Extension == ".jpeg")
                    {
                        fotos.Add(new Bitmap(f.FullName, true));
                    }
                }
            }
            else
            {
                label2.Text = "No se encontro el directorio";
                return false;
            }
            if (fotos.Count == 0)
            {
                label2.Text = "No se encontro imagen alguna";
                return false;
            }
            return true;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (rellenaFotos())
            {
                Thread muestra = new Thread(muestraFotosDeseadasPorElUsuarioYSumaUnSegundoQueHaraQueSeEjecuteUnTemporizador);
                muestra.IsBackground = true;
                muestra.Start();
                button1.Enabled = false;
            }

        }
        int cont = 0;
        private void muestraFotosDeseadasPorElUsuarioYSumaUnSegundoQueHaraQueSeEjecuteUnTemporizador()
        {
            while (true)
            {
                if (!userControl11.Corriendo)
                {
                    lock (l)
                    {
                        Monitor.Wait(l);
                    }
                }

                    flowLayoutPanel1.BackgroundImage = fotos[cont];
                    cont++;
                    if (cont >= fotos.Count)
                    {
                        cont = 0;
                    }

                    userControl11.YY++;
                Thread.Sleep(1000);
            }
        }

      


        private void userControl11_Load(object sender, EventArgs e)
        {
            if (userControl11.Corriendo)
            {
                lock (l)
                {
                    Monitor.Pulse(l);
                }
            }
        }

        private void userControl11_DesbordaTiempo(object sender, EventArgs e)
        {
            userControl11.XX++;
        }
    }
}
