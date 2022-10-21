using System;
using System.Collections.Generic;
using System.Linq;
using Web.Models;

namespace Web
{
    public class RepositoryTest
    {
        public readonly List<Req> items;
        private Random r = new Random();
        public RepositoryTest()
        {
            this.items = new List<Req>();
            string[] statuses = new string[] { "Получена", "В работе", "Выполнена", "Отклонена", "Отменена" };
            for (int i = 1; i < 100; i++)
            {
                this.items.Add(new Req() { Id = i, Email = $"test{i}@gmail.com", Name = $"Тестовая заявка{i}", Text = $"Тестовая заявка{i}", Time = new DateTime(2022, r.Next(1,11), r.Next(1, 11)), Status = statuses[r.Next(statuses.Length)] });
            }
        }

        public IEnumerable<Req> All()
        {
            return this.items.OrderBy(i => i.Time);
        }
    }
}
