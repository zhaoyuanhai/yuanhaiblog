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
        public IEnumerable<T> Data { get; set; }

        public PageModel(int index, int size, int total, List<T> data) : base(index, size, total)
        {
            this.Data = data;
        }

        public PageModel(PageModel pageModel, IEnumerable<T> data)
        {
            this.Index = pageModel.Index;
            this.Size = pageModel.Size;
            this.Total = pageModel.Total;
            this.Data = data;
        }
    }
}
