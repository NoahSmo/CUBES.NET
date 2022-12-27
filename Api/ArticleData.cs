using System;

namespace Api
{
    public class ArticleData
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public string Image { get; set; }
        
        public string Price { get; set; }
        
        public int Category { get; set; }
        
        public int Provider { get; set; }
        
        public string Year { get; set; }
    }
}
