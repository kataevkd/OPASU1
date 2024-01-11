
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RESTfull.Domain
{
    public class Author
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Degree { get; set; } = String.Empty;

        public string placeWork { get; set; } = String.Empty;
        public List<Publication> Publications { get; set; } = new List<Publication>();
        public void AddPublication(Publication publication)
        {
            Publications.Add(publication);
        }
        public void RemovePublication(int index)
        {
            Publications.RemoveAt(index);
        }
        public int PublicationCount { get { return Publications.Count; } }

    }
}

