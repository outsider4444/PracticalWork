using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalWork.Models
{
    public class HardDrive
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Capacity { get; set; }
        public string Status { get; set; }
    }
}
