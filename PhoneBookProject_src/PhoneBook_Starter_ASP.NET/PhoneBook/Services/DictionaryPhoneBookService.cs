using PhoneBook.Exceptions;
using PhoneBook.Model;
using System.Text.RegularExpressions;
using System.IO;
using PhoneBook.Logging;

namespace PhoneBook.Services
{
    public class DictionaryPhoneBookService : IPhoneBookService
    {
        public static string fileName = "myDatabase.txt";
        public static string path = Path.Combine(Environment.CurrentDirectory, fileName);
        public static string AddSuccess = "FAIL";
        public static string DeleteSuccess = "FAIL";



        private readonly Dictionary<string, string> _phoneBookEntries;

        public DictionaryPhoneBookService()
        {
            _phoneBookEntries = new Dictionary<string, string>();


            if (!File.Exists(path))
            {
                // Create the file if it doesn't exist
                using (StreamWriter sw = File.CreateText(path))
                {

                }
            }

            else 
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        // Split the line into key-value pairs
                        string[] parts = line.Split(',');
                        if (parts.Length == 2)
                        {
                            // Add the key-value pair to the dictionary
                            _phoneBookEntries[parts[0]] = parts[1];
                        }
                    }
                }

            }
        }

        public void Add(PhoneBookEntry phoneBookEntry)
        {
            if (phoneBookEntry.Name == null || phoneBookEntry.PhoneNumber == null)
            {
                AddSuccess = "FAIL";
                throw new ArgumentException("Name and phone number must both be specified.");
            }


            if (VerifyPhoneAndName(phoneBookEntry.PhoneNumber, phoneBookEntry.Name))
            {

                _phoneBookEntries.Add(phoneBookEntry.Name, phoneBookEntry.PhoneNumber);
                AddSuccess = "SUCCESS";
                Logger.WriteLog($"UserName Added: {phoneBookEntry.Name}");

                File.WriteAllText(path, "");
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    // Loop through the list and write each item to the file
                    foreach (KeyValuePair<string, string> pair in _phoneBookEntries)
                    {
                        // write the key and value to the file
                        writer.WriteLine(pair.Key + "," + pair.Value);
                    }
                }

            }

            else 
            {
                AddSuccess = "FAIL";
            }
        }

        public void Add(string name, string phoneNumber)
        {
            if (name == null || phoneNumber == null)
            {
                throw new ArgumentException("Name and phone number must both be specified.");
            }
           
            _phoneBookEntries.Add(name, phoneNumber);



        }

        public IEnumerable<PhoneBookEntry> List()
        {
            List<PhoneBookEntry> entriesList = new List<PhoneBookEntry>();

            foreach (var name in _phoneBookEntries.Keys)
            {
                entriesList.Add(new PhoneBookEntry { Name = name, PhoneNumber = _phoneBookEntries[name] });
            }

            return entriesList;
        }

        public void DeleteByName(string name)
        {
            if (!_phoneBookEntries.ContainsKey(name))
            {
                DeleteSuccess = "FAIL";
                throw new NotFoundException($"No phonebook entry found containing name {name}.");
            }

            _phoneBookEntries.Remove(name);
            DeleteSuccess = "SUCCESS";
            Logger.WriteLog($"UserName Deleted: {name}");

            File.WriteAllText(path, "");
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                // Loop through the list and write each item to the file
                foreach (KeyValuePair<string, string> pair in _phoneBookEntries)
                {
                    // write the key and value to the file
                    writer.WriteLine(pair.Key + "," + pair.Value);
                }
            }
        }

        public void DeleteByNumber(string number)
        {
            var name = _phoneBookEntries.Where(kvp => kvp.Value == number).FirstOrDefault().Key;
            if (name == null)
            {
                throw new NotFoundException($"No phonebook entry found containing phone number {number}.");
            }

            _phoneBookEntries.Remove(name);
            Logger.WriteLog($"UserName Deleted: {name}");

            File.WriteAllText(path, "");
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                // Loop through the list and write each item to the file
                foreach (KeyValuePair<string, string> pair in _phoneBookEntries)
                {
                    // write the key and value to the file
                    writer.WriteLine(pair.Key + "," + pair.Value);
                }
            }
        }

       
        public bool VerifyPhoneAndName(string phoneNumber, string userName)
        {
            string regexName = @"^(([A-Za-z]+([ -,.'’]|[ ])+[A-Za-z]+)|([A-Za-z]+)|([A-Za-z]+([ -,.'’]|[ ])+[A-Za-z]+[ -,.'’][A-Za-z]+)|[A-Za-z]+[-,.'’][A-Za-z]+[-,.'’][ ][A-Za-z]+[ ][A-Za-z]+[ ,'.-]|[A-Za-z]+[ ][A-Za-z]+[-,.'’][A-Za-z]+[ ,'.-][A-Za-z]+|[A-Za-z]+[ ][A-Za-z]+[ -.'’][A-Za-z]+|[A-Za-z]+[ ][A-Za-z]+[ ][A-Za-z]+[-.'’][A-Za-z]+)$";

            string regexPhone = @"^(\+?([1-9]|[1-9][0-9]|[1-9][0-9][0-9])[\s.(-])?((\([1-9][0-9][0-9]\))|\(\d{2,3})[\s.)-]\d{3}[\s.-]?\d{4}(\s?(ext|x|ext.)\s?\d{5})?$";
            string regexForeignPhone1 = @"^[0-9]{5}$";
            string regexForeignPhone2 = @"^\d{3}[\s.-]?\d{3}[\s.-]?\d{3}[\s.-]?\d{4}$";
            string regexForeignPhone3 = @"^\d{5}[-. ]\d{5}$";
            string regexForeignPhone4 = @"^(\+?\d{1,3}[\s.(-])?(\d{3}[\s.)-]){2}\d{4}$";
            string regexForeignPhone5 = @"^(\+?([1-9]|[1-9][0-9]|[1-9][0-9][0-9])[\s.(-])?((\([1-9][0-9][0-9]\))|\([1-9][0-9])\)[\s.)-]?\d{3}[\s.-]?\d{4}(\s?(ext|x|ext.)\s?\d{5})?$";
            string regexForeignPhone6 = @"^(?:\+45\s)?(?:\d{2}(?:[\.\s]|\b)){3}\d{2}$";
            string regexForeignPhone7 = @"^\d{3}[ -.]?\d{4}$";
            string regexForeignPhone8 = @"^\d{3}[.\s-]\d{1}[.\s-]\d{3}[.\s-]\d{3}[.\s-]\d{4}$";


            bool isValidPhoneNumber = Regex.IsMatch(phoneNumber, regexPhone);
            bool isValidPhoneNumber1 = Regex.IsMatch(phoneNumber, regexForeignPhone1);
            bool isValidPhoneNumber2 = Regex.IsMatch(phoneNumber, regexForeignPhone2);
            bool isValidPhoneNumber3 = Regex.IsMatch(phoneNumber, regexForeignPhone3);
            bool isValidPhoneNumber4 = Regex.IsMatch(phoneNumber, regexForeignPhone4);
            bool isValidPhoneNumber5 = Regex.IsMatch(phoneNumber, regexForeignPhone5);
            bool isValidPhoneNumber6 = Regex.IsMatch(phoneNumber, regexForeignPhone6);
            bool isValidPhoneNumber7 = Regex.IsMatch(phoneNumber, regexForeignPhone7);
            bool isValidPhoneNumber8 = Regex.IsMatch(phoneNumber, regexForeignPhone8);




            bool isValidName = Regex.IsMatch(userName, regexName);

            Console.WriteLine("String Value: " + phoneNumber);
            Console.WriteLine("Boolean Value: " + isValidPhoneNumber);
            Console.WriteLine("Boolean Value: " + isValidPhoneNumber1);
            Console.WriteLine("Boolean Value: " + isValidPhoneNumber2);
            Console.WriteLine("Boolean Value: " + isValidPhoneNumber3);
            Console.WriteLine("Boolean Value: " + isValidPhoneNumber4);
            Console.WriteLine("Boolean Value: " + isValidPhoneNumber5);
            Console.WriteLine("Boolean Value: " + isValidPhoneNumber6);
            Console.WriteLine("Boolean Value: " + isValidPhoneNumber7);
            Console.WriteLine("Boolean Value: " + isValidPhoneNumber8);


            Console.WriteLine("String Value: " + userName);
            Console.WriteLine("Boolean Value: " + isValidName);


            return (isValidPhoneNumber|| isValidPhoneNumber1  || isValidPhoneNumber2 || isValidPhoneNumber3  || isValidPhoneNumber4 || isValidPhoneNumber5 || isValidPhoneNumber6 || isValidPhoneNumber7 || isValidPhoneNumber8) && isValidName;
        }
    }
}
