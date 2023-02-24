using Azure.Storage.Files.Shares;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;

namespace StorageCRUD.Repository
{
    public class Queue
    {
        private static string connectionString = "DefaultEndpointsProtocol=https;AccountName=demostoragenew1;AccountKey=ZPs+vGcWeKwN1yYuSWB5qbW/kK4KV7ew+T4d9MmxRRHd9QjSHBpDyXmdMlFO317+/G0iwbi0D+2I+ASt3eZMcw==;EndpointSuffix=core.windows.net";
        public static async Task<bool> CreateQueue(string queueName)
        {
            if (string.IsNullOrEmpty(queueName))
            {
                throw new ArgumentNullException("Enter queue Name"); 
            }
            try
            {
                QueueClient container = new QueueClient(connectionString, queueName);
                await container.CreateIfNotExistsAsync();
                if(container.Exists())
                {
                    Console.WriteLine("Queue Created:" + container.Name);
                    return true;
                }
                else
                {
                    Console.WriteLine("check and try again");
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static async Task InsertMessage(string queueName, string msg)
        {
            if (string.IsNullOrEmpty(msg))
            {
                throw new ArgumentNullException("Enter message");
            }       
                QueueClient container = new QueueClient(connectionString, queueName);
                await container.CreateIfNotExistsAsync();
                if (container.Exists())
                {
                    var data = container.SendMessage(msg);
                    Console.WriteLine("message sent");                  
                }
                else
                {
                Console.WriteLine("message no sent");
                }          
        }
        public static async Task<PeekedMessage[]> peekMessage(string queueName)
        {
            QueueClient container = new QueueClient(connectionString, queueName);
            PeekedMessage[] msg = null;
            if (container.Exists())
            {
                msg = container.PeekMessages(2);
            }
            return msg;
        }
        public static async Task UpdateMessage(string queueName,string data)
        {
            QueueClient container = new QueueClient(connectionString,queueName);
            if (container.Exists())
            {
                QueueMessage[] msg = container.ReceiveMessages();
                container.UpdateMessage(msg[0].MessageId, msg[0].PopReceipt, data, TimeSpan.FromSeconds(10));
            }         
        }
        public static async Task DequeueMessage(string queueName)
        {
            QueueClient container = new QueueClient(connectionString, queueName);
            if (container.Exists())
            {
                QueueMessage[] msg = container.ReceiveMessages();
                System.Console.WriteLine("Dequeue message" + msg[0].Body);
                container.DeleteMessage(msg[0].MessageId, msg[0].PopReceipt);
            }
        }
        public static async Task DeleteQueue(string queueName)
        {
            QueueClient container = new QueueClient(connectionString, queueName);
            if (container.Exists())
            {
                await container.DeleteAsync();
            }
        }
    }
}
