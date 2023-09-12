using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace CustomerManagementSystem.Models
{
    public class DataTablesRequest
    {
        [JsonPropertyName("draw")]
        public int Draw { get; set; }

        [JsonPropertyName("start")]
        public int Start { get; set; }

        [JsonProperty(PropertyName = "length")]
        public int Length { get; set; }
        
        [JsonProperty(PropertyName = "columns")]
        public List<DataTablesColumn> Columns { get; set; }

        [JsonProperty(PropertyName = "order")]
        public List<DataTablesOrder> Order { get; set; }

        [JsonProperty(PropertyName = "search")]
        public DataTablesSearch Search { get; set; }
    }

    public class DataTablesColumn
    {
        [JsonProperty(PropertyName = "data")]
        public string Data { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "searchable")]
        public bool Searchable { get; set; }

        [JsonProperty(PropertyName = "orderable")]
        public bool Orderable { get; set; }

        [JsonProperty(PropertyName = "search")]
        public DataTablesSearch Search { get; set; }
    }

    public class DataTablesOrder
    {
        [JsonProperty(PropertyName = "column")]
        public int Column { get; set; }

        [JsonProperty(PropertyName = "dir")]
        public string Dir { get; set; }
    }

    public class DataTablesSearch
    {
        [JsonProperty(PropertyName = "value")]
        public string? Value { get; set; }

        [JsonProperty(PropertyName = "regex")]
        public bool Regex { get; set; }
    }

}
