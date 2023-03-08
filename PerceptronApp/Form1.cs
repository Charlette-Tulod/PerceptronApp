using System;
using System.Windows.Forms;

namespace PerceptronApp
{
    public partial class Form1 : Form
    {
        private Perceptron perceptron;
        public Form1()
        {
            InitializeComponent();

            // Initialize the perceptron with 2 input neurons
            perceptron = new Perceptron(2);

            // Will be enable after training
            btnPredict.Enabled = false;
            reset2.Enabled = false;


            // Set default values for the learning rate and number of iterations
            txtLearningRate.Text = "0.1";
            txtNumIterations.Text = "1000";

        }

        private void btnPredict_Click(object sender, EventArgs e)
        {
            // Get the input values from the text boxes
            double[] inputs = new double[2];
            inputs[0] = Convert.ToDouble(txtInput1.Text);
            inputs[1] = Convert.ToDouble(txtInput2.Text);

            // Use the perceptron to predict the output value
            double output = perceptron.Predict(inputs);

            // Display the output value in the result label
            lblResult.Text = output.ToString();
        }

        private void btnTrain_Click(object sender, EventArgs e)
        {
            // Get the input values from the text boxes
            double[] inputs1 = new double[] { 0, 0 };
            double[] inputs2 = new double[] { 0, 1 };
            double[] inputs3 = new double[] { 1, 0 };
            double[] inputs4 = new double[] { 1, 1 };

            double[] outputs = new double[] { 0, 0, 0, 1 };

            // Get the learning rate and number of iterations from the text boxes
            double learningRate = Convert.ToDouble(txtLearningRate.Text);
            int numIterations = Convert.ToInt32(txtNumIterations.Text);

            // Train the perceptron with the given inputs and outputs, learning rate, and number of iterations
            perceptron.Train(new double[][] { inputs1, inputs2, inputs3, inputs4 }, outputs, learningRate, numIterations);

            // Enable the predict button after training
            btnPredict.Enabled = true;
            reset2.Enabled = true;

            label8.Text = "Trained!";
        }

        private void reset2_Click(object sender, EventArgs e)
        {
            txtInput1.Clear();
            txtInput2.Clear();
        }

        private void reset1_Click(object sender, EventArgs e)
        {
            txtLearningRate.Clear();
            txtNumIterations.Clear();
        }
    }

    public class Perceptron
    {
        private double[] weights;
        private double bias;

        public Perceptron(int inputSize)
        {
            weights = new double[inputSize];
            for (int i = 0; i < inputSize; i++)
            {
                weights[i] = 0;
            }
            bias = 0;
        }

        public double Predict(double[] inputs)
        {
            double weightedSum = 0;
            for (int i = 0; i < inputs.Length; i++)
            {
                weightedSum += inputs[i] * weights[i];
            }
            weightedSum += bias;
            return Activate(weightedSum);
        }

        public void Train(double[][] inputs, double[] outputs, double learningRate, int numIterations)
        {
            for (int iteration = 0; iteration < numIterations; iteration++)
            {
                for (int i = 0; i < inputs.Length; i++)
                {
                    double prediction = Predict(inputs[i]);
                    double error = outputs[i] - prediction;

                    for (int j = 0; j < weights.Length; j++)
                    {
                        weights[j] += learningRate * error * inputs[i][j];
                    }

                    bias += learningRate * error;
                }
            }
        }

        private double Activate(double weightedSum)
        {
            if (weightedSum > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
