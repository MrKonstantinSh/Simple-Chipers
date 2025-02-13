﻿using System;
using System.IO;
using Simple_ciphers.Model.Ciphers;
using Simple_ciphers.Model.Validation;

namespace Simple_ciphers.Controller
{
    /// <summary>
    /// The class provides a convenient set of methods for working with encryption / decryption algorithms.
    /// </summary>
    public static class MainController
    {
        /// <summary>
        /// Performs one of the possible actions: encrypts or decrypts data.
        /// </summary>
        /// <param name="text">Data to be encrypted / decrypted.</param>
        /// <param name="key">The key for the encryption / decryption algorithm.</param>
        /// <param name="typeOfChiper">The name of the encryption / decryption algorithm.</param>
        /// <param name="action">Action to be taken (encrypt / decrypt).</param>
        /// <returns>The result of encryption / decryption.</returns>
        private static string PerformAction(string text, string key, 
            TypesOfCiphers typeOfChiper, Model.Ciphers.Action action)
        {
            switch (typeOfChiper)
            {
                case TypesOfCiphers.RailFence:
                    RailFence railFence = new RailFence();
                    if (action == Model.Ciphers.Action.Encrypt)
                    {
                        return railFence.Encrypt(text, key);
                    }
                    else
                    {
                        return railFence.Decrypt(text, key);
                    }
                case TypesOfCiphers.RotatingSquare:
                    RotatingGrill rotatingSquare = new RotatingGrill();
                    if (action == Model.Ciphers.Action.Encrypt)
                    {
                        return rotatingSquare.Encrypt(text);
                    }
                    else
                    {
                        return rotatingSquare.Decrypt(text);
                    }
                case TypesOfCiphers.Vigenere:
                    Vigenere vigener = new Vigenere();
                    if (action == Model.Ciphers.Action.Encrypt)
                    {
                        try
                        {
                            return vigener.Encrypt(text, key);
                        }
                        catch (DivideByZeroException)
                        {
                            return text;
                        }
                    }
                    else
                    {
                        try
                        {
                            return vigener.Decrypt(text, key);
                        }
                        catch (DivideByZeroException)
                        {
                            return text;
                        }
                    }
                default:
                    return null;
            }
        }

        /// <summary>
        /// Encrypts data from a file using the selected algorithm and writes the result to another file.
        /// </summary>
        /// <param name="pathToSrcFile">The path to the file with the text to be encrypted.</param>
        /// <param name="pathToDestFile">The path to the file where you want to save the encryption result.</param>
        /// <param name="key">The key for the encryption algorithm.</param>
        /// <param name="typeOfChiper">The name of the encryption algorithm.</param>
        public static void Encrypt(string pathToSrcFile, string pathToDestFile,
            string key, TypesOfCiphers typeOfChiper)
        {
            string plaintext;
            try
            {
                plaintext = File.ReadAllText(pathToSrcFile);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException();
            }
            catch (IOException)
            {
                throw new IOException();
            }

            plaintext = TextValidation.ModifyText(plaintext, typeOfChiper);

            string ciphertext = PerformAction(plaintext, key, typeOfChiper, Model.Ciphers.Action.Encrypt);

            File.WriteAllText(pathToDestFile, ciphertext);
        }

        /// <summary>
        /// Encrypts the text with the selected algorithm.
        /// </summary>
        /// <param name="plaintext">The text to be encrypted.</param>
        /// <param name="key">The key for the encryption algorithm.</param>
        /// <param name="typeOfChiper">The name of the encryption algorithm.</param>
        /// <returns>The cipher text.</returns>
        public static string Encrypt(string plaintext, string key, Model.Ciphers.TypesOfCiphers typeOfChiper)
        {
            return PerformAction(plaintext, key, typeOfChiper, Model.Ciphers.Action.Encrypt);
        }

        /// <summary>
        /// Decrypts data from a file using the selected algorithm and writes the result to another file.
        /// </summary>
        /// <param name="pathToSrcFile">The path to the file with the text to be decrypted.</param>
        /// <param name="pathToDestFile">The path to the file where you want to save the decryption result.</param>
        /// <param name="key">The key for the decryption algorithm.</param>
        /// <param name="typeOfChiper">The name of the decryption algorithm.</param>
        public static void Decrypt(string pathToSrcFile, string pathToDestFile,
            string key, TypesOfCiphers typeOfChiper)
        {
            string ciphertext;
            try
            {
                ciphertext = File.ReadAllText(pathToSrcFile);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException();
            }
            catch (IOException)
            {
                throw new IOException();
            }

            ciphertext = TextValidation.ModifyText(ciphertext, typeOfChiper);

            string plaintext = PerformAction(ciphertext, key, typeOfChiper, Model.Ciphers.Action.Decrypt);
            File.WriteAllText(pathToDestFile, plaintext);
        }

        /// <summary>
        /// Decrypts the text with the selected algorithm.
        /// </summary>
        /// <param name="ciphertext">The text to decrypt.</param>
        /// <param name="key">The key for the decryption algorithm.</param>
        /// <param name="typeOfChiper">The name of the decryption algorithm.</param>
        /// <returns>Decrypted text.</returns>
        public static string Decrypt(string ciphertext, string key, TypesOfCiphers typeOfChiper)
        {
            return PerformAction(ciphertext, key, typeOfChiper, Model.Ciphers.Action.Decrypt);
        }
    }
}