﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedClasses.DTOs
{
    public class EventRegistrationDTO
    {
        public string UserId { get; set; }
        public Guid EventId { get; set; }
    }
}
