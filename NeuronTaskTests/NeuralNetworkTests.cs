using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuronTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;
using NeuronTaskTests;

namespace NeuronTask.Tests
{
    [TestClass()]
    public class NeuralNetworkTests
    {
        [TestMethod()]
        public void FeedForwardTest()
        {
            var dataset = GetDataset(0, 100, 10);

            var topology = new Topology(1, 1, 0.0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001, 15);
            var neuralNetwork = new NeuralNetwork(topology);
            var diffPow2 = neuralNetwork.Learn(dataset, 1000000);

            var results = new List<float>();
            foreach(var data in dataset)
            {
                results.Add((float)neuralNetwork.FeedForward(data.Item2).Output);
            }

            List<string> a = new List<string>();
            List<PointF> approc = new List<PointF>();
            int x = 1;

            for(int i = 0; i < results.Count; i++)
            {
                var expected = dataset[i].Item1;
                var actual = results[i];

                a.Add(expected + "\t->\t" + actual);

                approc.Add(new PointF(x, results[i]));
                x += 10;
            }
            a.Add("ABS error: " + diffPow2.ToString());

            a.Add("\nTestResult [85]: " + neuralNetwork.FeedForward(85).Output.ToString());
            a.Add("TestResult [542]: " + neuralNetwork.FeedForward(542).Output.ToString());

            NeuronForm neuronform = new NeuronForm(a, dataset, approc);
            neuronform.ShowDialog();
        }

        public List<PointF> GetApproc(List<PointF> res)
        {
            return res;
        }

        public void SetSeries(Chart chart, string name, params PointF[] points)
        {
            chart.Series.Add(name);
            Series series = chart.Series.FindByName(name);
            series.ChartType = SeriesChartType.FastLine;
            series.BorderWidth = 3;

            foreach(PointF point in points)
            {
                series.Points.AddXY(point.X, point.Y);
            }
        }

        public PointF[] GetSQRT(int start, int end, int step)
        {
            List<PointF> points = new List<PointF>();

            for(int i = start; i < end; i += step)
            {
                points.Add(new PointF(i, (float)Math.Sqrt(i)));
            }

            return points.ToArray();
        }

        public List<(double, double[])> GetDataset(int start, int end, int step)
        {
            List<(double, double[])> dataset = new List<(double, double[])>();

            for(int i = start; i < end; i += step)
            {
                (double, double[]) n = (Math.Round(Math.Sqrt(i), 3), new double[] { i });
                dataset.Add(n);
            }

            return dataset;
        }
    }
}