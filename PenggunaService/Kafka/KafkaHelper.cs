using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PenggunaService.Data;
using PenggunaService.Models;

namespace PenggunaService.Kafka
{
    public class KafkaHelper
    {
        public static async Task<bool> SendMessage(KafkaSettings settings, string topic, string key, string val)
        {
            var succeed = false;
            var config = new ProducerConfig
            {
                BootstrapServers = settings.Server,
                ClientId = Dns.GetHostName(),

            };
            using (var adminClient = new AdminClientBuilder(config).Build())
            {
                try
                {
                    await adminClient.CreateTopicsAsync(new List<TopicSpecification> {
                        new TopicSpecification {
                            Name = topic,
                            NumPartitions = settings.NumPartitions,
                            ReplicationFactor = settings.ReplicationFactor } });
                }
                catch (CreateTopicsException e)
                {
                    if (e.Results[0].Error.Code != ErrorCode.TopicAlreadyExists)
                    {
                        Console.WriteLine($"An error occured creating topic {topic}: {e.Results[0].Error.Reason}");
                    }
                    else
                    {
                        Console.WriteLine("Topic already exists");
                    }
                }
            }
            using (var producer = new ProducerBuilder<string, string>(config).Build())
            {
                producer.Produce(topic, new Message<string, string>
                {
                    Key = key,
                    Value = val
                }, (deliveryReport) =>
                {
                    if (deliveryReport.Error.Code != ErrorCode.NoError)
                    {
                        Console.WriteLine($"Failed to deliver message: {deliveryReport.Error.Reason}");
                    }
                    else
                    {
                        Console.WriteLine($"Produced message to: {deliveryReport.TopicPartitionOffset}");
                        succeed = true;
                    }
                });
                producer.Flush(TimeSpan.FromSeconds(10));
            }

            return await Task.FromResult(succeed);
        }

        public static async Task<int> CancelOrder(KafkaSettings settings, PenggunaDbContext db)
        {
            var succeed = 0;
            var Serverconfig = new ConsumerConfig
            {
                BootstrapServers = settings.Server,
                GroupId = "Order",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            CancellationTokenSource cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true; // prevent the process from terminating.
                cts.Cancel();
            };
            Console.WriteLine("=================Cancelling Your Order=================");
            using (var consumer = new ConsumerBuilder<string, string>(Serverconfig).Build())
            {
                var topics = new string[] { "Order" };
                consumer.Subscribe(topics);
                try
                {
                    var cr = consumer.Consume(cts.Token);
                    Console.WriteLine($"Consumed record with Topic: {cr.Topic} key: {cr.Message.Key} and value: {cr.Message.Value}");

                        if (cr.Topic == "Order")
                        {
                            Order order = JsonConvert.DeserializeObject<Order>(cr.Message.Value);
                            order.Status = "Cancelled";
                            db.Orders.Add(order);
                        }
                        var cancel = await db.SaveChangesAsync();
                        Console.WriteLine("--> Data was saved into database");
                        Console.WriteLine("--> Your order was cancelled");
                        // if (cancel > 0)
                        // {
                        //     var penggunaId = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                        //     var currentPengguna = db.Penggunas.Where(o => o.PenggunaId == penggunaId).FirstOrDefault();
                        //     var oldSaldo = db.Saldos.Where(o => o.PenggunaId == currentPengguna.PenggunaId).OrderBy(o => o.SaldoId).LastOrDefault();
                        //     if (oldSaldo != null)
                        //     {
                        //         var newSaldo = new Saldo()
                        //         {
                        //             PenggunaId = currentPengguna.PenggunaId,
                        //             TotalSaldo = oldSaldo.TotalSaldo - (float)oldSaldo.MutasiSaldo,
                        //             MutasiSaldo = -oldSaldo.MutasiSaldo,
                        //             Created = DateTime.Now
                        //         };
                        //         db.Saldos.Add(newSaldo);
                        //         await db.SaveChangesAsync();
                        //     }
                        // }
                    

                }
                catch (OperationCanceledException)
                {
                    // Ctrl-C was pressed.
                }
                finally
                {
                    consumer.Close();
                    succeed = 1;
                }
            }
            return await Task.FromResult(succeed);
        }
    }
}