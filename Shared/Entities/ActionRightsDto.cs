﻿namespace DogtrekkingCzShared.Entities
{
    public record ActionRightsDto
    {
        public string? Id { get; set; } = Guid.Empty.ToString();

        public string UserId { get; set; } = string.Empty;

        public string ActionId { get; set; } = string.Empty;
        
        public RightsType Rights { get; set; }

        public IList<string> Roles { get; set; } = new List<string>();

        public enum RightsType
        {
            None = 0,
            Admin = 1
        };
    }
}
