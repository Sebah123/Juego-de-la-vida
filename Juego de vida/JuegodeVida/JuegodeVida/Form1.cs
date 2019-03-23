using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JuegodeVida
{
    public partial class Form1 : Form
    {
        private int longitud = 25;
        private int longitudPixel = 20;
        int cont = 1;
        int[,] celulas = new int[,] {
                                { 0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0 },
                                { 0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,0,0,0,0,0,0 },
                                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                                { 0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0 },
                                { 0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0 },
                                { 0,0,1,1,1,1,1,1,1,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0 },
                                { 0,0,1,0,0,0,0,1,0,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0 },
                                { 0,0,1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0 },
                                { 0,0,1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0 },
                                { 0,0,1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0 },
                                { 0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0 },
                                { 0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0 },
                                { 0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0 },
                                { 0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                                { 0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                                { 0,0,0,0,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                                { 0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0 },
                                { 0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,0,0,0,0 },
                                { 0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0 },
                                { 0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0 },
                                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },

                            };

        public Form1()
        {
            InitializeComponent();

            //inicializamos
            //celulas = new int[longitud, longitud];
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            PintarMatriz();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            List<string> lineas;

            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "Archivos de Texto (*.txt)|*.txt";

            dialog.Title = "Seleccione el archivo de Entrada";

            dialog.FileName = string.Empty;

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                LLenarMatriz(Convert.ToString(dialog.FileName));
            }
        }
        private void LLenarMatriz(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                MessageBox.Show("No hay una hoja para leer");
            }
            else
            {
                try
                {
                    var lines = GetLines(fileName);
                    if(lines.Count == 0)
                    {
                        MessageBox.Show("Archivo Vacío");
                    }
                    else
                    {
                        int[] lineaArchivo;
                        celulas = new int[lines.Count, lines.Count];
                        for(int y = 0; y<lines.Count; y++)
                        {
                            var line = lines[y];
                            lineaArchivo = line.Split(',').Select(x => x.Trim()).Select(x => int.Parse(x)).ToArray();

                            for(int x = 0; x < lineaArchivo.Length; x++)
                            {
                                celulas[y, x] = lineaArchivo[x];
                            }
                        }
                        PintarMatriz();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error, Verificar Archivo", ex.Message);
                }
            }
        }

        static List<string> GetLines(string inputFile)
        {
            List<string> lines = new List<string>();
            using (var reader = new StreamReader(inputFile))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        lines.Add(line);
                    }
                }
            }
            return lines;
        }
        private void PintarMatriz()
        {
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            for (int x = 0; x < longitud; x++)
            {
                for (int y = 0; y < longitud; y++)
                {
                    if (celulas[x, y] == 0)
                        PintarPixel(bmp, x, y, Color.White);
                    else
                        PintarPixel(bmp, x, y, Color.Black);
                }
            }
            pictureBox1.Image = bmp;
        }
        private void JuegoDeLaVida()
        {
            int[,] celulasTemp = new int[longitud, longitud];
            for (int x = 0; x < longitud; x++)
            {
                for (int y = 0; y < longitud; y++)
                {
                    if (celulas[x, y] == 0)
                        celulasTemp[x, y] = ReglaJuegoVida(x, y, false);
                    else
                        celulasTemp[x, y] = ReglaJuegoVida(x, y, true);
                }
            }
            celulas = celulasTemp;
        }
        private int ReglaJuegoVida(int x, int y, bool EsViva)
        {
            int VecinasVivas = 0;

            //vecina 1 
            if (x > 0 && y > 0)
                if (celulas[x - 1, y - 1] == 1)
                    VecinasVivas++;
            //vecina 2
            if (y > 0)
                if (celulas[x, y - 1] == 1)
                    VecinasVivas++;
            //vecina 3
            if (x < longitud - 1 && y > 0)
                if (celulas[x + 1, y - 1] == 1)
                    VecinasVivas++;
            //vecina4
            if (x > 0)
                if (celulas[x - 1, y] == 1)
                    VecinasVivas++;
            //vecina5
            if (x < longitud - 1)
                if (celulas[x + 1, y] == 1)
                    VecinasVivas++;
            //vecina6
            if (x > 0 && y < longitud - 1)
                if (celulas[x - 1, y + 1] == 1)
                    VecinasVivas++;
            //vecina7
            if (y < longitud - 1)
                if (celulas[x, y + 1] == 1)
                    VecinasVivas++;
            //vecina 8
            if (x < longitud - 1 && y < longitud - 1)
                if (celulas[x + 1, y + 1] == 1)
                    VecinasVivas++;

            if (EsViva)
                return (VecinasVivas == 2 || VecinasVivas == 3) ? 1 : 0;
            else
                return VecinasVivas == 3 ? 1 : 0;

        }
        //(x * longitudPixel)
        private void PintarPixel(Bitmap bmp, int x, int y, Color color)
        {
            for (int i = 0; i < longitudPixel; i++)
                for (int j = 0; j < longitudPixel; j++)
                    bmp.SetPixel(i + (y * longitudPixel), j + (x * longitudPixel), color );
            
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            JuegoDeLaVida();
            PintarMatriz();
            lbl_text.Text = cont+"";
            cont++;   
        }
        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true; 
        }
        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false; 
        }
        private void pictureBox1_Paint_1(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Pen p = new Pen(Color.Black);

            //lineas horizontales
            for (int y = 0; y <= longitud; ++y)
            {
                g.DrawLine(p, 0, y * longitudPixel, longitud * longitudPixel, y * longitudPixel);
            }
            //lineas verticales
            for (int x = 0; x <= longitud; ++x)
            {
                g.DrawLine(p, x * longitudPixel, 0, x * longitudPixel, longitud * longitudPixel);
            }
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
