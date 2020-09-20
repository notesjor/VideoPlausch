using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlauschiServer.Model
{
  public class ServerConfiguration
  {
    public string HostnameOrIp { get; set; } = "*";
    public ushort Port { get; set; } = 8123;
    public uint MaxUsersPerGroup { get; set; } = 15;
    public uint MaxEvents { get; set; } = 1000;
  }
}
