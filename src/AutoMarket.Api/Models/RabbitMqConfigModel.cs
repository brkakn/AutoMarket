﻿namespace AutoMarket.Api.Models
{
    public class RabbitMqConfigModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Exchange { get; set; }
        public string ExchangeType { get; set; }
        public int Port { get; set; }
    }
}
