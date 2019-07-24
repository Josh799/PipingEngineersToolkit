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

namespace PipingEngineersToolkit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double tempChangeNumber, pipeDiaNumber, reqLegLengthNumber, expCoeffNumber, lenChangeNumber;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CbExpCoeff_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cbExpCoeff.SelectedValue)
            {
                case "Carbon Steel":
                    expCoeffNumber = 0.000012;
                    break;
                case "Stainless Steel 304":
                    expCoeffNumber = 0.0000173;
                    break;
                case "Stainless Steel 316":
                    expCoeffNumber = 0.000016;
                    break;
            }
        }

        private void CbPipeDia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cbPipeDia.SelectedValue)
            {
                case "1/2":
                    pipeDiaNumber = 21.3;
                    break;
                case "3/4":
                    pipeDiaNumber = 26.7;
                    break;
                case "1":
                    pipeDiaNumber = 33.4;
                    break;
                case "1 1/2":
                    pipeDiaNumber = 48.3;
                    break;
                case "2":
                    pipeDiaNumber = 60.3;
                    break;
                case "2 1/2":
                    pipeDiaNumber = 73;
                    break;
                case "3":
                    pipeDiaNumber = 88.9;
                    break;
                case "3 1/2":
                    pipeDiaNumber = 101.6;
                    break;
                case "4":
                    pipeDiaNumber = 114.3;
                    break;
                case "5":
                    pipeDiaNumber = 141.3;
                    break;
                case "6":
                    pipeDiaNumber = 168.3;
                    break;
                case "8":
                    pipeDiaNumber = 219.1;
                    break;
                case "10":
                    pipeDiaNumber = 273;
                    break;
                case "12":
                    pipeDiaNumber = 323.8;
                    break;
                case "14":
                    pipeDiaNumber = 355.6;
                    break;
                case "16":
                    pipeDiaNumber = 406.4;
                    break;
                case "18":
                    pipeDiaNumber = 457;
                    break;
                case "20":
                    pipeDiaNumber = 508;
                    break;
                case "24":
                    pipeDiaNumber = 610;
                    break;
                case "26":
                    pipeDiaNumber = 666;
                    break;
                case "30":
                    pipeDiaNumber = 762;
                    break;
                case "32":
                    pipeDiaNumber = 813;
                    break;
                case "34":
                    pipeDiaNumber = 864;
                    break;
                case "36":
                    pipeDiaNumber = 914;
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool isDesignDouble = Double.TryParse(tbTempDesign.Text, out _);
            bool isAmbDouble = Double.TryParse(tbTempAmb.Text, out _);
            if (isDesignDouble)
            {
                if (isAmbDouble)
                {
                    tempChangeNumber = Convert.ToDouble(tbTempDesign.Text) - Convert.ToDouble(tbTempAmb.Text);
                    tbTempChange.Text = tempChangeNumber.ToString();
                }
                else
                {
                    // These could be red messages next to the box
                    MessageBox.Show("T2 - Amb is invalid");
                }
            }
            else
            {
                MessageBox.Show("T1 - Design is invalid");
            }

            bool isLenDouble = Double.TryParse(tbLen.Text, out _);
            if (isLenDouble)
            {
                lenChangeNumber = Convert.ToDouble(tbLen.Text) * expCoeffNumber * Convert.ToDouble(tbTempChange.Text);
                tbLenChange.Text = Convert.ToString(lenChangeNumber);
                reqLegLengthNumber = 66 * Math.Sqrt(pipeDiaNumber * Convert.ToDouble(tbLenChange.Text));
                tbReqLegLen.Text = Convert.ToString(Math.Round(reqLegLengthNumber));
            }
            else
            {
                MessageBox.Show("Length is invalid");
            }
        }
    }
}
