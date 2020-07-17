using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlauschiServer.Model
{
  public class Event
  {
    public Event(int max)
    {
      Guid = Guid.NewGuid();
      Max = max;
      Count = 0;
    }

    public Guid Guid { get; set; }
    public int Max { get; set; }
    public int Count { get; set; }
  }
}
