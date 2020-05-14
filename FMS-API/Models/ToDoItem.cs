using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//****************************************************************************
// This is here as an example ... not to go into finished project
//****************************************************************************
namespace FMS_API.Models
{
    public class ToDoItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public string Secret { get; set; }
    }
}
