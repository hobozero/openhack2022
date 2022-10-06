using System;
using System.Collections.Generic;
using System.Text;

namespace OrderAggregator
{
    public enum DocType
    {
        OrderHead,
        OrderDetail,
        ProductDetail
    }

    public class OrderEntity
    {
        public string OrderHeader { get; set; }
        public string OrderDetails { get; set; }
        public string ProductDetails { get; set; }
    }
}
