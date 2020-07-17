using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using PlauschiServer.Model;
using Tfres;

namespace PlauschiServer
{
  class Program
  {
    private static object _lock = new object();
    private static Dictionary<string, Event> _events = new Dictionary<string, Event>();

    private const int _maxUsersPerGroup = 15;
    private const int _maxEvents = 1000;

    static void Main(string[] args)
    {
      var server = new Server("*", 8123, ctx => ctx.Response.Send(HttpStatusCode.InternalServerError));
      server.AddEndpoint(HttpVerb.GET, "/new", NewGroupMember);
      server.AddEndpoint(HttpVerb.GET, "/check", CheckGroup);
    }

    private static Task CheckGroup(HttpContext ctx)
    {
      try
      {
        var get = ctx.Request.GetData();
        var eventId = get["event"];
        var groupId = Guid.Parse(get["group"]);

        lock (_lock)
        {
          if (!_events.ContainsKey(eventId))
            return ctx.Response.Send(HttpStatusCode.InternalServerError);

          var myEvent = _events[eventId];
          return myEvent.Guid != groupId ? ctx.Response.Send(HttpStatusCode.Created) : ctx.Response.Send(myEvent);
        }
      }
      catch
      {
        return ctx.Response.Send(HttpStatusCode.InternalServerError);
      }
    }

    private static Task NewGroupMember(HttpContext ctx)
    {
      try
      {
        var get = ctx.Request.GetData();
        var eventId = get["event"];
        var groupSize = int.Parse(get["size"]);
        if (groupSize > _maxUsersPerGroup)
          return ctx.Response.Send(HttpStatusCode.InternalServerError);

        lock (_lock)
        {
          if (!_events.ContainsKey(eventId))
          {
            if (_events.Count > _maxEvents)
              return ctx.Response.Send(HttpStatusCode.InternalServerError);
            _events.Add(eventId, new Event(groupSize));
          }

          var myEvent = _events[eventId];
          myEvent.Count++;

          _events[eventId] = myEvent.Count >= myEvent.Max ? new Event(groupSize) : myEvent;

          return ctx.Response.Send(myEvent);
        }
      }
      catch
      {
        return ctx.Response.Send(HttpStatusCode.InternalServerError);
      }
    }
  }
}
