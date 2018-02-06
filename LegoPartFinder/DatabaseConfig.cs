using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace LegoPartFinder
{
    // Model of tables
    public class Colors
    {
        public int id { get; set; }
        public string name { get; set; }
        public string rgb { get; set; }
        public string is_trans { get; set; }
    }

    public class Inventories
    {
        public int id { get; set; }
        public int version { get; set; }
        public string set_num { get; set; }
    }

    public class Inventory_Parts
    {
        public int inventory_id { get; set; }
        public string part_num { get; set; }
        public int color_id { get; set; }
        public int quantity { get; set; }
        public string is_spare { get; set; }
        public int id { get; set; }
    }

    public class Inventory_Sets
    {
        public int inventory_id { get; set; }
        public string set_num { get; set; }
        public int quantity { get; set; }
        public int id { get; set; }
    }

    public class Part_Categories
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Parts
    {
        public string part_num { get; set; }
        public string name { get; set; }
        public int part_cat_id { get; set; }
        public int id { get; set; }
    }

    public class Sets
    {
        public string set_num { get; set; }
        public string name { get; set; }
        public int year { get; set; }
        public int theme_id { get; set; }
        public int num_parts { get; set; }
        public int id { get; set; }
    }

    public class Themes
    {
        public int id { get; set; }
        public string name { get; set; }
        public int parent_id { get; set; }
    }

    // SQLite context setup
    public class SQLiteContext : DbContext
    {
        public DbSet<Colors> Colors { get; set; }
        public DbSet<Inventories> Inventories { get; set; }
        public DbSet<Inventory_Parts> Inventory_Parts { get; set; }
        public DbSet<Inventory_Sets> Inventory_Sets { get; set; }
        public DbSet<Part_Categories> Part_Categories { get; set; }
        public DbSet<Parts> Parts { get; set; }
        public DbSet<Sets> Sets { get; set; }
        public DbSet<Themes> Themes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = database/main.db");
        }
    }
}
