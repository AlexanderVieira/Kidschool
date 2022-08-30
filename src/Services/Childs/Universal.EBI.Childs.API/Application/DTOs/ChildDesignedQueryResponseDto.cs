﻿using System;

namespace Universal.EBI.Childs.API.Application.DTOs
{
    public class ChildDesignedQueryResponseDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string GenderType { get; set; }
    }
}
