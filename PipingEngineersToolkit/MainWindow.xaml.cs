using HelixToolkit.Wpf;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace PipingEngineersToolkit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double tempChangeNumber, pipeDiaNumber, reqLegLengthNumber, expCoeffNumber, lenChangeNumber;
        TubeVisual3D origTube = new TubeVisual3D();
        TubeVisual3D expTube = new TubeVisual3D();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void DrawOriginalPipe(Point3D originalStartPoint, Point3D originalEndPoint, double diameter)
        {
            origTube.Path = new Point3DCollection();
            origTube.Path.Add(originalStartPoint);
            origTube.Path.Add(originalEndPoint);
            origTube.Diameter = diameter;
            origTube.Fill = Brushes.Red;

            MainViewPort.Children.Remove(origTube);
            MainViewPort.Children.Add(origTube);
        }

        private void DrawExpansionPipe(Point3D originalEndPoint, Point3D cornerPoint, Point3D expansionEndPoint, double diameter)
        {
            expTube.Path = new Point3DCollection();
            expTube.Path.Add(originalEndPoint);
            expTube.Path.Add(cornerPoint);
            expTube.Path.Add(expansionEndPoint);
            expTube.Diameter = diameter;
            expTube.Fill = Brushes.Blue;

            MainViewPort.Children.Remove(expTube);
            MainViewPort.Children.Add(expTube);
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

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            bool isDesignDouble = Double.TryParse(tbTempDesign.Text, out _);
            bool isAmbDouble = Double.TryParse(tbTempAmb.Text, out _);
            bool isLenDouble = Double.TryParse(tbLen.Text, out _);

            if (isDesignDouble && isAmbDouble && isLenDouble == true)
            {
                tempChangeNumber = Convert.ToDouble(tbTempDesign.Text) - Convert.ToDouble(tbTempAmb.Text);
                tbTempChange.Text = tempChangeNumber.ToString();

                lenChangeNumber = Convert.ToDouble(tbLen.Text) * expCoeffNumber * Convert.ToDouble(tbTempChange.Text);
                tbLenChange.Text = Convert.ToString(lenChangeNumber);
                reqLegLengthNumber = 66 * Math.Sqrt(pipeDiaNumber * Convert.ToDouble(tbLenChange.Text));
                tbReqLegLen.Text = Convert.ToString(Math.Round(reqLegLengthNumber));

                // Pipe Drawing
                DataContext = this;

                double x2 = Convert.ToDouble(tbLen.Text);
                double x3 = Convert.ToDouble(tbLen.Text) + Convert.ToDouble(tbLenChange.Text);
                double y4 = Convert.ToDouble(tbReqLegLen.Text);
                var pt1 = new Point3D(0, 0, 0);
                var pt2 = new Point3D(x2, 0, 0);
                var pt3 = new Point3D(x3, 0, 0);
                var pt4 = new Point3D(x3, y4, 0);
                DrawOriginalPipe(pt1, pt2, pipeDiaNumber);
                DrawExpansionPipe(pt2, pt3, pt4, pipeDiaNumber);
                // Camera Control
                //var lookAtPoint = new Point3D(x3 / 2, y4 / 2, 0);
                //MainViewPort.Camera.LookAt(lookAtPoint, 0);
                MainViewPort.Camera.Position = new Point3D(x3 / 2, y4 / 2, 0);
                MainViewPort.Camera.LookDirection = new Vector3D();
                MainViewPort.Camera.UpDirection = new Vector3D(0, 0, 0);
            }
            else if (isDesignDouble == false)
            {
                MessageBox.Show("T1 - Design is invalid");
            }
            else if (isAmbDouble == false)
            {
                // These could be red messages next to the box
                MessageBox.Show("T2 - Amb is invalid");
            }
            else if (isLenDouble == false)
            {
                MessageBox.Show("Length is invalid");
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            tbTempDesign.Text = null;
            tbTempAmb.Text = null;
            cbPipeDia.SelectedIndex = -1;
            tbLenChange.Text = null;
            tbLen.Text = null;
            cbExpCoeff.SelectedIndex = -1;
            tbTempChange.Text = null;
            tbReqLegLen.Text = null;

            MainViewPort.Children.Remove(origTube);
            MainViewPort.Children.Remove(expTube);
        }
    }
}