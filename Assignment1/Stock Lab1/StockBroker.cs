using System;
using System.Collections.Generic;
using System.IO;

namespace Stock
{
    public class StockBroker
    {
        public string BrokerName { get; set; }
        public List<Stock> stocks = new List<Stock>();
        readonly string docPath = Directory.GetCurrentDirectory();
        public static string titles = "Broker".PadRight(10) + "Stock".PadRight(15) + "Value".PadRight(10) + "Changes".PadRight(10);

        /// <summary>
        /// The stockbroker object
        /// </summary>
        /// <param name="brokerName">The stockbroker's name</param>
        public StockBroker(string brokerName)
        {
            this.BrokerName = brokerName;
            docPath += "\\" + BrokerName + ".txt";
            File.WriteAllText(docPath, titles + "\n");
        }

        /// <summary>
        /// Adds stock objects to the stock list
        /// </summary>
        /// <param name="stock">Stock object</param>
        public void AddStock(Stock stock)
        {
            stocks.Add(stock);
            stock.StockEvent += new EventHandler<StockNotification>(EventHandler);
        }

        /// <summary>
        /// The eventhandler that raises the event of a change
        /// </summary>
        /// <param name="sender">The sender that indicated a change</param>
        /// <param name="e">Event arguments</param>
        void EventHandler(Object sender, EventArgs e)
        {
            StockNotification data = (StockNotification)e;
            String statement = this.BrokerName.PadRight(10) + data.StockName.PadRight(15) +
                data.CurrentValue.ToString().PadRight(10) + data.NumChanges.ToString().PadRight(10);
            Console.WriteLine(statement);
            LogEvent(statement);
        }

        void LogEvent(string line)
        {
            StreamWriter sw = File.AppendText(docPath);
            sw.WriteLine(line);
            sw.Close();
        }
    }
}