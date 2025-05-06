using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace Cilink.WebApi.Libraries
{
    public class Helper
    {
        public static List<Dictionary<string, object?>> MultiRowTable(DataTable t)
        {
            var lst = new List<Dictionary<string, object?>>();
            foreach (DataRow row in t.Rows)
            {
                var dict = new Dictionary<string, object?>();
                foreach (DataColumn col in t.Columns)
                {
                    if (row[col] == DBNull.Value)
                        dict[col.ColumnName] = null;
                    else
                        dict[col.ColumnName] = row[col];
                }
                lst.Add(dict);
            }

            return lst;
        }


        public static Dictionary<string, object?> SingleRowTable(DataTable t)
        {
            var dict = new Dictionary<string, object?>();
            foreach (DataRow row in t.Rows)
            {
                foreach (DataColumn col in t.Columns)
                {
                    if (row[col] == DBNull.Value)
                        dict[col.ColumnName] = null;
                    else
                        dict[col.ColumnName] = row[col];
                }
            }
            return dict;
        }

        public static string keyString = "yansingecelerpelinsueceler";

        public static string EncryptString(string text)
        {
            var key = Encoding.UTF8.GetBytes(keyString);

            using (var aesAlg = Aes.Create())
            {
                using (var encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV))
                {
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(text);
                        }

                        var iv = aesAlg.IV;

                        var decryptedContent = msEncrypt.ToArray();

                        var result = new byte[iv.Length + decryptedContent.Length];

                        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                        Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

                        return Convert.ToBase64String(result);
                    }
                }
            }
        }

        public static string DecryptString(string cipherText)
        {
            var fullCipher = Convert.FromBase64String(cipherText);

            var iv = new byte[16];
            var cipher = new byte[16];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, iv.Length);
            var key = Encoding.UTF8.GetBytes(keyString);

            using (var aesAlg = Aes.Create())
            {
                using (var decryptor = aesAlg.CreateDecryptor(key, iv))
                {
                    string result;
                    using (var msDecrypt = new MemoryStream(cipher))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                result = srDecrypt.ReadToEnd();
                            }
                        }
                    }

                    return result;
                }
            }
        }
    }
}
