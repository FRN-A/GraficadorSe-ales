using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GraficadorSeñales
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void graficar_Click(object sender, RoutedEventArgs e)
        {
            double amplitud = double.Parse(txtAmplitud.Text);
            double tiempoInicial = double.Parse(txtTiempoInicial.Text);
            double fase = double.Parse(txtFase.Text);
            double tiempoFinal = double.Parse(txtTiempoFinal.Text);
            double frecuencia = double.Parse(txtFrecuencia.Text);
            double frecuenciaMuestreo = double.Parse(txtFrecuenciaMuestreo.Text);

            SeñalSenoidal señal = new SeñalSenoidal(amplitud, fase, frecuencia);

         
            plnGrafica.Points.Clear();

            double periodoMuestro = 1 / frecuenciaMuestreo;
            for (double i = tiempoInicial; i <= tiempoFinal; i += periodoMuestro)
            {
                double valorMuestra = señal.evaluar(i);

                if (Math.Abs(valorMuestra) > señal.amplitudMaxima)
                {
                    señal.amplitudMaxima = Math.Abs(valorMuestra);
                }
                señal.muestras.Add(new Muestra(i, valorMuestra));
                
            }

            //recorrer una coleccion o arreglo
            foreach (Muestra muestra in señal.muestras)
            {
                plnGrafica.Points.Add(new Point(muestra.x * scrContenedor.Width, (muestra.y * ((scrContenedor.Height / 2.0) - 30) * -1) + (scrContenedor.Height / 2)));
            }
        }
    }
}

