﻿namespace ApartamentAPI.Exceptions
{
    public class ItemsDoNotExists : Exception
    {
        public ItemsDoNotExists(string? message):base(message) { }
    }
}
