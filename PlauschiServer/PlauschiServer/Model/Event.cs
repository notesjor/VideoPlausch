using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlauschiServer.Model
{
  public class Event
  {
    public Event(uint max)
    {
      Guid = Guid.NewGuid();
      Max = max;
      Count = 0;
    }

    public Guid Guid { get; set; }
    public uint Max { get; set; }
    public uint Count { get; set; }
  }
}
