﻿using System;
using System.ComponentModel.DataAnnotations;
using DomainModels.Infrastructure;

namespace DomainModels.Model
{
    [MetadataType(typeof(PayingItemMetaData))]
    public partial class PayingItem
    {

    }

    public class PayingItemMetaData
    {        
        public int ItemID { get; set; }

        [Required(ErrorMessage = "Необходимо ввести сумму")]
        [Display(Name = "Сумма")]
        [DataType(DataType.Currency)]
        public decimal Summ { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Необходимо указать дату")]
        public DateTime Date { get; set; }

        [AccountModel(ErrorMessage = "Необходимо завести хотя бы один счет")]
        public int AccountID { get; set; }

        [CategoryModel(ErrorMessage = "Необходимо завести хотя бы одну категорию")]
        public int CategoryID { get; set; }
    }
}
