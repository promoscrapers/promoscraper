﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class KotletaProduct
    {
        int _ID; public int ID { get { return _ID; } set { _ID = value; } }
        string _AmazonASIN; public string AmazonASIN { get { return _AmazonASIN; } set { _AmazonASIN = value; } }
        string _Name; public string Name { get { return _Name; } set { _Name = value; } }
        string _ImageURL; public string ImageURL { get { return _ImageURL; } set { _ImageURL = value; } }
        decimal _Price; public decimal Price { get { return _Price; } set { _Price = value; } }
        DateTime _DateTimeWhen; public DateTime DateTimeWhen { get { return _DateTimeWhen; } set { _DateTimeWhen = value; } }
        bool _isActive; public bool isActive { get { return _isActive; } set { _isActive = value; } }
    }
}
