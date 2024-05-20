using System;
using System.Windows.Forms;

namespace Klient1
{
    public partial class LoggaIn : Form
    {
        public string Anvandarnamn { get; private set; } // Egenskap för att hämta användarnamnet

        public LoggaIn()
        {
            InitializeComponent();
        }

        private void LoggaIn_Load(object sender, EventArgs e)
        {
            
        }

        // Händelsehanterare för klick på logga in-knappen
        private void btnLoggaIn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtAnvandarnamn.Text))
                {
                    Anvandarnamn = txtAnvandarnamn.Text; // Sätt användarnamnet
                    DialogResult = DialogResult.OK; // Ställ in resultatet för inloggningen till OK
                }
                else
                {
                    MessageBox.Show("Vänligen ange ett användarnamn.", "Fel", MessageBoxButtons.OK, MessageBoxIcon.Error); // Visa felmeddelande om användarnamnet är tomt
                }
            }
            catch (Exception ex)
            {
                HanteraException(ex); // Hantera undantag
            }
        }

        // Metod för att hantera undantag
        private void HanteraException(Exception ex)
        {
            MessageBox.Show($"Fel: {ex.Message}", Text);
            // Hantera vidare åtgärder om nödvändigt
        }

        // Händelsehanterare för klick på avbryt-knappen
        private void btnAvbryt_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel; // Ställ in resultatet för inloggningen till Avbryt
        }
    }
}
