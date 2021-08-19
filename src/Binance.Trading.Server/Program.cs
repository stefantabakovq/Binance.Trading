using System;
using System.IO;
using System.Collections.Generic;
using Binance.Net;
using Binance.Net.Objects;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace Binance.Trading.Server
{
    public class Program : IDisposable
    {
        private readonly string _filename = "config.json";
        private IConfiguration configuration { get; set; }

        public BinanceClient client { get; set; }
        public BinanceClientOptions options { get; set; }

        public string this[string key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Program()
        {
            Console.WriteLine(DateTime.Now + " - - - Starting trading server....");
            configuration = new ConfigurationBuilder()
                .AddJsonFile(_filename, optional: false)
                .Build();

            options = new BinanceClientOptions();
            client = new BinanceClient(options);       
            Server server = new Server(client, configuration);
            server.StartServer();
        }

        static void Main(string[] args)
        {
            Program _startProgram = new Program();
        }

        public void Dispose()
        {
            if (client == null) return;
            client.Dispose();
        }
    }
}
