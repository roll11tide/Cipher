using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Cipher
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Cipher Console --- JLW Technologies\r\n");

            readCommand();
            Console.WriteLine();
        }

        public static void readCommand()
        {
            Console.WriteLine("Awaiting command...");
            string command = Console.ReadLine();

            switch (command)
            {
                case "encrypt -m":
                    Console.WriteLine("Enter three digit key: ");
                    string key = Console.ReadLine();
                    int n;
                    while (!validateKey(key))
                    {
                        Console.WriteLine("Invalid key, re-enter: ");
                        key = Console.ReadLine();
                    }
                    int intKey = Convert.ToInt32(key);
                    char[] keyStringArray = key.ToCharArray();
                    Console.WriteLine("Enter text to encrypt: ");
                    string decryptedText = Console.ReadLine();
                    char[] decryptedArray = decryptedText.ToCharArray();
                    char[] encryptedArray = { };
                    int indexer = 0;
                    foreach (char c in decryptedArray)
                    {
                        if (indexer <= decryptedArray.Length)
                        {
                            int outputID = 0;
                            Console.WriteLine("The character is " + c.ToString());
                            Console.WriteLine("The key is " + intKey.ToString());
                            switch (intKey % 2)
                            {
                                case 0:
                                    if (intKey % 5 == 0)
                                    {
                                        if (c > Convert.ToInt32(keyStringArray[1]))
                                        {
                                            outputID = c - Convert.ToInt32(keyStringArray[1]);
                                        } else
                                        {
                                            outputID = (c + 3) - Convert.ToInt32(keyStringArray[1]);
                                        }
                                    } else
                                    {
                                        outputID = (c + 200) - Convert.ToInt32(keyStringArray[1]);
                                    }
                                    break;
                                case 1:
                                    outputID = c + Convert.ToInt32(keyStringArray[2]);
                                    break;
                                default:
                                    Console.WriteLine("Modulo conversion technique returns out of range integer.");
                                    break;
                            }
                            decryptedArray[indexer] = (char)outputID;
                            indexer++;
                        }
                    }

                    foreach (char c in decryptedArray)
                    {
                        Console.WriteLine(c.ToString());
                    }

                    Console.WriteLine(decryptedArray.ToString());
                    // System.Windows.Forms.Clipboard.SetText(decryptedArray.ToString());
                    break;
                case "encrypt -a":
                    var path = Application.StartupPath;
                    try
                    {
                        string cipherTxt = Path.Combine(Application.StartupPath, "cipherKey.txt");

                        if (File.Exists(cipherTxt))
                        {
                            StreamReader reader = new StreamReader(cipherTxt);
                            string cipherKey = reader.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("No cipher key has been written - operation broken.");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("The process failed {0}: " + e.ToString());
                    }
                    break;
                case "decrypt -m":
                    Console.WriteLine("Enter three digit key: ");
                    key = Console.ReadLine();
                    intKey = Convert.ToInt32(key);
                    int shiftedID = 0;
                    while (!validateKey(key))
                    {
                        Console.WriteLine("Invalid key, re-enter: ");
                        key = Console.ReadLine();
                    }
                    intKey = Convert.ToInt32(key);
                    keyStringArray = key.ToCharArray();
                    Console.WriteLine("Enter text to decrypt: ");
                    string encryptedText = Console.ReadLine();
                    encryptedArray = encryptedText.ToCharArray();
                    char[] decryptionArray = { };
                    indexer = 0;
                    foreach (char c in encryptedArray)
                    {
                        switch (intKey % 2)
                        {
                            case 0:
                                if (intKey % 5 == 0)
                                {
                                    if (c > Convert.ToInt32(keyStringArray[1]))
                                    {
                                        shiftedID = c + Convert.ToInt32(keyStringArray[1]);
                                    }
                                    else
                                    {
                                        shiftedID = (c - 3) + Convert.ToInt32(keyStringArray[1]);
                                    }
                                }
                                else
                                {
                                    shiftedID = (c - 200) + Convert.ToInt32(keyStringArray[1]);
                                }
                                break;
                            case 1:
                                shiftedID = c + Convert.ToInt32(keyStringArray[2]);
                                break;
                            default:
                                Console.WriteLine("Modulo conversion technique returns out of range integer.");
                                break;
                        }
                    }
                    break;
                case "writekey":
                    try
                    {
                        string cipherTxt = Path.Combine(Application.StartupPath, "cipherKey.txt");

                        if(!File.Exists(cipherTxt))
                        {
                            File.Create(cipherTxt).Close();
                        }

                        if (File.Exists(cipherTxt))
                        {
                            Console.WriteLine("Enter the cipher key to be securely saved: ");
                            string cipherKey = Console.ReadLine();
                            using (StreamWriter writer = new StreamWriter(cipherTxt))
                            {
                                writer.WriteLine(cipherKey);
                            }
                            // File.Encrypt(cipherTxt);
                        }
                        else
                        {
                            Console.WriteLine("No cipher key has been written - operation broken.");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("The process failed {0}: " + e.ToString());
                    }
                    break;
                case "exit":
                    Application.Exit();
                    break;
                default:
                    Console.WriteLine("Command unrecognized.");
                    readCommand();
                    break;
            }
        }

        public static bool validateKey(string key)
        {
            int n;
            if (key == String.Empty || !int.TryParse(key, out n))
            {
                return false;
            } else
            {
                if (key.Length == 3)
                {
                    return true;
                } else
                {
                    return false;
                }
            }
        }
    }
}