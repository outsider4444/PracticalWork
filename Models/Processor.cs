using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalWork.Models
{
    public class Processor
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int NumberOfCores { get; set; }
        public string MemoryType { get; set; }
        public int Frequency { get; set; }
        public string Status { get; set; }
    }
}
