using System;
using System.Collections.Generic;

namespace BasicModel
{
    public class PageModel
    {
        public int Index { get; set; }

        public int Size { get; set; }

        public int Total { get; set; }

        public PageModel() { }

        public PageModel(int index, int size, int total)
        {
            Index = index;
            Size = size;
            Total = total;
        }
    }

    public class PageModel<T> : PageModel where T : class
    {
        public List<T> Data { get; set; }

        public PageModel(int index, int size, int total, List<T> data) : base(index, size, total)
        {
            this.Data = data;
        }
    }
}
