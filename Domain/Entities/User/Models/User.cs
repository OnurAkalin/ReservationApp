using System;
using System.Collections.Generic;
using Core.Entities;

namespace Domain.Entities
{
    public class User : EntityBaseSiteWithAudit<Guid>, IEntity
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public List<UserRole> UserRoles { get; set; }
        public List<Calendar> Calendars { get; set; }
    }
}