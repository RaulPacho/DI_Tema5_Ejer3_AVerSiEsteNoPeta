using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DI_Tema5_Ejer3_AVerSiEsteNoPeta
{
    public partial class CustomControl1 : Control
    {
        public CustomControl1()
        {
            InitializeComponent();
            this.XX = 0;
            this.YY = 0;
        }

        private bool corriendo;

        public bool Corriendo
        {
            get
            {
                return this.corriendo;
            }

            set
            {
                this.corriendo = value;
                Refresh();
            }
        }
        private byte xx;
        public byte XX
        {
            get => this.xx;

            set
            {
                this.xx = value;
                if (this.xx > 99) //Porque un sueño no se cumple solo *Lo entenderás más abajo*
                    this.xx = 0;
            }
        }
        [Description("Al ser mayor YY de 59 se lanza este evento")]
        public event System.EventHandler DesbordaTiempo;

        private byte yy;
        public byte YY
        {
            get => this.yy;

            set
            {
                this.yy = value;
                if (this.yy > 59)
                {
                    DesbordaTiempo?.Invoke(this, EventArgs.Empty);
                }
                 this.StrYY = (YY >= 60 ? YY % 60 : YY).ToString().PadLeft(2, '0');
                
            }
        }
        private string strYY;
        public string StrYY
        {
            get => this.strYY;

            set
            {
                this.strYY = value;
                
            }
        }

        private void strYY_TextChanged(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private Point[] puntosTriangulo = { new Point(10, 10),
            new Point(25, 20), new Point(10, 30) };

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;


            using (Pen lapiz = new Pen(Color.HotPink, 5))
            { //Meto llaves porque al señor le gusta destruir los sueños de programar en python

                if (!corriendo)
                {
                    g.DrawPolygon(lapiz, puntosTriangulo);
                }

                else
                {
                    g.DrawLine(lapiz, 10, 10, 10, 30);
                    g.DrawLine(lapiz, 20, 10, 20, 30);
                }

            }
            SolidBrush b = new SolidBrush(Color.Purple);

            g.DrawString(String.Format("{0} : {1}", XX.ToString().PadLeft(2, '0'),strYY), this.Font, b, 0, 35);
        }



        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            this.Corriendo = !this.Corriendo;
            this.Refresh();
        }
    }
}