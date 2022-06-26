using System;
using System.IO;
using System.Collections.Generic;

namespace VSC
{
    class FileEncrypt
    {   


        static string Encrypt(string key, string fileName){
            byte[] byteContent = File.ReadAllBytes(fileName);           // reads bytes
            string base64byte = Convert.ToBase64String(byteContent);    // bytes to base64
            List<char> encryptedContent = new List<char>();             // declares list
            
            int i = 0;
            foreach(char letter in base64byte){
                int asciiLetter = (int)letter + key[i % key.Length];    // adds current key ascii to current letter ascii
                char encryptedLetter = Convert.ToChar(((asciiLetter - 32) % 94) + 32);  // reduces to printable characters
                encryptedContent.Add(encryptedLetter);

                i++;
        }

            string finalString = String.Join("", encryptedContent.ToArray());;
            return finalString;
        }

// ---

        static byte[] Decrypt(string key, string fileName){
                string fileContent = File.ReadAllText(fileName);
                List<char> decryptedContent = new List<char>();
                
                int i = 0;
                foreach(char letter in fileContent){
                    int asciiLetter = (int)letter - key[i % key.Length];    // adds current key ascii to current letter ascii
                    
                    while(asciiLetter < 32){                                // adds to printable characters
                        asciiLetter += 94;
                    }
                    char decryptedLetter = Convert.ToChar(asciiLetter);
                    decryptedContent.Add(decryptedLetter);

                    i++;
                }


                string decryptedBase64String = String.Join("", decryptedContent.ToArray());
                byte[] finalBytes = Convert.FromBase64String(decryptedBase64String);
                return finalBytes;
            }

// ---

        static string getFile(){     // Returns a retry if fails to detect file
            while (true){
                Console.Write("File name: ");
                string fileName = Console.ReadLine();
                if (File.Exists(fileName)){
                    return fileName;
                } else{
                    Console.WriteLine("Failed to detect file \"" + fileName + "\"\n");
                }
            }
        }

// ---

        static string get(string textToUser){     // Simpler input
            Console.Write(textToUser);
            string userInput = Console.ReadLine();
            return userInput;
        }

// ---

        static void overwriteFile(string fileName, string text){        // Simpler overwrite file
            using (StreamWriter file = new StreamWriter(fileName)){  
                file.Write(text);
                file.Close();
            }  
        }

// ---

        static void encryptionSequence(){
            string fileName = getFile();
            string key = get("Key: ");

            Console.WriteLine("\nEncrypting file \"" + fileName + "\" . . .");
            string encryptedString = Encrypt(key, fileName);

            Console.WriteLine("Overwriting . . .\n");
            overwriteFile(fileName, encryptedString);

            get("File " + fileName + " encrypted with key \"" + key + "\"");
        }

// ---

        static void decryptionSequence(){
                string fileName = getFile();
                string key = get("Key: ");

                Console.WriteLine("\nDecrypting file \"" + fileName + "\" . . .");
                byte[] encryptedString = Decrypt(key, fileName);

                Console.WriteLine("Overwriting . . .\n");
                File.WriteAllBytes(fileName, encryptedString);

                get("File " + fileName + " decrypted with key \"" + key + "\"");
            }

// ---

        static void Main(string[] args){        // M M A A I I N N

            bool gotIntention = false;
            while(gotIntention == false){

                string intention = get("Input '1' to encrypt\nInput '2' to decrypt\nInput: ");

                switch(intention){
                    case "1":
                        gotIntention = true;
                        encryptionSequence();
                        break;

                    case "2":
                        gotIntention = true;
                        decryptionSequence();
                        break;

                    default:
                        Console.WriteLine("Unable to interpret input\n");
                        break;
                }     
            }
        
        Environment.Exit(0);

        }
    }
}
