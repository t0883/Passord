using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Passord
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string LösenordsGenerator(int length)
        {
            //Alla tecken som ska vara med i lösenordet
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!#¤%&/()=?@£$€{[]}";

            StringBuilder res = new StringBuilder();

            Random rnd = new Random();

            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtSlumpmässigtLösenord.Text = LösenordsGenerator(20);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Hämta alla värdena
            string hemsida = txtHemsida.Text;
            string lösenord = txtPassword.Text;
            string pinkod = txtPinkod.Text;

            //Skapa en string för det krypterade lösenordet
            string krypteratLösenord = "";

            //Hämta längden av lösenordet
            int lösenordLenght = lösenord.Length;

            //Lägg till mellanslag på lösenordet
            for (int i = 0; i < pinkod.Length; i++)
            {
                lösenord += " ";
            }

            for (int i = 0; i < lösenordLenght; i += pinkod.Length)
            {
                string strSub = lösenord.Substring(i, pinkod.Length);

                for (int j = 0; j < strSub.Length; j++)
                {
                    int pos = Convert.ToInt32(pinkod.Substring(j, 1)) - 1;

                    krypteratLösenord+= strSub.Substring(pos, 1);
                }
            }
            //Skapa filePath
            string filePath = "test.txt";

            //Konvertera den till en string
            string connectionString = File.ReadAllText(filePath);

            //Skapa en ny anslutning till databasen
            MySqlConnection conn = new MySqlConnection(connectionString);

            //Skapa Query
            string insertQuery = $"INSERT INTO passord.lösenord VALUES ('{hemsida}', '{krypteratLösenord}')";

            //Öpnna anslutningen
            conn.Open();

            //Skapa kommandot till databasen
            MySqlCommand cmd = new MySqlCommand(insertQuery, conn);

            //Exekvera kommandot
            cmd.ExecuteReader();

            //Stänga anslutningen till databasen
            conn.Close();

            //Bekräftelse att allt har gått igenom
            MessageBox.Show($"Lösenordet har sparats i databasen. Kom ihåg din pinkod. Pinkoden är {pinkod}");

            //Tömmer alla textboxarna efter att lösenordet har sparats i databasen
            txtHemsida.Clear();
            txtPassword.Clear();
            txtPinkod.Clear();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string hemsida = txtHämtaHemsida.Text;

            string avkrypteratLösenord = "";

            //Skapa filePath
            string filePath = "test.txt";

            //Konvertera den till en string
            string conncectionString = File.ReadAllText(filePath);

            //Skapa en ny connection till databasen
            MySqlConnection conn = new MySqlConnection(conncectionString);

            //Skapa Query
            string query = $"SELECT * FROM passord.lösenord WHERE Hemsida = '{hemsida}';";

            //Öppna connectionen
            conn.Open();

            //Skapa kommando till databasen
            MySqlCommand cmd = new MySqlCommand(query, conn);

            //Exekverar kommandot och spara datan i reader
            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows) 
            {
                while(reader.Read())
                {
                    string krypteratLösenord = reader.GetString(1);
                    string pinkod = txtHämtaPinkod.Text;
                    

                    int krypteratLösenordLength = krypteratLösenord.Length;

                    for (int i = 0; i < pinkod.Length; i++)
                    {
                        krypteratLösenord += " ";
                    }

                    for (int i = 0; i < krypteratLösenordLength; i += pinkod.Length)
                    {
                        string strSub = krypteratLösenord.Substring(i, pinkod.Length);

                        for (int j = 0; j < strSub.Length; j++)
                        {
                            int pos = pinkod.IndexOf((j + 1).ToString());

                            avkrypteratLösenord += strSub.Substring(pos, 1);
                        }
                    }

                }

                //Stänger anslutningen
                conn.Close();

                //Vilka tecken som ska tas bort från lösenordet när det har avkrypterats
                char[] charsToTrim = { ' ' };

                //Tar bort mellanslagen
                string trimmatAvkrypteratLösenord = avkrypteratLösenord.Trim(charsToTrim);

                //Visar det avkrypterade lösenordet
                txtHämtaLösenord.Text = trimmatAvkrypteratLösenord;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Hämta alla värdena
            string hemsida = txtUppdateraHemsida.Text;
            string lösenord = txtUppdateraLösenord.Text;
            string pinkod = txtUppdateraPinkod.Text;

            //Skapa en string för det krypterade lösenordet
            string krypteratLösenord = "";

            //Hämta längden av lösenordet
            int lösenordLenght = lösenord.Length;

            //Lägg till mellanslag på lösenordet
            for (int i = 0; i < pinkod.Length; i++)
            {
                lösenord += " ";
            }

            for (int i = 0; i < lösenordLenght; i += pinkod.Length)
            {
                string strSub = lösenord.Substring(i, pinkod.Length);

                for (int j = 0; j < strSub.Length; j++)
                {
                    int pos = Convert.ToInt32(pinkod.Substring(j, 1)) - 1;

                    krypteratLösenord += strSub.Substring(pos, 1);
                }
            }
            //Skapa filePath
            string filePath = "test.txt";

            //Konvertera den till en string
            string connectionString = File.ReadAllText(filePath);

            //Skapa en ny anslutning till databasen
            MySqlConnection conn = new MySqlConnection(connectionString);

            //Skapa Query
            string updateQuery = $"UPDATE lösenord SET Lösenord = '{krypteratLösenord}' WHERE Hemsida = '{hemsida}'";

            //Öpnna anslutningen
            conn.Open();

            //Skapa kommandot till databasen
            MySqlCommand cmd = new MySqlCommand(updateQuery, conn);

            //Exekvera kommandot
            cmd.ExecuteReader();

            //Stänga anslutningen till databasen
            conn.Close();

            //Bekräftelse
            MessageBox.Show($"Lösenordet har sparats i databasen. Kom ihåg din pinkod. Pinkoden är {pinkod}");

            //Tömmer alla boxarna efter man har uppdaterat databasen med det nya lösenordet
            txtUppdateraHemsida.Clear();
            txtUppdateraLösenord.Clear();
            txtUppdateraPinkod.Clear();
        }
    }
}
