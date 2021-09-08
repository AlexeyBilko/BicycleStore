using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleStore.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();
        public void AddItem(Bicycle bicycle, int quantity)
        {
            CartLine line =
                lineCollection
                .Where(x => x.Bicycle.BicycleId == bicycle.BicycleId)
                .FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Bicycle = bicycle,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public void RemoveLine(Bicycle car)
        {
            lineCollection.RemoveAll(x => x.Bicycle.BicycleId == car.BicycleId);
        }
        public void Clear()
        {
            lineCollection.Clear();
        }
        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(x => x.Bicycle.Price * x.Quantity);
        }

        public IEnumerable<CartLine> Lines
        {
            get => lineCollection;
            set
            {
                lineCollection = (List<CartLine>)value;
            }
        }
    }

    public class CartLine
    {
        public Bicycle Bicycle { get; set; }
        public int Quantity { get; set; }
    }
}
