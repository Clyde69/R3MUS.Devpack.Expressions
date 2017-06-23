﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R3MUS.Devpack.Expressions.Tests
{
    public class TestModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Things { get; set; }
        public bool Enabled { get; set; }
    }
}
