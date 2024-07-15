using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalWork.Models
{
    public class Ram
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string MemoryType { get; set; }
        public int MemorySize { get; set; }
        public int ClockSpeed { get; set; }
        public string Status { get; set; }
    }
}
