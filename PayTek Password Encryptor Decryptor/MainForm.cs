using System;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace PayTek_Password_Encryptor_Decryptor
{
    public partial class MainForm : Form
    {
        #region Form loading
        public MainForm()
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(Shortcuts);
            this.KeyPreview = true;
        }

        private void Form1_Load(object sender, KeyEventArgs e)
        {
        }
        #endregion

        #region buttons
        private void start_Click(object sender, EventArgs e)
        {
            if(inputTextBox.Text != "")
            {
                if (decrypt.Checked)
                {
                    resultTextBox.Text = Decrypt(inputTextBox.Text);
                }
                else
                {
                    resultTextBox.Text = Encrypt(inputTextBox.Text);
                }
            }
            else
            {
                MessageBox.Show("Please input text to perform this action");
            }

        }
        #endregion

        #region functions
        string Encrypt(string plaintext, int shift = 3)
        {
            StringBuilder asciiValues = new StringBuilder();

            foreach (char c in plaintext)
            {
                char encryptedChar;

                if (char.IsUpper(c))
                {
                    encryptedChar = (char)((((c + shift) - 'A') % 26) + 'A');
                }
                else if (char.IsLower(c))
                {
                    encryptedChar = (char)((((c + shift) - 'a') % 26) + 'a');
                }
                else
                {
                    encryptedChar = c;
                }

                asciiValues.Append((int)encryptedChar + " ");
            }

            return asciiValues.ToString().Trim();
        }

        string Decrypt(string asciiValues, int shift = 3)
        {
            StringBuilder decryptedText = new StringBuilder();
            string[] asciiArray = asciiValues.Split(' ');

            foreach (string asciiValue in asciiArray)
            {
                if (int.TryParse(asciiValue, out int value))
                {
                    char decryptedChar;

                    if (value >= 'A' && value <= 'Z')
                    {
                        decryptedChar = (char)((((value - 'A') - shift + 26) % 26) + 'A');
                    }
                    else if (value >= 'a' && value <= 'z')
                    {
                        decryptedChar = (char)((((value - 'a') - shift + 26) % 26) + 'a');
                    }
                    else
                    {
                        decryptedChar = (char)value; // Non-alphabetic characters remain unchanged
                    }

                    decryptedText.Append(decryptedChar);
                }
            }

            return decryptedText.ToString(); // Return the decrypted string
        }

        void Shortcuts(object sender , KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.E) //exit
            {
                this.Close();
                return;
            }
            if (e.Control && e.KeyCode == Keys.M) //minimize
            {
                this.WindowState = FormWindowState.Minimized;
                return;
            }
            if(e.KeyCode == Keys.Enter)
            {
                start_Click(sender , e);
                return;
            }
        }
        #endregion

    }
}