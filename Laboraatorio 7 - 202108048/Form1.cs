using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Laboraatorio_7___202108048
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<Propietario> propietarios = new List<Propietario>();
        List<Casa> casas = new List<Casa>();
        List<General> general = new List<General>();

        private void Form1_Load(object sender, EventArgs e)
        {
            LeerP();
            LeerC();
        }

        private void GuardarP()
        {
            FileStream stream = new FileStream("Propietarios.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter writer = new StreamWriter(stream);

            foreach (var p in propietarios)
            {
                writer.WriteLine(p.dpi);
                writer.WriteLine(p.nombre);
                writer.WriteLine(p.apellido);
             
            }

            writer.Close();
        }

        private void GuardarC()
        {
            FileStream stream = new FileStream("Casas.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter writer = new StreamWriter(stream);

            foreach (var c in casas)
            {
                writer.WriteLine(c.dpi);
                writer.WriteLine(c.Nocasa);
                writer.WriteLine(c.cuota);

            }

            writer.Close();
        }

        private void LeerP()
        {
            FileStream stream = new FileStream("Propietarios.txt", FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);

            while (reader.Peek() > -1)
            {
                Propietario pro = new Propietario();
                pro.dpi = Convert.ToInt32(reader.ReadLine());
                pro.nombre = reader.ReadLine();
                pro.apellido = reader.ReadLine();

                propietarios.Add(pro);

            }

            reader.Close();

        }

        private void LeerC()
        {
            FileStream stream = new FileStream("Casas.txt", FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);

            while (reader.Peek() > -1)
            {
                Casa casa = new Casa();
                casa.dpi = Convert.ToInt32(reader.ReadLine());
                casa.Nocasa = Convert.ToInt32(reader.ReadLine());
                casa.cuota = Convert.ToInt32(reader.ReadLine());

                casas.Add(casa);

            }

            reader.Close();

        }

        private void Ingresar_Click(object sender, EventArgs e)
        {
            Propietario pro = new Propietario();
            Casa casa = new Casa();
            pro.dpi = Convert.ToInt32(textBox1.Text);
            pro.nombre = textBox2.Text;
            pro.apellido = textBox3.Text;
            casa.dpi = Convert.ToInt32(textBox1.Text);
            casa.Nocasa = Convert.ToInt32(textBox4.Text);
            casa.cuota = Convert.ToInt32(textBox5.Text);

            propietarios.Add(pro);
            casas.Add(casa);

            GuardarP();
            GuardarC();
        }

        private void Mostrar(bool ordenada = false)
        {
          general.Clear();

            foreach (var p in casas)
            {
                General gen = new General();

                Propietario propietario = propietarios.Find(a => a.dpi == p.dpi);

                gen.dpi = propietario.dpi;
                gen.nombre = propietario.nombre;
                gen.apellido = propietario.apellido;
                gen.Nocasa = p.Nocasa;
                gen.cuota = p.cuota;

                general.Add(gen);
            }

            if (ordenada)
                general = general.OrderByDescending(r => r.cuota).ToList();

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = general;
            //por si no queremos mostrar el dpi
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Refresh();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            Mostrar(true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            int valMax = casas.Max(x => x.cuota);
            lblcuota.Text = valMax.ToString();

            lblpropietario.Text = general[0].nombre.ToString();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Mostrar(true);

            int cuantos = general.Count();

     
            labelb.Text = "Más altas: " + general[0].cuota.ToString() + "," + general[1].cuota.ToString() + "," + general[2].cuota.ToString();
           
            labela.Text = "Más bajas: " + general[cuantos - 1].cuota.ToString() + "," + general[cuantos - 2].cuota.ToString() + "," + general[cuantos - 3].cuota.ToString();
        }
    }
}
