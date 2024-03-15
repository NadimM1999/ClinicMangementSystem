using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    [Serializable]
    public record DoctorDto 
    {
      public Guid Id {  get; set; }
      public string? Name { get; set; }
      public bool Available {  get; set; }
      public int WorkHours {  get; set; }
      public int Appointment {  get; set; }
    } 

}
