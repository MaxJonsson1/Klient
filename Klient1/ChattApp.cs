using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Klient1
{
    public partial class KlientForm : Form
    {
        private TcpClient klient; // TCP-klient för att hantera anslutningar
        private readonly int port = 12345; // Portnummer
        private string anvandarnamn; // Användarnamn

        public KlientForm()
        {
            InitializeComponent();
        }

        // Händelsehanterare när formuläret laddas
        private void KlientForm_Load(object sender, EventArgs e)
        {
            // Visa inloggningsdialogen
            if (!VisaInloggningsDialog())
            {
                Close(); // Stäng formuläret om inloggningsdialogen avbryts
                return;
            }
            btnKoppla.Enabled = true; // Aktivera anslutningsknappen
        }

        // Metod för att visa inloggningsdialogen
        private bool VisaInloggningsDialog()
        {
            using (var loggaIn = new LoggaIn())
            {
                if (loggaIn.ShowDialog() == DialogResult.OK)
                {
                    anvandarnamn = loggaIn.Anvandarnamn; // Sätt användarnamnet
                    return true; // Returnera true om inloggningen lyckas
                }
            }
            return false; // Returnera false om inloggningen avbryts
        }

        // Händelsehanterare för klick på anslutningsknappen
        private async void btnKoppla_Click(object sender, EventArgs e)
        {
            try
            {
                klient = new TcpClient(); // Initialisera TCP-klienten
                await klient.ConnectAsync("127.0.0.1", port); // Anslut till servern asynkront
                btnKoppla.Enabled = false; // Inaktivera anslutningsknappen
                tbxMeddelanden.Enabled = true; // Aktivera meddelandetextfältet
                btnSkicka.Enabled = true; // Aktivera skicka-knappen
                await SkickaInloggningsmeddelande(); // Skicka inloggningsmeddelandet
                _ = Task.Run(() => LyssnaEfterMeddelanden()); // Starta lyssnandet efter meddelanden i en separat uppgift
            }
            catch (SocketException ex)
            {
                HanteraSocketException(ex); // Hantera socket-undantag
            }
            catch (Exception ex)
            {
                HanteraException(ex); // Hantera andra undantag
            }
        }

        // Metod för att skicka inloggningsmeddelandet
        private async Task SkickaInloggningsmeddelande()
        {
            if (klient?.Connected == true)
            {
                try
                {
                    // Koda inloggningsmeddelandet och skicka det till servern
                    byte[] inloggningsMeddelandeBytes = Encoding.Unicode.GetBytes($"INLOGG| {anvandarnamn}");
                    await klient.GetStream().WriteAsync(inloggningsMeddelandeBytes, 0, inloggningsMeddelandeBytes.Length);
                }
                catch (IOException ex)
                {
                    HanteraIOException(ex); // Hantera IO-undantag
                }
                catch (Exception ex)
                {
                    HanteraException(ex); // Hantera andra undantag
                }
            }
            else
            {
                MessageBox.Show("Inte ansluten till servern.", Text); // Visa felmeddelande om inte ansluten till servern
            }
        }

        // Händelsehanterare för klick på skicka-knappen
        private async void btnSkicka_Click(object sender, EventArgs e)
        {
            if (klient == null || !klient.Connected)
            {
                MessageBox.Show("Inte ansluten till servern.", Text); // Visa felmeddelande om inte ansluten till servern
                return;
            }

            string meddelande = tbxMeddelanden.Text; // Hämta meddelandet från textfältet
            tbxMeddelanden.Clear(); // Rensa textfältet

            try
            {
                if (meddelande.StartsWith("/fil"))
                {
                    string filVag = meddelande.Substring(5).Trim(); // Hämta filvägen från meddelandet
                    if (File.Exists(filVag))
                        await SkickaFilAsync(filVag); // Skicka filen om den finns
                    else
                        MessageBox.Show("Fil ej hittad.", Text); // Visa felmeddelande om filen inte hittas
                }
                else
                {
                    // Koda meddelandet och skicka det till servern
                    byte[] meddelandeBytes = Encoding.Unicode.GetBytes($"{anvandarnamn}: {meddelande}");
                    await klient.GetStream().WriteAsync(meddelandeBytes, 0, meddelandeBytes.Length);
                }
            }
            catch (Exception ex)
            {
                HanteraException(ex); // Hantera undantag
            }
        }

        // Metod för att skicka en fil asynkront
     
        private async Task SkickaFilAsync(string filVag)
        {
            try
            {
                using (var filStream = new FileStream(filVag, FileMode.Open, FileAccess.Read))
                {
                    byte[] filBytes = new byte[filStream.Length]; // Läs filen till en byte-array
                    await filStream.ReadAsync(filBytes, 0, filBytes.Length);
                    byte[] headerBytes = Encoding.Unicode.GetBytes($"FILE:{Path.GetFileName(filVag)}"); // Kod filhuvudet
                    await klient.GetStream().WriteAsync(headerBytes, 0, headerBytes.Length); // Skicka filhuvudet
                    await klient.GetStream().WriteAsync(filBytes, 0, filBytes.Length); // Skicka fildata
                }
            }
            catch (Exception ex)
            {
                HanteraException(ex); // Hantera undantag
            }
        }


        // Metod för att lyssna efter meddelanden från servern
        private async Task LyssnaEfterMeddelanden()
        {
            byte[] buffer = new byte[1024]; // Buffer för att ta emot meddelanden
            int bytesRead;
            try
            {
                while ((bytesRead = await klient.GetStream().ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    string meddelande = Encoding.Unicode.GetString(buffer, 0, bytesRead); // Avkoda mottaget meddelande
                    tbxInkorg.Invoke(new Action(() =>
                    {
                        tbxInkorg.AppendText(meddelande + Environment.NewLine); // Visa meddelandet i inkorgsfältet
                    }));
                }
            }
            catch (IOException ex)
            {
                HanteraIOException(ex); // Hantera IO-undantag
            }
            catch (ObjectDisposedException)
            {
                // Hantera avslutad klient
            }
            catch (Exception ex)
            {
                HanteraException(ex); // Hantera andra undantag
            }
        }

        // Metod för att hantera socket-undantag
        private void HanteraSocketException(SocketException ex)
        {
            MessageBox.Show($"Socket Exception: {ex.Message}", Text); // Visa socket-undantagets meddelande
            // Extra åtgärder kan vidtas här vid behov
        }

        // Metod för att hantera IO-undantag
        private void HanteraIOException(IOException ex)
        {
            MessageBox.Show($"IO Exception: {ex.Message}", Text); // Visa IO-undantagets meddelande
            // Extra åtgärder kan vidtas här vid behov
        }

        // Metod för att hantera undantag
        private void HanteraException(Exception ex)
        {
            string felMeddelande = $"Fel: {ex.Message}"; // Konstruera felmeddelandet
            if (ex.InnerException != null)
                felMeddelande += Environment.NewLine + $"Inner Exception: {ex.InnerException.Message}"; // Lägg till inner undantagets meddelande
            MessageBox.Show(felMeddelande, Text); // Visa felmeddelandet
        }

        // Händelsehanterare för när formuläret stängs
        private async void KlientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (klient != null)
            {
                if (klient.Connected)
                {
                    await SkickaUtloggningsmeddelande(); // Skicka utloggningsmeddelandet om fortfarande ansluten
                }
                klient.Dispose(); // Stäng TCP-klienten
            }
        }

        // Metod för att skicka utloggningsmeddelandet
        private async Task SkickaUtloggningsmeddelande()
        {
            if (klient?.Connected == true)
            {
                try
                {
                    byte[] utloggningsMeddelandeBytes = Encoding.Unicode.GetBytes($"UTLOGG| {anvandarnamn}");
                    await klient.GetStream().WriteAsync(utloggningsMeddelandeBytes, 0, utloggningsMeddelandeBytes.Length);
                    klient.Close(); // Stäng klientanslutningen
                }
                catch (IOException ex)
                {
                    HanteraIOException(ex); // Hantera IO-undantag
                }
                catch (Exception ex)
                {
                    HanteraException(ex); // Hantera andra undantag
                }
            }
        }

        // Händelsehanterare för klick på logga ut-knappen
        private async void btnLoggaUt_Click(object sender, EventArgs e)
        {
            await SkickaUtloggningsmeddelande(); // Skicka utloggningsmeddelandet
            Application.Exit(); // Avsluta applikationen
        }
    }
}
