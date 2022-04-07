using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuronTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;

namespace NeuronTask.Tests
{
    [TestClass()]
    public class NeuralNetworkTests
    {
        [TestMethod()]
        public void FeedForwardTest()
        {
            var topology = new Topology(4, 1, 0.000001, 2);
            var neuralNetwork = new NeuralNetwork(topology);

            var result = neuralNetwork.FeedForward();
        }
    }
}