﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace SoclukProject.Common.Infrastructure;

public static class QueueFactory
{
    public static void SendMessageToExchange(string exchangeName,
                                    string exchangeType,
                                    string queueName,
                                    object obj)
    {
        var channel = CreateBasicConsumer()
            .EnsureExchange(exchangeName, exchangeType)
            .EnsureQueue(exchangeName, queueName)
            .Model;

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(obj));

        channel.BasicPublish(exchange: exchangeName,
            routingKey: queueName,
            basicProperties: null,
            body: body);
    }

    public static EventingBasicConsumer CreateBasicConsumer()
    {
        var factory = new ConnectionFactory() { HostName = SozlukConstants.RabbitMQHost };
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        return new EventingBasicConsumer(channel);
    }
    public static EventingBasicConsumer EnsureExchange(this EventingBasicConsumer consumer,
                                                        string exchangeName,
                                                        string exchangeType = SozlukConstants.DefaultExchangeType)
    {
        consumer.Model.ExchangeDeclare(exchange: exchangeName, type: exchangeType, durable: false, autoDelete: false);
        return consumer;
    }

    public static EventingBasicConsumer EnsureQueue(this EventingBasicConsumer consumer,
                                                        string exchangeName,
                                                        string queueName)
    {
        consumer.Model.QueueDeclare(queue: queueName, exclusive: false, autoDelete: false, arguments: null);

        consumer.Model.QueueBind(queueName, exchangeName, queueName);

        return consumer;
    }
}

