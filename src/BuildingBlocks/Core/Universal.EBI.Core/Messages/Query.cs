using System;

namespace Universal.EBI.Core.Messages
{
    public abstract class Query
    {
        public DateTime Timestamp { get; set; }

        protected Query()
        {
            Timestamp = DateTime.UtcNow;
        }
    }
}
