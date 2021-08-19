﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Binance.Net;
using Microsoft.Extensions.Configuration;

namespace Binance.Trading.Server
{
    public class Server
    {
        private readonly BinanceClient _client;
        private readonly IConfiguration _configuration;

        public Server(BinanceClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }

        public void StartServer()
        {
            bool canTrade = false;

            _client.SetApiCredentials(_configuration.GetValue<string>("ApiKey:Key"), _configuration.GetValue<string>("ApiKey:Secret"));
            // Wait for succesfull connection
            Console.WriteLine(DateTime.Now + " - - - Attempting connection to Binance server...");
            bool connected = false;
            while (!connected)
            {
                var data = _client.General.GetAccountInfoAsync();
                if (data.Result.Success)
                {
                    canTrade = data.Result.Data.CanTrade;
                    connected = true;
                }
            }
            Console.WriteLine(DateTime.Now + " - - - Connected succesfully.");
            Console.WriteLine(DateTime.Now + $" - - - Account can trade? : {canTrade}");
        }        
    }
}