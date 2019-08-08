using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CreatorAPI.Models;

namespace CreatorAPI.Models
{
    public class LogManager
    {
        CreatorEntities db;
        ConnectionsLog log;

        public LogManager()
        {
            db = new CreatorEntities();
        }

        public void Add(string UUID, string Identifier, string Description)
        {
            log = new ConnectionsLog();
            log.UUID = UUID;
            log.ConnectionIdentifier = Identifier;
            log.Description = Description;

            db.ConnectionsLog.Add(log);
        }

        public void SavetoSQL()
        {
            db.SaveChanges();
        }
    }
}