﻿namespace OnlineShop.Services.Basket.BusinessLayer.Models.Dto
{
    public class ResponseDto<T>
    {
        public T? Result { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }
}