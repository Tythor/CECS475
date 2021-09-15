using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock {
    public class StockBroker
    {
        public string BrokerName { get; set; }
        public List<Stock> stocks = new List<Stock>();
        //public static System.Threading.ReaderWriterLockSlim myLock = new ReaderWriterLockSlim();
        readonly string docPath = @"C:\Users\Documents\CECS 475\Lab3_output.txt";
        public string titles = "Broker".PadRight(10) + "Stock".PadRight(15) +
       "Value".PadRight(10) + "Changes".PadRight(10) + "Date and Time";

        /// <summary>
        /// The stockbroker object
        /// </summary>
        /// <param name="brokerName">The stockbroker's name</param>
        public StockBroker(string brokerName)
        {
            BrokerName = brokerName;
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
            // This method can also possibly handle writing to the file, since its the same information?
            Stock evStock = (Stock)sender;
            Console.WriteLine(this.BrokerName + " " + evStock.StockName + " " + evStock.CurrentValue + " " + evStock.NumChanges);
            StockNotification data = (StockNotification)e;
            String statement = data.StockName + data.CurrentValue + data.NumChanges;
            Console.WriteLine(statement);
        }
    }
}