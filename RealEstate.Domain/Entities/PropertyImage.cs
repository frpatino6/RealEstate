﻿namespace RealEstate.Domain.Entities
{
    public class PropertyImage
    {
        public Guid IdPropertyImage { get; set; }
        public Guid IdProperty { get; set; }
        public Property Property { get; set; }
        public string File { get; set; }
        public bool Enabled { get; set; }
    }
}