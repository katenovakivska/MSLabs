using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MS1
{
    public partial class Charts : Form
    {
        Random random = new Random();
        int amount = 10000;
        //конструктор
        public Charts()
        {
            InitializeComponent();
            FillChart1();
            FillChart2();
            FillChart3();
        }
        //генерація значень для експоненційного розподілу та побудова графіку функції розподілу
        public void FillChart1()
        {
            List<double> randoms = new List<double>();
            List<double> sortedX = new List<double>();

            double lambda = random.Next();

            for (int i = 0; i < amount; i++)
            {
                double x = (-1 / lambda) * Math.Log(random.NextDouble());
                double y = 1 - Math.Exp(-lambda * x);
                sortedX.Add(x);
                randoms.Add(x);
                chart1.Series["Task1"].Points.AddXY(x, y);
            }

            List<Point> points = FillHistogram(sortedX, "Histogram1");
            double dispersion = Dispersion(randoms);
            double medium = Medium(randoms);
            textBox1.Text = dispersion.ToString();
            textBox4.Text = medium.ToString();

            PirsonForExponential(sortedX, points, dispersion, medium);
        }
        //генерація значень для нормального розподілу та побудова графіку функції розподілу
        public void FillChart2()
        {
            int size = 12;
            double sigma = random.NextDouble();
            double a = random.Next(0, 10);
            List<double> sortedX = new List<double>();
            double[] generatedNumbers = new double[amount];
            List<double> randoms = new List<double>();
            for (int i = 0; i < amount; i++)
            {
                generatedNumbers[i] = random.NextDouble();
            }
            for (int i = 0; i < amount; i++)
            {
                double averageEstimate = new double();

                if (amount - i < 12)
                {
                    size = amount - i;
                }
                for (int j = i; j < i + size - 1; j++)
                {
                    averageEstimate += generatedNumbers[j];
                }
                averageEstimate -= 6;
                double x = sigma * averageEstimate + a;
                double y = (Math.Exp(-Math.Pow(x - a, 2) / (2 * Math.Pow(sigma, 2))) / sigma * Math.Sqrt(2 * Math.PI));
                sortedX.Add(x);
                randoms.Add(x);
                chart2.Series["Task2"].Points.AddXY(x, y);
            }

            List<Point> points = FillHistogram(sortedX, "Histogram2");

            double dispersion = Dispersion(randoms);
            double medium = Medium(randoms);

            textBox2.Text = dispersion.ToString();
            textBox5.Text = medium.ToString();

            PirsonForNormal(sortedX, points, dispersion, medium);
        }
        //генерація значень для рівномірного розподілу та побудова графіку функції розподілу
        public void FillChart3()
        {
            Random random = new Random();
            double a = Math.Pow(5, 13);
            double c = Math.Pow(2, 31);
            List<double> randoms = new List<double>();
            List<double> sortedX = new List<double>();

            double previousZ = random.NextDouble();

            for (int i = 0; i < amount; i++)
            {
                double z = a * previousZ % c;
                double x = z / c;
                sortedX.Add(x);
                randoms.Add(x);
                chart5.Series["Task3"].Points.AddXY(z, x);
                previousZ = z;
            }

            List<Point> points = FillHistogram(sortedX, "Histogram3");

            textBox3.Text = Dispersion(randoms).ToString();
            textBox6.Text = Medium(randoms).ToString();

            PirsonForSteady(sortedX, points);
        }
        //метод для побудови гістограм
        public List<Point> FillHistogram(List<double> sortedX, string histogram)
        {
            sortedX = sortedX.OrderBy(x => x).ToList();

            double h = (sortedX.Max() - sortedX.Min()) / 20;
            double hLeft = sortedX.Min();
            double hRight = hLeft + h;
            int counter = 0;
            List<Point> points = new List<Point> ();

            for (int i = 0; i < sortedX.Count; i++)
            {
                if (sortedX[i] >= hLeft && sortedX[i] < hRight)
                {
                    counter++;
                }
                else
                {
                    hLeft += h;
                    hRight += h;
                    points.Add(new Point(sortedX[i], counter));
                    AddPoint(sortedX[i], counter, histogram);
                    counter = 0;
                }
            }

            return points;
        }
        //метод додавання точок до гістограм
        public void AddPoint(double x, double y, string histogram)
        {
            if (histogram == "Histogram1")
            {
                chart3.Series[histogram].Points.AddXY(x, y);
            }
            else if (histogram == "Histogram2")
            {
                chart4.Series[histogram].Points.AddXY(x, y);
            }
            else if (histogram == "Histogram3")
            {
                chart6.Series[histogram].Points.AddXY(x, y);
            }
        }
        //метод для пошуку середнього значення вибірки
        public double Medium(List<double> randoms)
        {
            return randoms.Sum() / randoms.Count;
        }
        //метод для пошуку дисперсії
        public double Dispersion(List<double> randoms)
        {
            double medium = Medium(randoms);
            double sum = 0;
            for (int i = 0; i < randoms.Count; i++)
            {
                double n = Math.Pow((randoms[i] - medium), 2);
                sum += n;
            }
            return sum / randoms.Count;
        }
        //перевірка на експоненційність розподілу за критерієм пірсона
        public void PirsonForExponential(List<double> sortedX, List<Point> points, double dispersion, double medium)
        {
            double[] p = new double[points.Count];
            List<double> nTheoretical = new List<double>();
            double average = Math.Sqrt(dispersion);
            double lambda = 1 / average;
            for (int i = 0; i < points.Count; i++)
            {
                if (i == 0)
                {
                    p[i] = 1 - Math.Exp(-lambda * points[i].x);
                }
                else
                {
                    p[i] = Math.Exp(-lambda * points[i - 1].x) - Math.Exp(-lambda * points[i].x);
                }
                nTheoretical.Add(p[i] * sortedX.Count);
            }

            CombineFrequency(points, nTheoretical);
            double hiApproximate = FindHiApproximate(points, nTheoretical);

            int s = points.Count;
            int k = s - 2;
            double[] hiTable = new double[] { 0, 6.6, 9.2, 11.3, 13.3, 15.1, 16.8, 18.5, 20.1, 21.7, 23.2,
            24.7, 26.2, 27.7, 29.1, 30.6, 32.0, 33.4,  34.8, 36.2}; 
            double hiCritical = hiTable[k];
            textBox9.Text = hiCritical.ToString();
            textBox10.Text = hiApproximate.ToString();
            string isTrue = CompareHi(hiApproximate, hiCritical);
            textBox13.Text = isTrue;
        }
        //перевірка на рівномірність розподілу за критерієм пірсона
        public void PirsonForSteady(List<double> sortedX, List<Point> points)
        {
            double[] p = new double[points.Count];
            List<double> nTheoretical = new List<double>();

            for (int i = 0; i < points.Count; i++)
            {
                if (i == 0)
                {
                    p[i] = (points[i].x) / (sortedX.Max() - sortedX.Min());
                }
                else
                {
                    p[i] = (points[i].x - points[i - 1].x) / (sortedX.Max() - sortedX.Min());
                }

                nTheoretical.Add(p[i] * sortedX.Count);
            }

            CombineFrequency(points, nTheoretical);
            double hiApproximate = FindHiApproximate(points, nTheoretical);

            int s = points.Count;
            int k = s - 3;
            double[] hiTable = new double[] { 0, 6.6, 9.2, 11.3, 13.3, 15.1, 16.8, 18.5, 20.1, 21.7, 23.2,
            24.7, 26.2, 27.7, 29.1, 30.6, 32.0, 33.4, 34.8, 36.2}; 
            double hiCritical = hiTable[k];
            textBox11.Text = hiCritical.ToString();
            textBox12.Text = hiApproximate.ToString();
            string isTrue = CompareHi(hiApproximate, hiCritical);
            textBox14.Text = isTrue;
        }
        //перевірка на нормальність розподілу за критерієм пірсона
        public void PirsonForNormal(List<double> sortedX, List<Point> points, double dispersion, double medium)
        {
            double[] z = new double[points.Count];
            double[] fz = new double[points.Count];
            List<double> nTheoretical = new List<double> ();
            double h = (sortedX.Max() - sortedX.Min()) / 20;
            double f(double x) => 1 / Math.Sqrt(2 * Math.PI * dispersion) * Math.Exp(-Math.Pow((x - medium), 2) / (2 * dispersion));
            for (int i = 0; i < points.Count; i++)
            {
                if (i == 0)
                {
                    fz[i] = Simpson(f, sortedX.Min(), points[i].x, 200);
                }
                else
                {
                    fz[i] = Simpson(f, points[i - 1].x, points[i].x, 200);
                }

                nTheoretical.Add(fz[i] * 10000);
            }
            CombineFrequency(points, nTheoretical);
            double hiApproximate = FindHiApproximate(points, nTheoretical);

            int s = points.Count;
            int k = s - 3;
            double[] hiTable = new double[] { 0, 6.6, 9.2, 11.3, 13.3, 15.1, 16.8, 18.5, 20.1, 21.7, 23.2,
            24.7, 26.2, 27.7, 29.1, 30.6, 32.0, 33.4, 34.8, 36.2};
            double hiCritical = hiTable[k];
            textBox7.Text = hiCritical.ToString();
            textBox8.Text = hiApproximate.ToString();

            string isTrue = CompareHi(hiApproximate, hiCritical);
            textBox15.Text = isTrue;
        }
        
        //метод для порівняння значень хі-критеріїв (наближеного та критичного)
        public string CompareHi(double hiApproximate, double hiCritical)
        {
            string isTrue;
            if (hiApproximate <= hiCritical)
            {
                isTrue = "true";
            }
            else
            {
                isTrue = "false";
            }

            return isTrue;
        }
        //метод для об'єднування частот
        public void CombineFrequency(List<Point> points, List<double> nTheoretical)
        {
            int j = 0;
            while (j < points.Count)
            {
                if (points[j].n <= 5 && j != points.Count - 1)
                {
                    points[j + 1].n += points[j].n;
                    nTheoretical[j + 1] += nTheoretical[j];
                    points.RemoveAt(j);
                    nTheoretical.RemoveAt(j);
                    j--;
                }
                else if (points[j].n <= 5 && j == points.Count - 1)
                {
                    points[j - 1].n += points[j].n;
                    nTheoretical[j - 1] += nTheoretical[j];
                    points.RemoveAt(j);
                    nTheoretical.RemoveAt(j);
                }
                j++;
            }
        }
        //метод для розрахунку значення хі-критерію наближеного
        public double FindHiApproximate(List<Point> points, List<double> nTheoretical)
        {
            double hiApproximate = new double();
            for (int i = 0; i < points.Count; i++)
            {
                hiApproximate += (Math.Pow(points[i].n - nTheoretical[i], 2) / nTheoretical[i]);
            }

            return hiApproximate;
        }
        //метод для пошуку інтегралу методом сімпсона
        private static double Simpson(Func<double, double> f, double a, double b, int n)
        {
            var h = (b - a) / n;
            var sum1 = 0d;
            var sum2 = 0d;
            for (var k = 1; k <= n; k++)
            {
                var xk = a + k * h;
                if (k <= n - 1)
                {
                    sum1 += f(xk);
                }

                var xk1 = a + (k - 1) * h;
                sum2 += f((xk + xk1) / 2);
            }

            var result = h / 3d * (1d / 2d * f(a) + sum1 + 2 * sum2 + 1d / 2d * f(b));
            return result;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var series in chart1.Series)
            {
                series.Points.Clear();
            }
            foreach (var series in chart2.Series)
            {
                series.Points.Clear();
            }
            foreach (var series in chart3.Series)
            {
                series.Points.Clear();
            }
            foreach (var series in chart4.Series)
            {
                series.Points.Clear();
            }
            foreach (var series in chart5.Series)
            {
                series.Points.Clear();
            }
            foreach (var series in chart6.Series)
            {
                series.Points.Clear();
            }
            textBox1.Text = String.Empty;
            textBox2.Text = String.Empty;
            textBox3.Text = String.Empty;
            textBox4.Text = String.Empty;
            textBox5.Text = String.Empty;
            textBox6.Text = String.Empty;
            textBox7.Text = String.Empty;
            textBox8.Text = String.Empty;
            textBox9.Text = String.Empty;
            textBox10.Text = String.Empty;
            textBox11.Text = String.Empty;
            textBox12.Text = String.Empty;
            textBox13.Text = String.Empty;
            textBox14.Text = String.Empty;
            textBox15.Text = String.Empty;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FillChart1();
            FillChart2();
            FillChart3();
        }

        private void chart1_Click(object sender, EventArgs e)
        {
        }

        private void chart2_Click(object sender, EventArgs e)
        {

        }

        private void chart3_Click(object sender, EventArgs e)
        {

        }

        private void chart5_Click(object sender, EventArgs e)
        {

        }

        private void chart6_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox13_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox14_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox15_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
