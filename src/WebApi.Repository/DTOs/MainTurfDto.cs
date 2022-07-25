﻿namespace WebApi.Repository.DTOs
{
    public class MainTurfDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ImageLink { get; set; }
        public Guid OwnerId { get; set; }
        public string OwnerName { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
    }
}
