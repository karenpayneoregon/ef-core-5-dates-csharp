﻿
// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>

using System;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WorkingWithDates.Models
{
    public partial class Sales
    {
        [Key]
        public int Id { get; set; }
        public DateTime? SaleDate { get; set; }
        public int? ShipCountry { get; set; }
    }
}