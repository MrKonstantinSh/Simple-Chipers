﻿using System;
using System.Windows.Forms;
using Simple_ciphers.Model.Ciphers;
using Simple_ciphers.Model.Validation;
using Simple_ciphers.Controller;
using System.IO;

namespace Simple_ciphers
{
    public partial class fMain : Form
    {
        public fMain()
        {
            InitializeComponent();
        }

        private void BtnEncrypt_Click(object sender, EventArgs e)
        {
            TypesOfCiphers typesOfCipher = TypesOfCiphers.RailFence;
            if (rbRailFence.Checked)
            {
                typesOfCipher = TypesOfCiphers.RailFence;
            }
            else if (rbRotatingSquare.Checked)
            {
                typesOfCipher = TypesOfCiphers.RotatingSquare;
            }
            else if (rbVigenerCipher.Checked)
            {
                typesOfCipher = TypesOfCiphers.Vigenere;
            }

            tbKey.Text = KeyValidation.ModifyKey(tbKey.Text, typesOfCipher);
            rtbSrcText.Text = TextValidation.ModifyText(rtbSrcText.Text, typesOfCipher);

            if (cbUseDataInRcb.Checked)
            {
                rtbResText.Text = MainController.Encrypt(rtbSrcText.Text, tbKey.Text, typesOfCipher);
            }
            else
            {
                try
                {
                    MainController.Encrypt(tbPathToSrcFile.Text, tbPathToResFile.Text,
                    tbKey.Text, typesOfCipher);
                }
                catch (ArgumentException)
                {
                    MessageBox.Show("Incorrect file path entered!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (IOException)
                {
                    MessageBox.Show("Incorrect file path entered!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } 
        }

        private void BtnDecrypt_Click(object sender, EventArgs e)
        {
            TypesOfCiphers typesOfCipher = TypesOfCiphers.RailFence;
            if (rbRailFence.Checked)
            {
                typesOfCipher = TypesOfCiphers.RailFence;
            }
            else if (rbRotatingSquare.Checked)
            {
                typesOfCipher = TypesOfCiphers.RotatingSquare;
            }
            else if (rbVigenerCipher.Checked)
            {
                typesOfCipher = TypesOfCiphers.Vigenere;
            }

            tbKey.Text = KeyValidation.ModifyKey(tbKey.Text, typesOfCipher).ToString();
            rtbSrcText.Text = TextValidation.ModifyText(rtbSrcText.Text, typesOfCipher);

            if (cbUseDataInRcb.Checked)
            {
                rtbResText.Text = MainController.Decrypt(rtbSrcText.Text, tbKey.Text,
                    typesOfCipher);
            }
            else
            {
                try
                {
                    MainController.Decrypt(tbPathToSrcFile.Text, tbPathToResFile.Text,
                    tbKey.Text, typesOfCipher);
                }
                catch (ArgumentException)
                {
                    MessageBox.Show("Incorrect file path entered!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (IOException)
                {
                    MessageBox.Show("Incorrect file path entered!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnChoosePathToSrcFile_Click(object sender, EventArgs e)
        {
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            openFileDialog.Filter = "Text files (*.txt)|*.txt";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                tbPathToSrcFile.Text = openFileDialog.FileName;
            }
        }

        private void BtnChoosePathToResFile_Click(object sender, EventArgs e)
        {
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            openFileDialog.Filter = "Text files (*.txt)|*.txt";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                tbPathToResFile.Text = openFileDialog.FileName;
            }
        }

        private void RbRotatingSquare_CheckedChanged(object sender, EventArgs e)
        {
            tbKey.ReadOnly = true;
            tbKey.Text = null;
        }

        private void RbRailFence_CheckedChanged(object sender, EventArgs e)
        {
            tbKey.ReadOnly = false;
        }

        private void RbVigenerCipher_CheckedChanged(object sender, EventArgs e)
        {
            tbKey.ReadOnly = false;
        }
    }
}