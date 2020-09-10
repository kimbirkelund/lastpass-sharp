// Copyright (C) 2013 Dmitry Yakimenko (detunized@gmail.com).
// Licensed under the terms of the MIT license. See LICENCE for details.

using System;
using System.IO;
using System.Linq;
using LastPass;

namespace Example
{
    class Program
    {
        // Very simple text based user interface that demonstrates how to respond to
        // to Vault UI requests.
        private class TextUi: Ui
        {
            public override string ProvideSecondFactorPassword(SecondFactorMethod method)
            {
                return GetAnswer(string.Format("Please enter {0} code", method));
            }

            public override void AskToApproveOutOfBand(OutOfBandMethod method)
            {
                Console.WriteLine("Please approve out-of-band via {0}", method);
            }

            private static string GetAnswer(string prompt)
            {
                Console.WriteLine(prompt);
                Console.Write("> ");
                var input = Console.ReadLine();

                return input == null ? "" : input.Trim();
            }
        }

        static void Main(string[] args)
        {
            // Read LastPass credentials from a file
            // The file should contain 4 lines:
            //   - username
            //   - password
            //   - client ID
            //   - client description
            // See credentials.txt.example for an example.
            var credentials = File.ReadAllLines("../../credentials.txt");
            var username = credentials[0];
            var password = credentials[1];
            var id = credentials[2];
            var description = credentials[3];

            try
            {
                // Fetch and create the vault from LastPass
                var vault = Vault.Open(username,
                    password,
                    new ClientInfo(Platform.Desktop, id, description, false),
                    new TextUi());

                // Dump all the accounts
                foreach (var (entry, index)in vault.Entries.Select((e, i) => (e, i)))
                {
                    switch (entry)
                    {
                        case Account account:
                            Console.WriteLine($@"{index + 1}:
        id: {account.Id}
      name: {account.Name}
  username: {account.Username}
  password: {account.Password}
       url: {account.Url}
     group: {account.Group}");
                            break;

                        case GenericNote genericNote:
                            Console.WriteLine($@"{index + 1}:
        id: {genericNote.Id}
      name: {genericNote.Name}
  contents: {genericNote.Contents}");
                            break;
                    }
                }
            }
            catch (LoginException e)
            {
                Console.WriteLine("Something went wrong: {0}", e);
            }
        }
    }
}
