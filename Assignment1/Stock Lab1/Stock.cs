using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Stock { 
    public class Stock
    {
        public event EventHandler<StockNotification> StockEvent;
        public string StockName { get; set; }
        public int InitialValue { get; set; }
        public int CurrentValue { get; set; }
        public int MaxChange { get; set; }
        public int Threshold { get; set; }
        public int NumChanges { get; set; }

        /// <summary>
        /// Stock class that contains all the information and changes of the stock
        /// </summary>
        /// <param name="name">Stock name</param>
        /// <param name="startingValue">Starting stock value</param>
        /// <param name="maxChange">The max value change of the stock</param>
        /// <param name="threshold">The range for the stock</param>
        public Stock(string name, int startingValue, int maxChange, int threshold)
        {
            this.StockName = name;
            this.InitialValue = startingValue;
            this.CurrentValue = startingValue;
            this.MaxChange = maxChange;
            this.Threshold = threshold;

            this.CreateThread();
        }
        
        public void CreateThread()
        {
            Console.WriteLine("Starting Thread");
            ThreadStart stockRef = new ThreadStart(this.Activate);
            Thread stockThread = new Thread(stockRef);
            stockThread.Start();
            Thread.Sleep(25000);
            stockThread.Abort();
        }

        /// <summary>
        /// Activates the threads synchronizations
        /// </summary>
        public void Activate()
        {
            Console.WriteLine("Activating Thread");
            for (int i = 0; i < 25; i++)
            {
                Thread.Sleep(500); // 1/2 second
                this.ChangeStockValue();
                i++;
            }
        }
        
        /// <summary>
        /// Changes the stock value and also raising the event of stock value changes\
        /// returns true if the stock goes above or below the threshold
        /// </summary>
        public void ChangeStockValue()
        {
            Console.WriteLine("Changing stock value");
            var rand = new Random();
            this.CurrentValue += rand.Next(-this.MaxChange, this.MaxChange);
            this.NumChanges++;
            if (Math.Abs(this.CurrentValue - this.InitialValue) > this.Threshold)
            {
                StockNotification a = new StockNotification(this.StockName, this.CurrentValue, this.NumChanges);
                if (StockEvent != null)
                {
                    StockEvent(this, a);
                }
                Console.WriteLine(this.StockName + " " + this.CurrentValue + " " + this.NumChanges);
                this.InitialValue = this.CurrentValue;
            }
        }
    }
}