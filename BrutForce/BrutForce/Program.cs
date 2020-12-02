using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace BrutForce
{

    class Program
    {
        
        static void Main(string[] args)
        {
            string hashCode = null;
            Thread myThread1 = new Thread(new ParameterizedThreadStart(Brut));
            myThread1.Start(hashCode = ("1115dd800feaacefdf481f1f9070374a2a81e27880f187396db67958b207cbad"));
            Thread myThread2 = new Thread(new ParameterizedThreadStart(Brut));
            myThread2.Start(hashCode = ("3a7bd3e2360a3d29eea436fcfb7e44c735d117c42d1c1835420b6b9942dd4f1b"));
            Thread myThread3 = new Thread(new ParameterizedThreadStart(Brut));
            myThread3.Start(hashCode = ("74e1bb62f8dabb8125a58852b63bdf6eaef667cb56ac7f7cdba6d7305c50a22f"));
        }
        public static void Brut(object hashCode)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            string hashCode1 = (string)hashCode;
            string alphabet = "abcdefghijklmnopqrstuvwxyz";
            char[] arralphabet;
            char[] password1 = new char[5];
            for (int i = 0; i < 5; i++) { password1[i] = ' '; }

            arralphabet = alphabet.ToCharArray(0, 26);
            for (int i = 0; i < alphabet.Length; i++)
            {
                for (int j = 0; j < alphabet.Length; j++)
                {
                    for (int k = 0; k < alphabet.Length; k++)
                    {
                        for (int l = 0; l < alphabet.Length; l++)
                        {
                            for (int m = 0; m < alphabet.Length; m++)
                            {
                                password1[0] = arralphabet[i];
                                password1[1] = arralphabet[j];
                                password1[2] = arralphabet[k];
                                password1[3] = arralphabet[l];
                                password1[4] = arralphabet[m];
                                string PasswordStr = new string(password1);
                                using (SHA256 sha256hash = SHA256.Create())
                                {
                                    string hash = GetHash(sha256hash, PasswordStr);
                                    if (hash == hashCode1)
                                    {
                                        Console.WriteLine(PasswordStr + " пароль найден"); Console.WriteLine("хэш данного пароля " + hash);
                                        stopwatch.Stop();
                                        Console.WriteLine("Время поиска пароля: " + stopwatch.ElapsedMilliseconds+"мс");
                                    }
                                    
                                }
                            }
                        }
                    }
                }
            }Thread.Sleep(500);
        }
        private static string GetHash(HashAlgorithm hashAlgorithm, string input)
        {


            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));


            var sBuilder = new StringBuilder();


            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
