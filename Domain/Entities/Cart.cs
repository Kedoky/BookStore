using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public IEnumerable<CartLine> Lines { get { return lineCollection; } }

        //Добавление товара в корзину
        public void AddItem(Book book, int quantity)
        {
            CartLine line = lineCollection
                .Where(b => b.Book.BookId == book.BookId)
                .FirstOrDefault();
            if(line == null)
            {
                lineCollection.Add(new CartLine { Book = book, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        //Удаление товара из корзины
        public void RemoveLine(Book book)
        {
            lineCollection.RemoveAll(l => l.Book.BookId == book.BookId);
        }

        //Вычисление итоговой стоимости товаров
        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Book.Price * e.Quantity);
        }

        //Очитска корзины
        public void Clear()
        {
            lineCollection.Clear();
        }
    }

    public class CartLine
    {
        public Book Book { get; set; }
        public int Quantity { get; set; }
    }
}
