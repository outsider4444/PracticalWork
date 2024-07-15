using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalWork.Models
{
    public class Computer
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string Purpose { get; set; }
        public string Description { get; set; }
        public int ProcessorId { get; set; }
        public int GraphicsCardId { get; set; }
        public int PowerSupplyId { get; set; }
        public int HardDriveId { get; set; }
        public int RamId { get; set; }
        public string Mouse { get; set; }
        public string Keyboard { get; set; }
        public string Monitor { get; set; }
    }
}
