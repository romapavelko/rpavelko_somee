using System;
using System.Collections.Generic;

namespace rpavelko.Data.Extensions
{
    public static class EnumerableExtentions
    {
        public static JsonCollection<T> AsJsonCollection<T>(this IEnumerable<T> source, int page, int rows, int total)
        {
            return new JsonCollection<T>(total, page, rows, source);
        }
    }
    
    public class JsonCollection<T>
    {
        public JsonCollection(int count, int currentPage, int totalRows, IEnumerable<T> stuff)
        {
            records = count;
            page = currentPage;
            total = (int)Math.Ceiling(count / (float)totalRows);
            rows = stuff;
        }
      
        public int records { get; set; } //-------------count of records to show
        public int page { get; set; } //----------------number of page
        public int total { get; set; } //---------------count of records all in DB
        public IEnumerable<T> rows { get; set; } //-----Collection to show
    }

}
