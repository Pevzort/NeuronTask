using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuronTask
{
    public class Neuron
    {
        public List<double> Weights { get; }
        public List<double> Inputs { get; }
        public NeuronType NeuronType { get; }
        public double Output { get; private set; }
        public double Delta { get; private set; }

        public Neuron(int inputCount, NeuronType type = NeuronType.Hidden)
        {
            NeuronType = type;
            Weights = new List<double>();
            Inputs = new List<double>();

            InitWeightsRandomValues(inputCount);
        }

        private void InitWeightsRandomValues(int inputCount)
        {
            var rand = new Random();
            for (int i = 0; i < inputCount; i++)
            {
                if(NeuronType == NeuronType.Input)
                {
                    //Weights.Add(1);
                    Weights.Add(rand.NextDouble());
                }
                else
                {
                    Weights.Add(rand.NextDouble());
                }
                Inputs.Add(0);
            }
        }

        public double FeedForward(List<double> inputs)
        {
            for(int i = 0; i < inputs.Count; i++)
            {
                Inputs[i] = inputs[i];
            }

            var sum = 0.0;
            for(int i=0; i < inputs.Count; i++)
            {
                sum += inputs[i] * Weights[i];
            }

            if(NeuronType!= NeuronType.Input)
            {
                Output = Sigmoid(sum);
            }
            else
            {
                Output = sum;
            }
            return Output;
        }

        private double Sigmoid(double x)
        {
            //var result = 1.0 / (1.0 + Math.Exp(-x));
            var result = Math.Sqrt(x);
            //return 1.0 / (1.0 + Math.Exp(-x));
            return result;
        }

        private double SigmoidDx(double x)
        {
            //var result = 1.0 / (1.0 + Math.Exp(-x));
            var result = 1 / 2 * Math.Sqrt(x);
            //return 1.0 / (1.0 + Math.Exp(-x));
            return result;
        }

        public void Learn(double error, double learningRate)
        {
            if(NeuronType == NeuronType.Input)
            {
                return;
            }

            Delta = error * SigmoidDx(Output);

            for(int i = 0; i < Weights.Count; i++)
            {
                var weight = Weights[i];
                var input = Inputs[i];

                var newweight = weight - input * Delta * learningRate;
                Weights[i] = newweight;
            }
        }

        public override string ToString()
        {
            return Output.ToString();
        }
    }
}
