﻿using MediatR;
using System;

namespace Universal.EBI.Core.Messages.Integration
{
    public class Event : Message, INotification
    {
        public DateTime Timestamp { get; private set; }

        protected Event()
        {
            Timestamp = DateTime.Now;
        }
    }
}
