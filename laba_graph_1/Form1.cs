using System.ComponentModel.DataAnnotations.Schema;

namespace laba_graph_1
{
    public partial class Form1 : Form
    {
        private Graphics g;
        Func<double, double> func;
        //Form2 f2 = new Form2();

        public string X_1;
        public string X_2;
        public Form1()
        {
            InitializeComponent();
            g = this.CreateGraphics();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string t = listBox1.SelectedItem.ToString();
            if (t == "sin(x)")
                func = Math.Sin;
            else if (t == "cos(x)")
                func = Math.Cos;
            else if (t == "x^2")
                func = p => (double)Math.Pow(p, 2);
            else if (t == "x^3")
                func = p => (double)Math.Pow(p, 3);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Form2(X_1, X_2, func).ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            X_1 = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            X_2 = textBox2.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Form3().ShowDialog();
        }
    }
}