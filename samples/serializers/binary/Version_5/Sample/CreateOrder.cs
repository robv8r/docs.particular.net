﻿using System;
using System.Collections.Generic;
using NServiceBus;

[Serializable]
public class CreateOrder : IMessage
{
    public int OrderId { get; set; }
    public DateTime Date { get; set; }
    public int CustomerId { get; set; }
    public List<OrderItem> OrderItems { get; set; }
}