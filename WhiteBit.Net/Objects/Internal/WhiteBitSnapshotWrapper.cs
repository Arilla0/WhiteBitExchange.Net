using Newtonsoft.Json;

namespace WhiteBit.Net.Objects.Internal
{
    internal class WhiteBitSnapshotWrapper<T>
    {
        public int Code { get; set; }
        [JsonProperty("msg")] 
        public string Message { get; set; } = string.Empty;
        [JsonProperty("snapshotVos")]
        public T SnapshotData { get; set; } = default!;
    }
}
