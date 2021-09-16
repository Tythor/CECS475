using System;
using System.Threading;

namespace Stock
{
    public class Stock
    {
        public event EventHandler<StockNotification> StockEvent;
        public string StockName { get; set; }
        public int InitialValue { get; set; }
        public int CurrentValue { get; set; }
        public int MaxChange { get; set; }
        public int Threshold { get; set; }
        public int NumChanges { get; set; }

        private Thread stockThread;

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

            this.StartThread();
        }

        public void StartThread()
        {
            ThreadStart stockRef = new ThreadStart(Activate);
            stockThread = new Thread(stockRef);
            stockThread.Start();
        }

        public void StopThread()
        {
            stockThread.Abort();
        }

        /// <summary>
        /// Activates the threads synchronizations
        /// </summary>
        public void Activate()
        {
            for (int i = 0; i < 25; i++)
            {
                Thread.Sleep(500); // 1/2 second
                this.ChangeStockValue();
            }
            this.StopThread();
        }

        /// <summary>
        /// Changes the stock value and also raising the event of stock value changes
        /// returns true if the stock goes above or below the threshold
        /// </summary>
        public void ChangeStockValue()
        {
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
                this.InitialValue = this.CurrentValue;
            }
        }
    }
}