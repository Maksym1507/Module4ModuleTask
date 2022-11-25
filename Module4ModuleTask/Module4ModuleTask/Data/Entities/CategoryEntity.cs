﻿namespace Module4ModuleTask.Data.Entities
{
    public class CategoryEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public bool Active { get; set; }

        public List<ProductEntity> Products { get; set; } = null!;
    }
}
